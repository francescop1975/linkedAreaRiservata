using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class StradarioReferente
    {

        public string IdComune { get; set; }

        public int Id { get; set; }

        public string CodiceStrada { get; set; }


        public double KmIniziale { get; set; }

        public double KmFinale { get; set; }

 
        public int? CodiceFunzionarioPO { get; set; }

   
        public int CodiceTecnicoZona { get; set; }


        public int? CodiceCapoCantone { get; set; }


    }
}
