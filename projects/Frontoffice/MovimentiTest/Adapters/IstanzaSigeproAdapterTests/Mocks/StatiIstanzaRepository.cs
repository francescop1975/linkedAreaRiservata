using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.Adapters.IstanzaSigeproAdapterTests.Mocks
{
    public class StatiIstanzaRepositoryFake : IStatiIstanzaRepository
    {
        public StatiIstanza GetById(string aliasComune, string software, string codiceStato)
        {
            return new StatiIstanza
            {
                Codicestato = "ST1",
                Stato = "STATO 1"
            };
        }

        public StatiIstanza[] GetList(string aliasComune, string software)
        {
            throw new NotImplementedException();
        }
    }
}
