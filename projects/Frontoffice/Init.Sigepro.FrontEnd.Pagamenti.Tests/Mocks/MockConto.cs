using Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Init.Sigepro.FrontEnd.Pagamenti.Tests.Mocks
{
    public class MockConto : IContoDTO
    {
        public int Id => 1;

        public string CodiceConto => "CodiceConto";

        public string CodiceSottoConto => "CodiceSottoconto";

        public string NumeroAccertamento => "NumeroAccertamento";

        public string Conto => "Conto";

        public int? AnnoAccertamento => 2021;

        public int? Iva => 20;

        public string CodiceRiferimentoCausale => "CodiceCausalePeople";

        public string NumeroSottoAccertamento => "NumeroSottoAccertamento";
    }
}
