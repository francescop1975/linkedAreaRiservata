using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Frontoffice
{
    public class WsEndoprocedimentiFrontofficeRepository: IEndoprocedimentiFrontofficeRepository
    {
        private readonly EndoFrontofficeServiceCreator _serviceCreator;

        internal WsEndoprocedimentiFrontofficeRepository(EndoFrontofficeServiceCreator serviceCreator)
        {
            _serviceCreator = serviceCreator;
        }


        public FamigliaEndoFrontoffice[] GetListaFamiglieFrontoffice(string alias, string software)
        {
            using (var ws = _serviceCreator.CreateClient(alias))
            {
                try
                {
                    return ws.Service.GetFamiglieEndoFrontoffice(ws.Token, software).ToArray();
                }
                catch (Exception)
                {
                    ws.Service.Abort();
                    throw;
                }
            }
        }

        public CategoriaEndoFrontoffice[] GetListaCategorieFrontoffice(string alias, string software, int codiceFamiglia)
        {
            using (var ws = _serviceCreator.CreateClient(alias))
            {
                try
                {
                    return ws.Service.GetCategorieEndoFrontoffice(ws.Token, software, codiceFamiglia).ToArray();
                }
                catch (Exception)
                {
                    ws.Service.Abort();
                    throw;
                }
            }
        }

        public EndoBreveFrontoffice[] GetListaEndoFrontoffice(string alias, string software, int codiceCategoria)
        {
            using (var ws = _serviceCreator.CreateClient(alias))
            {
                try
                {
                    return ws.Service.GetListaEndoFrontoffice(ws.Token, software, codiceCategoria).ToArray();
                }
                catch (Exception)
                {
                    ws.Service.Abort();
                    throw;
                }
            }
        }

        public RisultatoCaricamentoGerarchiaEndo CaricaGerarchiaFrontoffice(string alias, int id, LivelloCaricamentoGerarchia livello)
        {
            using (var ws = _serviceCreator.CreateClient(alias))
            {
                try
                {
                    return ws.Service.GetGerarchiaEndo(ws.Token, id, livello);
                }
                catch (Exception)
                {
                    ws.Service.Abort();
                    throw;
                }
            }
        }

        public RisultatoRicercaTestualeEndo RicercaTestualeEndoFrontoffice(string alias, string software, string partial, TipoRicercaEnum tipoRicerca)
        {
            using (var ws = _serviceCreator.CreateClient(alias))
            {
                try
                {
                    return ws.Service.RicercaTestualeEndo(ws.Token, software, partial, tipoRicerca);
                }
                catch (Exception)
                {
                    ws.Service.Abort();

                    throw;
                }
            }
        }
    }
}
