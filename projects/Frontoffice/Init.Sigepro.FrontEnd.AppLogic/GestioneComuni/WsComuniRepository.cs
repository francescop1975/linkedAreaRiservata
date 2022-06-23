using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{

    internal class WsComuniRepository
    {
        //const string CACHE_KEY_LISTA_COMUNI_PROVINCIA = "SESSION_KEY_LISTA_COMUNI_PROVINCIA_";
        //const string CACHE_KEY_LISTA_PROVINCIE = "SESSION_KEY_LISTA_PROVINCIE";
        //const string CACHE_KEY_LISTA_CITTADINANZE = "CACHE_KEY_LISTA_CITTADINANZE";
        //const string CACHE_KEY_DATI_COMUNE = "CACHE_KEY_DATI_COMUNE_";
        //const string CACHE_KEY_DATI_PROVINCIA = "CACHE_KEY_DATI_PROVINCIA_";
        //      const string CACHE_KEY_COMUNE_DA_COD_ISTAT = "COMUNE:COD:ISAT:";
        private readonly ComuniServiceCreator _serviceCreator;
        ILog _log = LogManager.GetLogger(typeof(WsComuniRepository));

        public WsComuniRepository(ComuniServiceCreator serviceCreator)
        {
            _serviceCreator = serviceCreator;

        }

        public DatiComuneCompatto GetDatiComune(string codiceComune)
        {
            if (String.IsNullOrEmpty(codiceComune))
                return new DatiComuneCompatto();

            return WsExecute(ws => ws.Service.GetDatiComune(ws.Token, codiceComune));
        }

        public IEnumerable<DatiComuneCompatto> GetListaComuni()
        {
            return GetListaComuni(String.Empty);
        }

        public IEnumerable<DatiComuneCompatto> GetComuniAssociati(string software)
        {
            return WsExecute(ws => ws.Service.GetComuniAssociati(ws.Token, software));
        }

        public string GetPecComuneAssociato(string codiceComune, string software)
        {
            return WsExecute(ws => ws.Service.GetPecComuneAssociato(ws.Token, codiceComune, software));
        }

        public IEnumerable<DatiComuneCompatto> GetListaComuni(string siglaProvincia)
        {
            return WsExecute(ws => ws.Service.GetListaComuni(ws.Token, siglaProvincia));
        }

        public IEnumerable<DatiComuneCompatto> FindComuneDaMatchParziale(string matchComune)
        {
            return this.WsExecute(ws => ws.Service.FindComuniDaMatchParziale(ws.Token, matchComune));
        }

        public DatiProvinciaCompatto GetProvinciaDaCodiceComune(string codiceComune)
        {
            if (String.IsNullOrEmpty(codiceComune))
                return null;

            var comune = GetDatiComune(codiceComune);

            if (comune != null)
            {
                return new DatiProvinciaCompatto
                {
                    Provincia = comune.Provincia,
                    SiglaProvincia = comune.SiglaProvincia
                };
            }

            return null;
        }

        public DatiProvinciaCompatto GetDatiProvincia(string siglaProvincia)
        {
            if (String.IsNullOrEmpty(siglaProvincia))
                return null;

            return this.WsExecute(ws => ws.Service.GetDatiProvincia(ws.Token, siglaProvincia));
        }

        public IEnumerable<DatiProvinciaCompatto> GetListaProvincie()
        {
            return WsExecute(ws => ws.Service.GetListaProvincie(ws.Token));
        }

        public IEnumerable<DatiProvinciaCompatto> FindProvinciaDaMatchParziale(string matchProvincia)
        {
            matchProvincia = matchProvincia.ToUpper();

            return GetListaProvincie().Where(x => x.Provincia.ToUpper().StartsWith(matchProvincia));
        }


        public IEnumerable<CittadinanzaCompatto> GetCittadinanze()
        {
            return WsExecute(ws => ws.Service.GetCittadinanze(ws.Token));
        }

        public CittadinanzaCompatto GetCittadinanzaDaId(int idCittadinanza)
        {
            return WsExecute(ws => ws.Service.GetCittadinanzaById(ws.Token, idCittadinanza));
        }

        public bool IsCittadinanzaExtracomunitaria(int idCittadinanza)
        {
            var cittadinanza = GetCittadinanzaDaId(idCittadinanza);

            if (cittadinanza == null)
                throw new InvalidOperationException("La cittadinanza con codice " + idCittadinanza + " non è stata trovata");

            return cittadinanza.FlgPaeseComunitario;
        }


        public DatiComuneCompatto GetComuneDaCodiceIstat(string codiceIstat)
        {
            return WsExecute(ws => ws.Service.GetComuneDaCodiceIstat(ws.Token, codiceIstat));
        }

        private T WsExecute<T>(Func<ServiceInstance<WsComuniService.WsComuniServiceClient>, T> callback)
        {
            using (var ws = this._serviceCreator.CreateClient())
            {
                try
                {
                    return callback(ws);
                }
                catch (Exception ex)
                {
                    this._log.Error($"{GetType()}, Errore durante la chiamata al web service: {ex} ");
                    ws.Service.Abort();
                    throw;
                }
            }
        }
    }
}
