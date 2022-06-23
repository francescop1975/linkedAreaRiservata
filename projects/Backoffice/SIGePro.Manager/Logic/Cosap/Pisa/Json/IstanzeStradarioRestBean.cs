using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.Json
{
    public class IstanzeStradarioRestBean
    {
        [JsonProperty("id")]
        public Int32? Id;
        [JsonProperty("codice_istanza")]
        public Int32? CodiceIstanza;
        [JsonProperty("tipi_localizzazioni_id")]
        public Int32? TipiLocalizzazioniId;
        [JsonProperty("codice_stradario")]
        public Int32? CodiceStradario;
        [JsonProperty("civico")]
        public String Civico;
        [JsonProperty("colore")]
        public String Colore;
        [JsonProperty("note")]
        public String Note;
        [JsonProperty("primario")]
        public Boolean Primario;
        [JsonProperty("frazione")]
        public String Frazione;
        [JsonProperty("circoscrizione")]
        public String Circoscrizione;
        [JsonProperty("cap")]
        public String Cap;
        [JsonProperty("km")]
        public String Km;
        [JsonProperty("esponente")]
        public String Esponente;
        [JsonProperty("scala")]
        public String Scala;
        [JsonProperty("interno")]
        public String Interno;
        [JsonProperty("esponente_interno")]
        public String EsponenteInterno;
        [JsonProperty("fabbricato")]
        public String Fabbricato;
        [JsonProperty("piano")]
        public String Piano;
        [JsonProperty("quartiere")]
        public String Quartiere;
        [JsonProperty("codice_civico")]
        public String CodiceCivico;
        [JsonProperty("uuid")]
        public String Uuid;
        [JsonProperty("latitudine")]
        public String Latitudine;
        [JsonProperty("longitudine")]
        public String Longitudine;
        [JsonProperty("accesso_tipo")]
        public String AccessoTipo;
        [JsonProperty("accesso_numero")]
        public String AccessoNumero;
        [JsonProperty("accesso_descrizione")]
        public String AccessoDescrizione;
        [JsonProperty("valido")]
        public Boolean Valido;
        [JsonProperty("id_punto_sit")]
        public String IdPuntoSit;
        [JsonProperty("id_comune")]
        public String IdComune;
    }
}
