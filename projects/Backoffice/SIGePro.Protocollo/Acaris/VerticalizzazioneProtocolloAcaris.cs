using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris
{
    public class VerticalizzazioneProtocolloAcaris : Verticalizzazione
    {
        private const string NOME_VERTICALIZZAZIONE = "PROTOCOLLO_ACARIS";

        public string Url
        {
            get { return GetString("URL"); }
            set { SetString("URL", value); }
        }

        public string AppKey 
        {
            get { return GetString("APPKEY"); }
            set { SetString("APPKEY", value); }
        }

        public string Repository 
        {
            get { return GetString("REPOSITORY"); }
            set { SetString("REPOSITORY", value); }
        }
        public int? IdAOO 
        {
            get { return GetInt("IDAOO"); }
            set { SetInt("IDAOO", value); }
        }
        public int? AnniConservazioneCorrente 
        {
            get { return GetInt("ANNI_CONSERVAZIONE_CORRENTE"); }
            set { SetInt("ANNI_CONSERVAZIONE_CORRENTE", value); }
        }
        public int? AnniConservazioneGenerale 
        {
            get { return GetInt("ANNI_CONSERVAZIONE_GENERALE"); }
            set { SetInt("ANNI_CONSERVAZIONE_GENERALE", value); }
        }
        public int? IdGradoVitalita 
        {
            get { return GetInt("GRADO_VITALITA"); }
            set { SetInt("GRADO_VITALITA", value); }
        }

        public int? IdTitolario
        {
            get { return GetInt("TITOLARIO"); }
            set { SetInt("TITOLARIO", value); }
        }


        public string SerieFascicoli
        {
            get { return GetString("SERIEFASCICOLI"); }
            set { SetString("SERIEFASCICOLI", value); }
        }

        public VerticalizzazioneProtocolloAcaris(string idComuneAlias, string software, string codiceComune) : base(idComuneAlias,NOME_VERTICALIZZAZIONE,software,codiceComune)
        {

        }
    }
}
