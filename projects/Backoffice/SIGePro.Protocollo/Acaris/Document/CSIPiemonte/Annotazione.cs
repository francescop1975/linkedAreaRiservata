using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Document.CSIPiemonte
{
    public class Annotazione
    {
        public string Testo { get; internal set; }
        public bool Formale { get; internal set; }
        public bool AnnotaInteroDocumento { get; internal set; }
        public bool AnnotaClassificazioneCorrente { get; internal set; }
    }
}
