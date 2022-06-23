using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Auriga.UD.GetMetadataUd
{
    public class ResponseInfo : ProxyResponseInfo
    {
        public DatiUD DatiUD;
        public bool IsFascicolato {
            get
            {
                return !String.IsNullOrEmpty(NumeroFascicolo);
            }
        }
        public string AnnoFascicolo
        {
            get
            {
                return GetAnnoNumeroPratica(this.DatiUD.CollocazioneClassificazioneUD);
            }
        }
        public string NumeroFascicolo
        {
            get
            {
                return GetNumeroPratica(this.DatiUD.CollocazioneClassificazioneUD);
            }
        }
        public string Classifica
        {
            get
            {
                return GetClassifica(this.DatiUD.CollocazioneClassificazioneUD);
            }
        }
        public string OggettoFasc
        {
            get
            {
                return GetOggettoFasc(this.DatiUD.CollocazioneClassificazioneUD);
            }
        }


        public DatiProtocolloLetto ToDatiProtocolloLetto()
        {
            if (!String.IsNullOrEmpty(this.WsError))
            {
                return new DatiProtocolloLetto
                {
                    Errore = new ErroreProtocollo
                    {
                        Descrizione = this.WsError
                    }
                };
            }

            if (this.DatiUD == null || this.DatiUD.RegistrazioneData == null)
            {
                throw new InvalidOperationException("Non è possibile richiamare il metodo ToDatiProtocolloLetto senza aver prima valorizzato DatiUD");
            }

            return new DatiProtocolloLetto
            {
                IdProtocollo = this.DatiUD.IdUD,
                AnnoProtocollo = this.DatiUD.RegistrazioneData[0].AnnoReg,
                DataProtocollo = this.DatiUD.DataOraCreazione,
                NumeroProtocollo = this.DatiUD.RegistrazioneData[0].NumReg,
                Oggetto = this.DatiUD.OggettoUD.Value,
                AnnoNumeroPratica = this.AnnoFascicolo,
                NumeroPratica = this.NumeroFascicolo,
                Classifica_Descrizione = this.Classifica,
                Origine = TipoProvenienzaToOrigine(this.DatiUD.TipoProvenienza),
                MittentiDestinatari = SetMittDestOut(),
                InCaricoA = SetInCaricoA(), // id 
                InCaricoA_Descrizione = SetInCaricoADescrizione(), // descrizione
                Allegati = SetAllegati(this.DatiUD)
            };
        }

        private MittDestOut[] SetMittDestOut()
        {
            switch (this.DatiUD.TipoProvenienza)
            {
                case DatiUDTipoProvenienza.E:
                    {
                        return this.DatiUD.DatiEntrata.MittenteEsterno.ToList().ConvertAll(new Converter<SoggettoEsternoType, MittDestOut>(SoggettoEsternoToMittDestOut)).ToArray();
                    }
                case DatiUDTipoProvenienza.I:
                    {
                        return this.DatiUD.AssegnazioneInterna.ToList().ConvertAll(new Converter<AssegnazioneInternaType, MittDestOut>(AssegnazioneInternaToMittDestOut)).ToArray();
                    }
                case DatiUDTipoProvenienza.U:
                    {
                        return this.DatiUD.DatiUscita.DestinatarioEsterno.ToList().ConvertAll(new Converter<DestinatarioEsternoType, MittDestOut>(DestinatarioEsternoToMittDestOut)).ToArray();
                    }
            }

            return null;
        }
        private static MittDestOut DestinatarioEsternoToMittDestOut(DestinatarioEsternoType soggetto)
        {
            if (soggetto != null)
            {
                return new MittDestOut
                {
                    CognomeNome = $"{soggetto.Denominazione_Cognome} {soggetto.Nome}",
                    IdSoggetto = null,
                };
            }

            return null;
        }
        private static MittDestOut UOTypeToMittDestOut(UOType soggetto)
        {
            if (soggetto != null)
            {
                return new MittDestOut
                {
                    CognomeNome = $"{soggetto.DenominazioneUO}",
                    IdSoggetto = soggetto.IdUO
                };
            }

            return null;
        }
        private static MittDestOut SoggettoEsternoToMittDestOut(SoggettoEsternoType soggetto)
        {
            if (soggetto != null)
            {
                return new MittDestOut
                {
                    CognomeNome = $"{soggetto.Denominazione_Cognome} {soggetto.Nome}",
                    IdSoggetto = null,
                };
            }

            return null;
        }
        private static MittDestOut AssegnazioneInternaToMittDestOut(AssegnazioneInternaType soggetto)
        {
            if (soggetto != null && soggetto.Item != null)
            {
                switch (soggetto.Item.GetType().Name)
                {
                    case "OggDiTabDiSistemaType":   //Gruppo
                        {
                            return new MittDestOut
                            {
                                CognomeNome = ((OggDiTabDiSistemaType)soggetto.Item).Decodifica_Nome,
                                IdSoggetto = ((OggDiTabDiSistemaType)soggetto.Item).CodId
                            };
                        }
                    case "RuoloAmmContestualizzatoType":    // RuoloAmmContestualizzato
                        {
                            return new MittDestOut
                            {
                                CognomeNome = ((RuoloAmmContestualizzatoType)soggetto.Item).RuoloAmm.Decodifica_Nome,
                                IdSoggetto = ((RuoloAmmContestualizzatoType)soggetto.Item).RuoloAmm.CodId
                            };
                        }
                    case "ScrivaniaVirtualeType":   //ScrivaniaVirtuale
                        {
                            return new MittDestOut
                            {
                                CognomeNome = ((ScrivaniaVirtualeType)soggetto.Item).DesScrivaniaVirt,
                                IdSoggetto = null
                            };
                        }
                    case "UOType":   //UO
                        {
                            return new MittDestOut
                            {
                                CognomeNome = ((UOType)soggetto.Item).DenominazioneUO,
                                IdSoggetto = ((UOType)soggetto.Item).IdUO
                            };
                        }
                    case "UserType":   //Utente
                        {
                            return new MittDestOut
                            {
                                CognomeNome = ((UserType)soggetto.Item).Decodifica_CognomeNome,
                                IdSoggetto = ((UserType)soggetto.Item).IdInterno
                            };
                        }
                }
            }
            return null;
        }

        private string SetInCaricoA()
        {
            switch (this.DatiUD.TipoProvenienza)
            {
                case DatiUDTipoProvenienza.E:
                    {
                        return this.DatiUD.AssegnazioneInterna.ToList().ConvertAll(new Converter<AssegnazioneInternaType, string>(AssegnazioneInternaToInCaricoA)).Aggregate((partialRetVal, assint) => $"{partialRetVal} {assint}");
                    }
                case DatiUDTipoProvenienza.I:
                    {
                        return this.DatiUD.AssegnazioneInterna.ToList().ConvertAll(new Converter<AssegnazioneInternaType, string>(AssegnazioneInternaToInCaricoA)).Aggregate((partialRetVal, assint) => $"{partialRetVal} {assint}");
                    }
                case DatiUDTipoProvenienza.U:
                    {
                        return this.DatiUD.AssegnazioneInterna.ToList().ConvertAll(new Converter<AssegnazioneInternaType, string>(AssegnazioneInternaToInCaricoA)).Aggregate((partialRetVal, assint) => $"{partialRetVal} {assint}");
                    }
            }

            return null;
        }
        private static string AssegnazioneInternaToInCaricoA(AssegnazioneInternaType soggetto)
        {
            if (soggetto != null && soggetto.Item != null)
            {
                switch (soggetto.Item.GetType().Name)
                {
                    case "OggDiTabDiSistemaType":   //Gruppo
                        {
                            return ((OggDiTabDiSistemaType)soggetto.Item).CodId;
                        }
                    case "RuoloAmmContestualizzatoType":    // RuoloAmmContestualizzato
                        {
                            return ((RuoloAmmContestualizzatoType)soggetto.Item).RuoloAmm.CodId;
                        }
                    case "ScrivaniaVirtualeType":   //ScrivaniaVirtuale
                        {
                            return "";
                        }
                    case "UOType":   //UO
                        {
                            return ((UOType)soggetto.Item).IdUO;
                        }
                    case "UserType":   //Utente
                        {
                            return ((UserType)soggetto.Item).IdInterno;
                        }
                }
            }
            return string.Empty;
        }

        private string SetInCaricoADescrizione()
        {
            switch (this.DatiUD.TipoProvenienza)
            {
                case DatiUDTipoProvenienza.E:
                    {
                        return this.DatiUD.AssegnazioneInterna.ToList().ConvertAll(new Converter<AssegnazioneInternaType, string>(AssegnazioneInternaToInCaricoADescrizione)).Aggregate((partialRetVal, assint) => $"{partialRetVal} {assint}");
                    }
                case DatiUDTipoProvenienza.I:
                    {
                        return this.DatiUD.AssegnazioneInterna.ToList().ConvertAll(new Converter<AssegnazioneInternaType, string>(AssegnazioneInternaToInCaricoADescrizione)).Aggregate((partialRetVal, assint) => $"{partialRetVal} {assint}");
                    }
                case DatiUDTipoProvenienza.U:
                    {
                        return this.DatiUD.AssegnazioneInterna.ToList().ConvertAll(new Converter<AssegnazioneInternaType, string>(AssegnazioneInternaToInCaricoADescrizione)).Aggregate((partialRetVal, assint) => $"{partialRetVal} {assint}");
                    }
            }

            return null;
        }
        private static string AssegnazioneInternaToInCaricoADescrizione(AssegnazioneInternaType soggetto)
        {
            if (soggetto != null && soggetto.Item != null)
            {
                switch (soggetto.Item.GetType().Name)
                {
                    case "OggDiTabDiSistemaType":   //Gruppo
                        {
                            return ((OggDiTabDiSistemaType)soggetto.Item).Decodifica_Nome;
                        }
                    case "RuoloAmmContestualizzatoType":    // RuoloAmmContestualizzato
                        {
                            return ((RuoloAmmContestualizzatoType)soggetto.Item).RuoloAmm.Decodifica_Nome;
                        }
                    case "ScrivaniaVirtualeType":   //ScrivaniaVirtuale
                        {
                            return ((ScrivaniaVirtualeType)soggetto.Item).DesScrivaniaVirt;
                        }
                    case "UOType":   //UO
                        {
                            return ((UOType)soggetto.Item).DenominazioneUO;
                        }
                    case "UserType":   //Utente
                        {
                            return ((UserType)soggetto.Item).Decodifica_CognomeNome;
                        }
                }
            }
            return null;
        }

        private static string TipoProvenienzaToOrigine(DatiUDTipoProvenienza provenienza)
        {
            switch (provenienza)
            {
                case DatiUDTipoProvenienza.E:
                    {
                        return ProtocolloConstants.COD_ARRIVO;
                    }
                case DatiUDTipoProvenienza.U:
                    {
                        return ProtocolloConstants.COD_PARTENZA;
                    }
                case DatiUDTipoProvenienza.I:
                    {
                        return ProtocolloConstants.COD_INTERNO;
                    }
            }
            return null;
        }

        private static string GetClassifica(DatiUDCollocazioneClassificazioneUD classifica)
        {
            string retVal = null;

            if (classifica != null && classifica.ClassifFascicolo != null)
            {
                classifica.ClassifFascicolo.ToList().ForEach((x) =>
                {
                    if (x.Item.GetType().Name == "ClassifUAType")
                    {
                        ((ClassifUAType)x.Item).LivelloClassificazione.ToList().ForEach((a) =>
                        {
                            retVal += a.Codice + ".";
                        });
                    }
                });
            }

            if( !String.IsNullOrEmpty(retVal))
            {
                retVal = retVal.Substring(0, retVal.Length - 1);
            }
            return retVal;
        }

        private static string GetAnnoNumeroPratica(DatiUDCollocazioneClassificazioneUD classifica)
        {
            if (classifica != null)
            {
                if (classifica.ClassifFascicolo != null)
                {
                    var cf = classifica.ClassifFascicolo.Where(x => x.Item.GetType().Name == "ClassifUAType").FirstOrDefault();
                    if(cf != null )
                    {
                        return ((ClassifUAType)cf.Item).AnnoAperturaUA;
                    }
                }
            }

            return null;
        }
        private static string GetNumeroPratica(DatiUDCollocazioneClassificazioneUD classifica)
        {
            if (classifica != null)
            {
                if (classifica.ClassifFascicolo != null)
                {
                    var cf = classifica.ClassifFascicolo.Where(x => x.Item.GetType().Name == "ClassifUAType").FirstOrDefault();
                    if (cf != null)
                    {
                        return ((ClassifUAType)cf.Item).NroProgrUA;
                    }
                }
            }

            return null;
        }
        private static string GetOggettoFasc(DatiUDCollocazioneClassificazioneUD classifica)
        {
            if (classifica != null)
            {
                if (classifica.ClassifFascicolo != null)
                {
                    return classifica.ClassifFascicolo[0].OggettoFasc;
                }
            }

            return null;
        }
        
        private static AllOut[] SetAllegati(DatiUD dati)
        {

            if (String.IsNullOrEmpty(dati.IdDocPrimario) && dati.AllegatoUD == null)
            {
                return null;
            }

            AllegatoUDType[] allegati = dati.AllegatoUD;
            List<AllOut> elenco = new List<AllOut>();

            //allegato primario
            if (!String.IsNullOrEmpty(dati.IdDocPrimario))
            {
                elenco.Add(new AllOut
                {
                    IDBase = "",
                    Serial = dati.VersioneElettronica.NomeFile,
                    TipoFile = null,
                    Commento = dati.VersioneElettronica.NomeFile,
                    ContentType = ""
                });
            }

            //allegati secondari
            if ( allegati != null)
            {
                for (int i = 0; i < allegati.Length; i++)
                {
                    elenco.Add( new AllOut 
                    {
                        IDBase = (i+1).ToString(),
                        Serial = allegati[i].VersioneElettronica.NomeFile,
                        TipoFile = (allegati[i].TipoDocAllegato != null) ? allegati[i].TipoDocAllegato.Decodifica_Nome : null,
                        Commento = allegati[i].DesAllegato,
                        ContentType = ""
                    } );
                }

            }
            return ( elenco.Count > 0) ? elenco.ToArray() : null;
        }
        
    }
}
