using Init.SIGePro.Protocollo.AurigaProxyService;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Linq;
using System.Collections.Generic;
using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Auriga.SharedInfo;

namespace Init.SIGePro.Protocollo.Auriga.UD.AddUD
{

    internal class SoggettoNotifica 
    { 
        public string Mezzo { get; set; }
        public string Pec { get; set; }
    }
    
    
    public class ServiceWrapper : ProxyServiceWrapper
    {

        private DatiProtocolloIn _protoIn;
        private static string _caratteriDaEliminare;

        private static class Constants
        {
            public const string Titolo = "PROTOCOLLAZIONE";
            public const string NameSpace = @"http://addunitadoc.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSAddUd";
            public const string RequestServiceName = "add";
            public const NewUDRegistrazioneDaDareCategoriaReg registroDefault = NewUDRegistrazioneDaDareCategoriaReg.PG;
        }

        public ServiceWrapper(ParametriRegoleInfo parametri, ProtocolloSerializer serializer, ProtocolloLogs log, ProxyRequestInfo request )
        {
            this._parametri = parametri;
            this._serializer = serializer;
            this._logs = log;
            this._request = request;
            this._titolo = Constants.Titolo;
            this._serviceName = Constants.ServiceName;
            _caratteriDaEliminare = this._parametri.CaratteriDaEliminare;
        }
        public ResponseInfo Protocolla(DatiProtocolloIn protoIn)
        {
            try
            {
                this._protoIn = protoIn;

                using (var ws = CreaWebService())
                {
                    ServiceRequestInfo addUdRequest = new ServiceRequestInfo
                    {
                        RegistrazioneDaDare = SetNewUDRegistrazioneDaDare(),
                        OggettoUD = this._protoIn.Oggetto,
                        TipoProvenienza = MapProvenienza(),
                        Items = SetItems(),
                        AssegnazioneInterna = SetAssegnazioneInterna(),
                        CollocazioneClassificazioneUD = SetCollocazioneInterna(),
                        AllegatoUD = SetAllegati(),
                        AttributoAddUD = this._protoIn.Flusso == "P" ? SetAttributoAddUD( ) : null
                    };

                    var addUdRequestXML = Utility.HtmlEncodeContent( this._serializer.Serialize( "addUdRequest.xml", addUdRequest ) );


                    this._request.xml =addUdRequestXML;

                    this._request.hash = getHashSHA1();

                    if(this._protoIn.Allegati != null && this._protoIn.Allegati.Count > 0)
                    {
                        this._request.codiciOggetto = new List<int>();
                        this._protoIn.Allegati.ForEach(x => this._request.codiciOggetto.Add( Convert.ToInt32(x.CODICEOGGETTO)));
                    }

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.ProtocollazioneRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.ProtocollazioneResponseFileName, serviceResponse);

                    this.LogInfoResponseWS(responseXml);

                    var response = new ResponseInfoAdapter(serviceResponse).Adatta();

                    if (response.WsResult != "1")
                        throw new Exception(response.WsError);

                    this.LogSuccess();

                    return response;

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{this._titolo} fallita: {ex.Message}", ex);
            }
        }

        private AttributoAddizionaleType[] SetAttributoAddUD()
        {
            var retVal = new List<AttributoAddizionaleType>();

            var elencoSoggetti = new List<SoggettoNotifica>();

            if (this._protoIn.Destinatari.Amministrazione != null)
            {
                elencoSoggetti.AddRange(this._protoIn.Destinatari.Amministrazione.Where(x => !String.IsNullOrEmpty(x.PEC)).Select(x => new SoggettoNotifica
                {
                    Mezzo = x.Mezzo,
                    Pec = x.PEC
                }));
            }

            if (this._protoIn.Destinatari.Anagrafe != null)
            {
                elencoSoggetti.AddRange(this._protoIn.Destinatari.Anagrafe.Where(x => !String.IsNullOrEmpty(x.Pec)).Select(x => new SoggettoNotifica
                {
                    Mezzo = x.Mezzo,
                    Pec = x.Pec
                }));
            }


            if (elencoSoggetti.Where(x => x.Mezzo == "PEC").Any()) 
            {
                retVal.Add( new AttributoAddizionaleType 
                { 
                    Nome = "INDIRIZZO_EMAIL_DEST_Ud",
                    Item = new AttributoAddizionaleTypeLista 
                    {
                        Riga = elencoSoggetti.Where(x => x.Mezzo == "PEC").Select( x => new AttributoAddizionaleTypeListaRigaColonna{ Nro = "1", Text = new[] { x.Pec } } ).ToArray()
                    } 
                } );
            }

           
            return retVal.ToArray();
        }

        private string MapProvenienza()
        {
            switch (this._protoIn.Flusso)
            {
                case "A": return "E";
                case "P": return "U";
                default: return this._protoIn.Flusso;
            }
        }
        private NewUDRegistrazioneDaDare[] SetNewUDRegistrazioneDaDare()
        {
            return new NewUDRegistrazioneDaDare[]
            {
                new NewUDRegistrazioneDaDare{
                    CategoriaReg = Constants.registroDefault,
                    AnnoReg = DateTime.Now.Year.ToString(),
                    SiglaReg = null
                }
            };
        }
        private object[] SetItems()
        {
            List<object> list = new List<object>();

            switch (this._protoIn.Flusso)
            {
                case "A":
                    {
                        list.AddRange(GetDatiEntrata());
                        break;
                    }
                case "I":
                    {
                        list.AddRange(GetNewUDDatiProduzione());
                        break;
                    }
                case "P":
                    {
                        list.AddRange(GetNewUDDatiProduzione());
                        list.AddRange(GetDatiUscita());
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            return list.ToArray();
        }
        private NewUDDatiEntrata[] GetDatiEntrata()
        {
            return new NewUDDatiEntrata[] {
                new NewUDDatiEntrata
                {
                    MittenteEsterno = SetMittenti(),
                    DataDocRicevutoSpecified = false,
                    DataOraArrivo = GetNowWithoutSeconds(),
                    DataOraArrivoSpecified = true, 
                    DataRaccomandataSpecified = false,
                }
            };
        }

        private DateTime GetNowWithoutSeconds()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0, 0, DateTimeKind.Local);
        }

        private SoggettoEsternoType[] SetMittenti()
        {
            List<SoggettoEsternoType> mittenti = new List<SoggettoEsternoType>();
            if (this._protoIn.Mittenti != null)
            {
                if (this._protoIn.Mittenti.Anagrafe != null)
                {
                    var v = this._protoIn.Mittenti.Anagrafe.ConvertAll(new Converter<ProtocolloAnagrafe, SoggettoEsternoType>(AnagrafeToSoggettoEsternoType));
                    mittenti.AddRange(v);
                }
                if (this._protoIn.Mittenti.Amministrazione != null)
                {
                    var v = this._protoIn.Mittenti.Amministrazione.ConvertAll(new Converter<Amministrazioni, SoggettoEsternoType>(AmministrazioneToSoggettoEsternoType));
                    mittenti.AddRange(v);
                }
            }
            return mittenti.ToArray();
        }
        private static SoggettoEsternoType AnagrafeToSoggettoEsternoType(ProtocolloAnagrafe anagrafe)
        {
            return new SoggettoEsternoType {
                CodiceFiscale = anagrafe.CODICEFISCALE,
                DataNascitaIstituzioneSpecified = false,
                Denominazione_Cognome = anagrafe.NOMINATIVO,
                FlagFisica = (anagrafe.TIPOANAGRAFE == "F") ? "1" : "0",
                Nome = anagrafe.NOME,
                PartitaIva = anagrafe.PARTITAIVA,
            };
        }
        private static SoggettoEsternoType AmministrazioneToSoggettoEsternoType(Amministrazioni amministrazione)
        {
            return new SoggettoEsternoType
            {
                DataNascitaIstituzioneSpecified = false,
                Denominazione_Cognome = amministrazione.AMMINISTRAZIONE,
                FlagFisica = "0",
                PartitaIva = amministrazione.PARTITAIVA
            };
        }
        private NewUDDatiProduzione[] GetNewUDDatiProduzione()
        {
            return new NewUDDatiProduzione[]
            {
                new NewUDDatiProduzione
                {
                    UffProduttore = new UOType[]
                    {
                        new UOType
                        {
                            LivelloUO = new LivelloGerarchiaType[] {
                                new LivelloGerarchiaType
                                {
                                    Codice = this._protoIn.Mittenti.Amministrazione[0].PROT_UO,
                                    Nro = "1"
                                }
                            }
                        }
                    }
                }
            };
        }
        private NewUDDatiUscita[] GetDatiUscita()
        {
            return new NewUDDatiUscita[] {
                new NewUDDatiUscita
                {
                    DataOraSped = GetNowWithoutSeconds(),
                    DataOraSpedSpecified = true,
                    DataRaccomandataSpecified = false,
                    DestinatarioEsterno = SetDestinatari(),
                }
            };
        }
        private DestinatarioEsternoType[] SetDestinatari()
        {
            List<DestinatarioEsternoType> destinatari = new List<DestinatarioEsternoType>();
            if (this._protoIn.Destinatari != null)
            {
                if (this._protoIn.Destinatari.Anagrafe != null && this._protoIn.Destinatari.Anagrafe.Count > 0)
                {
                    var v = this._protoIn.Destinatari.Anagrafe.ConvertAll(new Converter<ProtocolloAnagrafe, DestinatarioEsternoType>(AnagrafeToDestinatarioEsternoType));
                    destinatari.AddRange(v);
                }

                if (this._protoIn.Destinatari.Amministrazione != null && this._protoIn.Destinatari.Amministrazione.Count > 0)
                {
                    var v = this._protoIn.Destinatari.Amministrazione.ConvertAll(new Converter<Amministrazioni, DestinatarioEsternoType>(AmministrazioneToDestinatarioEsternoType));
                    destinatari.AddRange(v);
                }
            }
            return destinatari.ToArray();
        }
        private static DestinatarioEsternoType AnagrafeToDestinatarioEsternoType(ProtocolloAnagrafe anagrafe)
        {
            return new DestinatarioEsternoType
            {
                CodiceFiscale = anagrafe.CODICEFISCALE,
                DataNascitaIstituzioneSpecified = false,
                Denominazione_Cognome = anagrafe.NOMINATIVO,
                FlagFisica = (anagrafe.TIPOANAGRAFE == "F") ? "1" : "0",
                Nome = anagrafe.NOME,
                PartitaIva = anagrafe.PARTITAIVA,
            };
        }
        private static DestinatarioEsternoType AmministrazioneToDestinatarioEsternoType(Amministrazioni amministrazione)
        {
            return new DestinatarioEsternoType
            {
                DataNascitaIstituzioneSpecified = false,
                Denominazione_Cognome = amministrazione.AMMINISTRAZIONE,
                FlagFisica = "0",
                PartitaIva = amministrazione.PARTITAIVA,            };
        }
        private AssegnazioneInternaType[] SetAssegnazioneInterna()
        {
            if( this._protoIn.Flusso != "P" )
            {
                if (this._protoIn.Destinatari.Amministrazione != null)
                {
                    List<AssegnazioneInternaType> retval = new List<AssegnazioneInternaType>();
                    var v = this._protoIn.Destinatari.Amministrazione.ConvertAll(new Converter<Amministrazioni, AssegnazioneInternaType>(AmministrazioneToAssegnazioneInternaType));
                    retval.AddRange(v);
                    return retval.ToArray();
                }
            }
            return null;
        }
        private static AssegnazioneInternaType AmministrazioneToAssegnazioneInternaType( Amministrazioni amministrazione)
        {
            return new AssegnazioneInternaType
            {
                Item = new UOType
                {
                    DenominazioneUO = null,
                    IdUO = null,
                    LivelloUO = new LivelloGerarchiaType[] {
                        new LivelloGerarchiaType
                        {
                            Codice = amministrazione.PROT_UO,
                            Nro = "1"
                        }
                    }
                }
            };
        }
        private NewUDCollocazioneClassificazioneUD SetCollocazioneInterna()
        {
            return new NewUDCollocazioneClassificazioneUD
            {
                ClassifFascicolo = new ClassifFascicoloType[]
                {
                    new ClassifFascicoloType
                    {
                        Item = ClassificaToClassifUAType(  )
                    }
                }
            };
        }

        private ClassifUAType ClassificaToClassifUAType()
        {
            return new ClassifUAType {
                LivelloClassificazione = Utility.GetLivelloGerarchiaDaClassifica(this._protoIn.Classifica)
            };
        }

        private AllegatoUDType[] SetAllegati()
        {
            if( this._protoIn.Allegati != null && this._protoIn.Allegati.Count > 0 )
            {
                var retVal = this._protoIn.Allegati.ConvertAll(new Converter<ProtocolloAllegati, AllegatoUDType>(AllegatiToAllegatoUDType));

                int i = 1;
                retVal.ForEach((x) => {
                    x.VersioneElettronica.NroAttachmentAssociato = i.ToString();
                    i++;
                });

                return retVal.ToArray();
            }

            return null;
        }
        private static AllegatoUDType AllegatiToAllegatoUDType(ProtocolloAllegati allegato)
        {
            return new AllegatoUDType
            {
                DesAllegato = new String(allegato.Descrizione.Replace(allegato.Extension,"").Where(c => !_caratteriDaEliminare.Contains(c)).ToArray()),
                VersioneElettronica = new VersioneElettronicaType
                {
                    NomeFile = new String(allegato.NOMEFILE.Where(c => !_caratteriDaEliminare.Contains(c)).ToArray()),
                },
                AttributoAddAlleg = null
            };
        }
    }
}
