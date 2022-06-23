using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class CategorieStradeDistributori
    {
        public readonly string Distributori;
        public readonly string Cosap;

        public CategorieStradeDistributori(string distributori, string cosap)
        {
            Distributori = distributori;
            Cosap = cosap;
        }
    }
}
