using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Verticalizzazioni
{
    public interface IVerticalizzazioneAttiva<T> where T:Verticalizzazione
    {
        bool IsAttiva(string software);
    }
}
