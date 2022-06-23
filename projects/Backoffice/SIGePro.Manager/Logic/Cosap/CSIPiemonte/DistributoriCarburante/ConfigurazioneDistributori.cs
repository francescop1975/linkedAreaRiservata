using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class ConfigurazioneDistributori
    {
        public int Singoli { get; } = 0;
        public int Doppi { get; } = 0;
        public int Multiprodotto { get; } = 0;
        public bool Chiosco { get; } = false;
        public bool StazioneRifornimento { get; } = false;
        public bool StazioneServizio { get; } = false;

        public ConfigurazioneDistributori(int singoli, int doppi, int multiprodotto, bool chiosco, bool stazioneRifornimento, bool stazioneServizio)
        {
            this.Singoli = singoli;
            this.Doppi = doppi;
            this.Multiprodotto = multiprodotto;
            this.Chiosco = chiosco;
            this.StazioneRifornimento = stazioneRifornimento;
            this.StazioneServizio = stazioneServizio;
        }
    }
}
