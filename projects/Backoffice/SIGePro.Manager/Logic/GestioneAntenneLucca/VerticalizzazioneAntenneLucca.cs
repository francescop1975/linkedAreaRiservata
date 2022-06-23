using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneAntenneLucca
{
    public class VerticalizzazioneAntenneLucca : Verticalizzazione
    {
        public static class Constants
        {
            public const string NomeVerticalizzazione = "ANTENNE_LUCCA";
            public const string ParametroNomeCampoIdAntenna = "NOME_CAMPO_ID_ANTENNA";
        }

        // Solo ad uso interno
        public VerticalizzazioneAntenneLucca(){}

        public VerticalizzazioneAntenneLucca(string idComuneAlias, string software) : base(idComuneAlias, Constants.NomeVerticalizzazione, software) { }

        public string NomeCampoIdAntenna => GetString(Constants.ParametroNomeCampoIdAntenna);
    }
}
