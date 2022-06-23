using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.Adapters.IstanzaSigeproAdapterTests.Mocks
{
    public class FormeGiuridicheRepositoryFake : IFormeGiuridicheRepository
    {
        public FormeGiuridiche GetById(string id)
        {
            return new FormeGiuridiche
            {
                FORMAGIURIDICA = "Forma giuridica",
                CODICEFORMAGIURIDICA = id
            };
        }

        public FormeGiuridiche[] GetList(string aliasComune)
        {
            throw new NotImplementedException();
        }
    }
}
