using Init.SIGePro.Protocollo.Auriga;
using Init.SIGePro.Protocollo.Auriga.Folder.Exceptions;
using Init.SIGePro.Protocollo.Auriga.Folder.GetMetadataFolder;
using Init.SIGePro.Protocollo.Auriga.Folder.GetMetadataFolder.Response;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Web;

namespace Init.SIGePro.Protocollo
{
    public class PROTOCOLLO_AURIGA : ProtocolloBase
    {

        public override DatiProtocolloRes Protocollazione(DatiProtocolloIn protoIn)
        {

            //risoluzione della verticalizzazione
            var par = new ParametriRegoleInfoAdapter(base.DatiProtocollo.Token, base.DatiProtocollo.IdComuneAlias, base.DatiProtocollo.Software, base.DatiProtocollo.CodiceComune).Adatta();
            var proxyReqInfo = new ProxyRequestInfoAdapter(par).Adatta();

            //protocollazione
            var protocolloResponse = new Auriga.UD.AddUD.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).Protocolla(protoIn);

            if (protocolloResponse != null)
            {
                var r = protocolloResponse.ToDatiProtocolloRes();
                r.DataProtocollo = DateTime.Now.Date.ToString("dd/MM/yyyy");

                return r;
            }

            return null;
        }

        public override DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            //risoluzione della verticalizzazione
            var par = new ParametriRegoleInfoAdapter(base.DatiProtocollo.Token, base.DatiProtocollo.IdComuneAlias, base.DatiProtocollo.Software, base.DatiProtocollo.CodiceComune).Adatta();
            var proxyReqInfo = new ProxyRequestInfoAdapter(par).Adatta();

            var leggiprotocolloresponse = new Auriga.UD.GetMetadataUd.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).LeggiProtocollo(idProtocollo, annoProtocollo, numeroProtocollo);

            if (leggiprotocolloresponse != null)
            {
                var r = leggiprotocolloresponse.ToDatiProtocolloLetto();

                return r;
            }
            return null;
        }

        public override DatiFascicolo Fascicola(Fascicolo fascicolo)
        {
            //risoluzione della verticalizzazione
            var par = new ParametriRegoleInfoAdapter(base.DatiProtocollo.Token, base.DatiProtocollo.IdComuneAlias, base.DatiProtocollo.Software, base.DatiProtocollo.CodiceComune).Adatta();
            var proxyReqInfo = new ProxyRequestInfoAdapter(par).Adatta();

            var updProtFascicoloResponse = new Auriga.UD.UppUD.ResponseInfo();
            var fascicoloresponse = new Auriga.Folder.NewFolder.ResponseInfo();

            //tento la fascicolazione ma il fascicolo potrebbe anche esistere
            if (String.IsNullOrEmpty(fascicolo.NumeroFascicolo))
            {

                try
                {
                    fascicoloresponse = new Auriga.Folder.NewFolder.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).Fascicola(fascicolo);
                }
                catch(FolderExistException ex)
                {
                    var leggiFascicoloResponse = new Auriga.Folder.GetMetadataFolder.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).LeggiFascicoloDaPath(ex.Libreria, ex.PathNome);

                    fascicolo.AnnoFascicolo = leggiFascicoloResponse.ServiceResponse.Apertura.DataOra.Year;
                    fascicolo.DataFascicolo = leggiFascicoloResponse.ServiceResponse.Apertura.DataOra.ToString("dd/MM/yyyy");
                    fascicolo.NumeroFascicolo = ((FascDiTitolarioType)leggiFascicoloResponse.ServiceResponse.Item).NroFascicolo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (fascicoloresponse != null && !string.IsNullOrEmpty(fascicoloresponse.IdFolder))
            {
                //aggiorno il protocollo
                updProtFascicoloResponse = new Auriga.UD.UppUD.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).FascicolaProtocollo(base.IdProtocollo, fascicoloresponse.IdFolder);

                //rilettura dei dati del fascicolo
                var leggiFascicoloResponse = new Auriga.Folder.GetMetadataFolder.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).LeggiFascicoloDaID(fascicoloresponse.IdFolder);

                //ritorno i dati del fascicolo al sistema di protocollazione
                return new DatiFascicolo
                {
                    AnnoFascicolo = leggiFascicoloResponse.ServiceResponse.Apertura.DataOra.Year.ToString(),
                    DataFascicolo = leggiFascicoloResponse.ServiceResponse.Apertura.DataOra.ToString("dd/MM/yyyy"),
                    NumeroFascicolo = ((FascDiTitolarioType)leggiFascicoloResponse.ServiceResponse.Item).NroFascicolo,
                    Errore = string.IsNullOrEmpty(updProtFascicoloResponse.WsError) ? null : new ErroreProtocollo
                    {
                        Descrizione = updProtFascicoloResponse.WsError
                    },
                    Warning = updProtFascicoloResponse.WarningMessage
                };
            }
            else
            {

                if (!fascicolo.AnnoFascicolo.HasValue)
                {
                    throw new InvalidOperationException("Non è possibile associare il protocollo ad un fascicolo esistente senza aver specificato l'anno di apertura del fascicolo");
                }

                var idProtocollo = base.IdProtocollo;
                var numFascicolo = fascicolo.NumeroFascicolo;
                var classifica = fascicolo.Classifica;
                var annoFascicolo = fascicolo.AnnoFascicolo.Value.ToString();

                //aggiorno il protocollo
                updProtFascicoloResponse = new Auriga.UD.UppUD.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).FascicolaProtocollo(idProtocollo, numFascicolo, annoFascicolo, classifica);

                //ritorno i dati del fascicolo al sistema di protocollazione
                return new DatiFascicolo
                {
                    AnnoFascicolo = fascicolo.AnnoFascicolo.Value.ToString(),
                    DataFascicolo = fascicolo.DataFascicolo,
                    NumeroFascicolo = fascicolo.NumeroFascicolo,
                    Errore = string.IsNullOrEmpty(updProtFascicoloResponse.WsError) ? null : new ErroreProtocollo
                    {
                        Descrizione = updProtFascicoloResponse.WsError
                    },
                    Warning = updProtFascicoloResponse.WarningMessage
                };
            }
        }

        public override DatiProtocolloFascicolato IsFascicolato(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            var par = new ParametriRegoleInfoAdapter(base.DatiProtocollo.Token, base.DatiProtocollo.IdComuneAlias, base.DatiProtocollo.Software, base.DatiProtocollo.CodiceComune).Adatta();
            var proxyReqInfo = new ProxyRequestInfoAdapter(par).Adatta();

            var leggiprotocolloresponse = new Auriga.UD.GetMetadataUd.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).LeggiProtocollo(idProtocollo, annoProtocollo, numeroProtocollo);

            if (leggiprotocolloresponse != null)
            {
                return new DatiProtocolloFascicolato
                {
                    Fascicolato = (leggiprotocolloresponse.IsFascicolato) ? EnumFascicolato.si : EnumFascicolato.no,
                    AnnoFascicolo = leggiprotocolloresponse.AnnoFascicolo,
                    NumeroFascicolo = leggiprotocolloresponse.NumeroFascicolo,
                    Classifica = leggiprotocolloresponse.Classifica,
                    Oggetto = leggiprotocolloresponse.OggettoFasc,
                };
            }

            return new DatiProtocolloFascicolato { Fascicolato = EnumFascicolato.no };
        }

        public override AllOut LeggiAllegato()
        {
            var par = new ParametriRegoleInfoAdapter(base.DatiProtocollo.Token, base.DatiProtocollo.IdComuneAlias, base.DatiProtocollo.Software, base.DatiProtocollo.CodiceComune).Adatta();
            var proxyReqInfo = new ProxyRequestInfoAdapter(par).Adatta();

            Auriga.UD.ExtractOne.ResponseInfo allegatoResponse;

            if (String.IsNullOrEmpty(IdAllegato))
            {
                allegatoResponse = new Auriga.UD.ExtractOne.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).EstraiPrimario(IdProtocollo, AnnoProtocollo, NumProtocollo);

            }
            else 
            {
                allegatoResponse = new Auriga.UD.ExtractOne.ServiceWrapper(par, base._protocolloSerializer, base._protocolloLogs, proxyReqInfo).EstraiAllegato(IdProtocollo, AnnoProtocollo, NumProtocollo, IdAllegato);
            }

            var retVal = allegatoResponse.ToAllOut();
            return retVal;
        }
    }
}