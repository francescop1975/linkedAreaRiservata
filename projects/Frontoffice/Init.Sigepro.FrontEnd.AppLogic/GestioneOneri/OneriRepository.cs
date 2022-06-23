// -----------------------------------------------------------------------
// <copyright file="OneriRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneOneri
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
    using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
    using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
    using CuttingEdge.Conditions;
    using Init.Sigepro.FrontEnd.AppLogic.Common;



    internal class OneriRepository : IOneriRepository
    {
        private readonly AreaRiservataServiceCreator _serviceCreator;
        private readonly IAliasResolver _aliasResolver;

        public OneriRepository(AreaRiservataServiceCreator serviceCreator, IAliasResolver aliasResolver)
        {
            this._serviceCreator = serviceCreator ?? throw new ArgumentNullException(nameof(serviceCreator));
            this._aliasResolver = aliasResolver ?? throw new ArgumentNullException(nameof(aliasResolver));
        }

        public IEnumerable<Onere> GetByIdInterventoIdEndo(int codiceIntervento, List<int> listaIdEndo)
        {
            using (var ws = _serviceCreator.CreateClient(_aliasResolver.AliasComune))
            {
                var rVal = ws.Service.GetListaOneriDaIdInterventoECodiciEndo(ws.Token, codiceIntervento, listaIdEndo.ToArray());

                if (rVal == null)
                {
                    return Enumerable.Empty<Onere>();
                }

                return rVal.Select(x => new Onere(x));
            }
        }

        public IEnumerable<TipoPagamento> GetModalitaPagamento()
        {
            using (var ws = _serviceCreator.CreateClient(_aliasResolver.AliasComune))
            {
                return ws.Service.GetModalitaPagamento(ws.Token).Select(x => new TipoPagamento(x.Codice, x.Descrizione));
            }
        }

        public TipoPagamento GetModalitaPagamentoById(string modalitaPagamentoId)
        {
            return this.GetModalitaPagamento().Where(x => x.Codice == modalitaPagamentoId).FirstOrDefault();
        }

        public string GetCodiceCausaleOnereTraslazione(int idCausale)
        {
            using (var ws = _serviceCreator.CreateClient(_aliasResolver.AliasComune))
            {
                return ws.Service.GetCodiceCausaleOnereTraslazione(ws.Token, idCausale);
            }
        }
    }
}
