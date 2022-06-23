using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Verticalizzazioni
{
    public class VerticalizzazioneProtocolloStorico : Verticalizzazione, IVerticalizzazioneProtocollo
    {
        private const string NOME_VERTICALIZZAZIONE = "PROTOCOLLO_STORICO";

        private static class Constants
        {
            public const string AOO = "AOO";
            public const string CodiceRegistro = "CODICEREGISTRO";
            public const string CodiceUfficio = "CODICEUFFICIO";
            public const string DataUltimaProtocollazione = "DATAULTIMAPROTOCOLLAZIONE";
            public const string Operatore = "OPERATORE";
            public const string Ruolo = "RUOLO";
            public const string Tipoprotocollo = "TIPOPROTOCOLLO";
            public const string URLLeggiProtocollo = "URLLEGGIPROTOCOLLO";
            public const string URLLeggiAllegati = "URLLEGGIALLEGATI";
            public const string UsaNumAnnoLeggi = "USA_NUM_ANNO_LEGGI";
            public const string Utente = "UTENTE";
            public const string VisualizzaRicevutePEC = "VISUALIZZA_RICEVUTE_PEC";

        }



        public VerticalizzazioneProtocolloStorico()
        {

        }

        public VerticalizzazioneProtocolloStorico(string idComuneAlias, string software, string codiceComune) : base(idComuneAlias, NOME_VERTICALIZZAZIONE, software, codiceComune)
        {

        }

        public string AOO => GetString(Constants.AOO);
        public string CodiceRegistro => GetString(Constants.CodiceRegistro);
        public string CodiceUfficio => GetString(Constants.CodiceUfficio);
        public DateTime? DataUltimaProtocollazione => GetDate(Constants.DataUltimaProtocollazione);
        public string Operatore => GetString(Constants.Operatore);
        public string Ruolo => GetString(Constants.Ruolo);
        public string Tipoprotocollo => GetString(Constants.Tipoprotocollo);
        public string URLLeggiProtocollo => GetString(Constants.URLLeggiProtocollo);
        public string URLLeggiAllegati => GetString(Constants.URLLeggiAllegati);
        public bool UsaNumAnnoLeggi => GetBool(Constants.UsaNumAnnoLeggi);
        public string Utente => GetString(Constants.Utente);
        public bool VisualizzaRicevutePEC => GetBool(Constants.VisualizzaRicevutePEC);
        public string ProxyAddress => throw new NotImplementedException("Attualmente non gestita ma prevista a livello di interfaccia");

    }
}
