﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Fascicolazione.Lettura
{
    public class LeggiFascicoloWSRequest
    {
        public int? Id { get; internal set; }
        public string Utente { get; internal set; }
        public string Ruolo { get; internal set; }
        public string Classifica { get; internal set; }
        public string Anno { get; internal set; }
        public string Numero { get; internal set; }
    }
}
