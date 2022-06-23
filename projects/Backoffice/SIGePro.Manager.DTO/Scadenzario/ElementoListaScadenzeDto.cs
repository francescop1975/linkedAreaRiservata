﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace Init.SIGePro.Manager.DTO.Scadenzario
{
    [DataContract]
    public class ElementoListaScadenzeDto
    {
        [DataMember]
        public int CodiceScadenza { get; set; }
        [DataMember]
        public string NumeroIstanza { get; set; }
        [DataMember]
        public string Uuid { get; set; }
        [DataMember]
        public string ModuloSoftware { get; set; }
        [DataMember]
        public string DatiRichiedente { get; set; }
        [DataMember]
        public string DatiAzienda { get; set; }
        [DataMember]
        public string DatiTecnico { get; set; }
        [DataMember]
        public string DescrStatoIstanza { get; set; }
        [DataMember]
        public string DatiMovimento { get; set; }
        [DataMember]
        public string DescrMovimentoDaFare { get; set; }
        [IgnoreDataMember]
        public string DataScadenzaStr => this.DataScadenza.HasValue ? this.DataScadenza.Value.ToString("dd/MM/yyyy") : "";

        [DataMember]
        public int CodiceIstanza { get; set; }

        [DataMember]
        public string Localizzazione { get; set; }

        [DataMember]
        public DateTime? DataScadenza { get; set; }

        [IgnoreDataMember]
        public long DataScadenzaOrderBy {
            get
            {
                if (!this.DataScadenza.HasValue)
                {
                    return 0;
                }

                return this.DataScadenza.Value.Ticks;
            }
        }
    }
}
