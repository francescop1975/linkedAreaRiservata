using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.Infrastructure.StepsDomanda.Attributi
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class StepPropertyAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public string Descrizione { get; private set; } = "Descrizione non disponibile";

        public string ValoreDefault { get; set; } = "";

        // This is a positional argument
        public StepPropertyAttribute(string descrizione)
        {
            this.Descrizione = descrizione;

        }

    }
}
