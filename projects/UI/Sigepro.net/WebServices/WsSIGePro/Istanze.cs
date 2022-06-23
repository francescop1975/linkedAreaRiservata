using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using Init.SIGePro.Exceptions.Token;
using Init.SIGePro.Manager;
using Init.SIGePro.Authentication;
using System.ComponentModel;
using PersonalLib2.Data;
using SIGePro.Net.WebServices.WsSIGePro;
using Init.SIGePro.Utils;
using Init.SIGePro.Data;
using System.IO;
using System.Xml.Serialization;
using Init.Utils;
using PersonalLib2.Sql;
using System.Configuration;
using Init.SIGePro.Manager.Manager;
using Init.SIGePro.Protocollo;
using System.ServiceModel;
using log4net;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Manager;
using Init.SIGePro.Manager.DTO.Visura;
using Init.SIGePro.Manager.Logic.Visura;
using Sigepro.net.WebServices.WsSIGePro;
using Init.SIGePro.Verticalizzazioni;
using Init.SIGePro.Manager.Logic.SubvisuraPratica;
using System.Linq;
using Init.SIGePro.Manager.Logic.GestioneOggetti;

namespace SIGePro.Net.WebServices.WsSIGePro
{
    /// <summary>
    /// Summary description for Istanze
    /// </summary>
    [WebService(Namespace = "http://init.sigepro.it")]
	[ServiceContract(Namespace = "http://init.sigepro.it")]
    public class IstanzeWs : SigeproWebService
    {
		ILog _log = LogManager.GetLogger(typeof(IstanzeWs));

		[WebMethod]
		[OperationContract]
		public Init.SIGePro.Data.Istanze GetDettaglioPratica(string token, int codiceIstanza )
		{
			AuthenticationInfo ai = AuthenticationManager.CheckToken(token);

			if (ai == null)
				throw new InvalidTokenException(token);


			try
			{
				Istanze istanza = new IstanzeMgr(ai.CreateDatabase()).GetById(ai.IdComune, codiceIstanza, useForeignEnum.Recoursive);// VisuraPraticheManager(ai).GetDettaglioPratica(request);

				return istanza;
			}
			catch (Exception ex)
			{
				Logger.LogEvent(ai, "GetDettaglioPratica", ex.Message, "ERR_DETTAGLIO");
				throw new Exception(ex.Message);
			}
		}

        [WebMethod]
        [OperationContract]
        public Init.SIGePro.Data.Istanze GetDettaglioPraticaByUuid(string token, string uuid, bool effettuaSubVisuraMovimenti)
        {
            AuthenticationInfo ai = AuthenticationManager.CheckToken(token);

            if (ai == null)
                throw new InvalidTokenException(token);
            
            try
            {
                using (var db = ai.CreateDatabase())
                {
                    var mgr = new IstanzeMgr(db);
                    var riferimentiIstanza = mgr.GetCodiceIstanzaDaUuid( uuid);

                    var istanza = mgr.GetById(riferimentiIstanza.IdComune, riferimentiIstanza.CodiceIstanza, useForeignEnum.Recoursive);

                    if (effettuaSubVisuraMovimenti)
                    {
                        // Leggo eventuali poratiche collegate
                        var verticalizzazioneStc = new VerticalizzazioneStc(ai.Alias, istanza.SOFTWARE);
                        var subVisuraService = new SubvisuraPraticheService(ai, verticalizzazioneStc);

                        // TODO: spostare il ciclo nel service e magari parallelizzare il lavoro
                        // visto che si tratta di un'operazione abbastanza lenta
                        foreach (var movimento in istanza.Movimenti.Where(x => x.INVIATO_CON_STC.GetValueOrDefault(0) != 0))
                        {
                            var uidPraticaCollegata = subVisuraService.GetUidPraticaCollegataDaIdMovimento(movimento);

                            movimento.UuidPraticaCollegata = uidPraticaCollegata;
                        }
                    }

                    return istanza;
                }
            }
            catch (Exception ex)
            {
                Logger.LogEvent(ai, "GetDettaglioPratica", ex.Message, "ERR_DETTAGLIO");
                throw new Exception(ex.Message);
            }
        }

        [WebMethod]
        [OperationContract]
        public RisultatoVisuraPraticaV3 GetListaPraticheV3(string token, RichiestaListaPraticheV3 richiestaLista)
        {
            var ai = CheckToken(token);

            try
            {
                return new VisuraPraticheV3Service(ai).GetListaPratiche(richiestaLista);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore nella chiamata a GetListaPratiche: {0} - {1}", ex.ToString(), StreamUtils.SerializeClass(richiestaLista));

                throw;
            }
        }
        /*
        [WebMethod]
        [OperationContract]
        public RisultatoVisuraListaDto GetListaPratiche(string token, RichiestaListaPratiche richiestaLista)
        {
            var ai = CheckToken(token);

            try
            {
                richiestaLista.CodEnte = ai.IdComune;

                return new VisuraPraticheManager(ai).GetListaPraticheV2(richiestaLista);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore nella chiamata a GetListaPratiche: {0} - {1}", ex.ToString(), StreamUtils.SerializeClass(richiestaLista));

                throw;
            }
        }
        */
        [WebMethod]
        public int[] GetOggettiIstanzaDaValoriMetadato(string token, int codiceIstanza, string chiaveMetadato, string valoreMetadato)
        {
            var authInfo = CheckToken(token);

            using (var db = authInfo.CreateDatabase())
            {
                var repo = new OggettiRepository(db, authInfo.IdComune);

                return repo.GetCodiciOggettoDocumentiIstanzaDaValoreMetadato(codiceIstanza, chiaveMetadato, valoreMetadato).ToArray();
                
            }
        }

        [WebMethod]
        [OperationContract]
        public string GetUuidDaCodiceIstanza(string token, int codiceIstanza)
        {
            var authInfo = CheckToken(token);

            using (var db = authInfo.CreateDatabase())
            {
                var uuid = new IstanzeMgr(db).GetUUIDdaCodiceIstanza(authInfo.IdComune, codiceIstanza);

                if (String.IsNullOrEmpty(uuid))
                {
                    throw new ArgumentException($"Non è stato possibile risalire ad un UUID valido per il codice istanza {authInfo.IdComune}-{codiceIstanza}");
                }

                return uuid;
            }
        }

    }
}
