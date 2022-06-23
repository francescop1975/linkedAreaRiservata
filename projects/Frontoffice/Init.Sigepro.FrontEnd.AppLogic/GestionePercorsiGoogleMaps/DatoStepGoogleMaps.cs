using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePercorsiGoogleMaps
{
    public class DatoStepGoogleMaps
    {
        public DatoStepGoogleMaps(double distanza, string descrizione)
        {
            Distanza = distanza;
            Descrizione = descrizione;
        }

        public double Distanza { get; }
        public string Descrizione { get; }
    }
}
