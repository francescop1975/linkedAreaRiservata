using Init.SIGePro.Manager.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class CalcolaImportiRequest
    {
        public class ConfigurazioneDistributori
        {
            public int Singoli { get; } = 0;
            public int Doppi { get; } = 0;
            public int Multiprodotto { get; } = 0;

            public ConfigurazioneDistributori(int singoli, int doppi, int multiprodotto)
            {
                Singoli = singoli;
                Doppi = doppi;
                Multiprodotto = multiprodotto;
            }
        }

        public string TipologiaDistributore { get; }
        public ConfigurazioneDistributori Distributori { get; }
        public IEnumerable<string> ServiziAccessoriAttivi { get; } = new string[0];
        public double SommaMqPassiCarrabili { get;}
        public CategorieStradeDistributori Categorie { get; }



        public CalcolaImportiRequest(string tipologiaDistributore, string categoriaStradaDistributore, string categoriaStradaCosap, int distributoriSingoli = 0, int distributoriDoppi = 0, int distributoriMultiprodotto = 0, 
                                    double mqPassiCarrabili = 0.0d, IEnumerable<string> serviziAccessori = null)
        {
            if (string.IsNullOrEmpty(tipologiaDistributore))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(tipologiaDistributore));
            }

            if (string.IsNullOrEmpty(categoriaStradaDistributore))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(categoriaStradaDistributore));
            }

            if (string.IsNullOrEmpty(categoriaStradaCosap))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(categoriaStradaCosap));
            }

            if (distributoriSingoli < 0)
            {
                throw new ArgumentException($"Valore non valido: {distributoriSingoli}", nameof(distributoriSingoli));
            }

            if (distributoriDoppi < 0)
            {
                throw new ArgumentException($"Valore non valido: {distributoriDoppi}", nameof(distributoriDoppi));
            }

            if (distributoriMultiprodotto < 0)
            {
                throw new ArgumentException($"Valore non valido: {distributoriMultiprodotto}", nameof(distributoriMultiprodotto));
            }

            if (mqPassiCarrabili < 0.0d)
            {
                throw new ArgumentException($"Valore non valido: {mqPassiCarrabili}", nameof(mqPassiCarrabili));
            }

            this.TipologiaDistributore = tipologiaDistributore;
            this.Categorie = new CategorieStradeDistributori(categoriaStradaDistributore, categoriaStradaCosap);
            this.Distributori = new ConfigurazioneDistributori(distributoriSingoli, distributoriDoppi, distributoriMultiprodotto);
            this.ServiziAccessoriAttivi = serviziAccessori ?? Enumerable.Empty<string>();
            this.SommaMqPassiCarrabili = mqPassiCarrabili;
        }


    }
}