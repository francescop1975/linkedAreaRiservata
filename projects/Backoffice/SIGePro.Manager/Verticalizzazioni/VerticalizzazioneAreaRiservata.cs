
using System;
using System.IO;
using Init.SIGePro.Data;
using PersonalLib2.Data;

namespace Init.SIGePro.Verticalizzazioni
{
    /// <summary>
    /// Gestione delle domande online 
    /// </summary>
    public partial class VerticalizzazioneAreaRiservata : Verticalizzazione
    {
        private static class Constants
        {
            public const string UrlServiziMobile = "SERVIZI_MOBILE_URL";
            public const string AliasSportelloServiziMobile = "SERVIZI_MOBILE_ALIAS_SPORTELLO";
            public const string IdSchedaEstremiDocumento = "ID_SCHEDA_ESTREMI_DOCUMENTO";
            public const string StatoinizialeIstanza = "STATO_INIZIALE_ISTANZA";
            public const string IntestazioneCertificatoInvio = "INTESTAZIONE_CERTIFICATO_INVIO";
            public const string DimensioneMassimaAllegati = "DIMENSIONE_MASSIMA_ALLEGATI";
            public const int DimensioneMassimaAllegatiDefault = 10485760;  // 10mb
            public const string WarningDimensioneMassimaAllegatiDefault = "Attenzione! Il file allegato supera la dimensione massima consentita ({0} MB).";
            public const string WarningDimensioneMassimaAllegati = "WARNING_DIMENSIONE_MASSIMA_ALL";
            public const string DescrizioneDelegaATrasmettere = "DESCR_DELEGA_A_TRASMETTERE";
            public const string UsernameUtenteAnonimo = "USERNAME_UTENTE_ANONIMO";
            public const string PasswordUtenteAnonimo = "PASSWORD_UTENTE_ANONIMO";
            public const string CiviciNumerici = "CIVICI_NUMERICI";
            public const string EsponentiNumerici = "ESPONENTI_NUMERICI";
            public const string UrlLogo = "URL_LOGO";
            public const string NascondiNoteMovimento = "NASCONDI_NOTE_MOVIMENTO";
            public const string IntegrazioniNoUploadAllegati = "INTEGR.NO_UPLOAD_ALLEGATI";
            public const string IntegrazioniNoUploadRiepiloghiSchedeDinamiche = "INTEGR.NO_UPLOAD_RIEPILOGHI_SD";
            public const string IntegrazioniNoInserimentoNote = "INTEGR_NO_INSERIMENTO_NOTE";
            public const string IntegrazioniNoNomiAllegati = "INTEGR_NO_NOMI_ALLEGATI";
            public const string TecnicoInSoggettiCollegati = "TECNICO_IN_SOGGETTI_COLLEGATI";
            public const string FlagSchedeDinamicheFirmateInRiepilogo = "FL_SCHEDE_FIRMATE_IN_RIEPILOGO";
            public const string UrlAuthenticationOverride = "URL_AUTHENTICATION_OVERRIDE";
            public const string AidaSmartCrossLoginUrl = "ASMART_CROSS_LOGIN_URL";
            public const string AidaSmartUrlNuovaDomanda = "ASMART_URL_NUOVA_DOMANDA";
            public const string AidaSmartUrlIstanzeInSospeso = "ASMART_URL_ISTANZE_IN_SOSPESO";
            public const string VisuraNascondiStatoIstanza = "VISURA_NASCONDI_STATO_ISTANZA";
            public const string VisuraNascondiResponsabili = "VISURA_NASCONDII_RESPONSABILI";
            public const string NascondiRigeneraRiepilogo = "NASCONDI_RIGENERA_RIEPILOGO";
            public const string NomeFileRicevuta = "NOME_FILE_RICEVUTA";
            public const string NomeFileRicevutaDefault = "certificato-di-invio.pdf";
            public const string DescrizioneFileRicevuta = "DESCRIZIONE_FILE_RICEVUTA";
            public const string DescrizioneFileRicevutaDefault = "Certificato di invio";
            public const string AbilitaTemplateDomanda = "ABILITA_TEMPLATE_DOMANDA";
            public const string IstanzePresentatePosizioneArchivio = "ISTANZEPRES_POSIZIONEARCHIVIO";
            public const string QuestionarioSoddisfazioneAttivo = "QUESTIONARIO_FO_ATTIVO";
            public const string ForzaStepLocalizzazioniSit = "FORZA_STEP_LOCALIZZAZIONI_SIT";

        }

        private const string NOME_VERTICALIZZAZIONE = "AREA_RISERVATA";

        public VerticalizzazioneAreaRiservata()
        {

        }

        public VerticalizzazioneAreaRiservata(string idComuneAlias, string software) : base(idComuneAlias, NOME_VERTICALIZZAZIONE, software) { }


        public string StatoInizialeIstanza => GetString(Constants.StatoinizialeIstanza);
        public string MessaggioInvioFallito => GetString("MESSAGGIO_INVIO_FALLITO");
        public string IntestazioneDettaglioVisura => GetString("INTESTAZIONE_DETTAGLIO_VISURA");
        public string ImpostaAutoTecnico => GetString("IMPOSTA_AUTO_TECNICO");
        public string ScadCercaRichiedente => GetString("SCAD_CERCA_RICHIEDENTE");
        public string ScadCercaTecnico => GetString("SCAD_CERCA_TECNICO");
        public string ScadCercaAzienda => GetString("SCAD_CERCA_AZIENDA");
        public string ImpostaAutoRichiedente => GetString("IMPOSTA_AUTO_RICHIEDENTE");
        public string ScadCercaPartitaiva => GetString("SCAD_CERCA_PARTITAIVA");
        public string VisTCercaRichiedente => GetString("VIS_T_CERCA_RICHIEDENTE");
        public string VisTCercaTecnico => GetString("VIS_T_CERCA_TECNICO");
        public string VisTCercaAzienda => GetString("VIS_T_CERCA_AZIENDA");
        public string VisTCercaPartitaiva => GetString("VIS_T_CERCA_PARTITAIVA");
        public string VisTCercaSoggColl => GetString("VIS_T_CERCA_SOGG_COLL");
        public string VisNtCercaRichiedente => GetString("VIS_NT_CERCA_RICHIEDENTE");
        public string VisNtCercaTecnico => GetString("VIS_NT_CERCA_TECNICO");
        public string VisNtCercaAzienda => GetString("VIS_NT_CERCA_AZIENDA");
        public string VisNtCercaPartitaiva => GetString("VIS_NT_CERCA_PARTITAIVA");
        public string UrlApplicazioneFacct => GetString("URL_APPLICAZIONE_FACCT");
        public string WsNotificaistanzaUrl => GetString("WS_NOTIFICAISTANZA_URL");
        public string CodNaturaAutocertificabile => GetString("COD_NATURA_AUTOCERTIFICABILE");
        public string VerificaHashFilesFirmati => GetString("VERIFICA_HASH_FILES_FIRMATI");
        public string AtecoPrimariaIdCampo => GetString("ATECO_PRIMARIA_ID_CAMPO");
        public string VisNtCercaSoggColl => GetString("VIS_NT_CERCA_SOGG_COLL");
        public string VisFilCercaRichiedente => GetString("VIS_FIL_CERCA_RICHIEDENTE");
        public string VisFilCercaTecnico => GetString("VIS_FIL_CERCA_TECNICO");
        public string VisFilCercaAzienda => GetString("VIS_FIL_CERCA_AZIENDA");
        public string VisFilCercaPartitaiva => GetString("VIS_FIL_CERCA_PARTITAIVA");
        public string VisFilCercaSoggColl => GetString("VIS_FIL_CERCA_SOGG_COLL");
        public string ReturnToUrlPerServizi => GetString("RETURN_TO_URL_PER_SERVIZI");
        public string CentroServizi => GetString("CENTRO_SERVIZI");
        public string CentroServiziUrlBrevi => GetString("CENTRO_SERVIZI_URL_BREVI");
        public string UrlPaginaIniziale => GetString("URL_PAGINA_INIZIALE");
        public string AreaRiservataJavaAttiva => GetString("AREA_RISERVATA_JAVA_ATTIVA");
        public string UrlServiziMobile => GetString(Constants.UrlServiziMobile);
        public string AliasSportelloServiziMobile => GetString(Constants.AliasSportelloServiziMobile);
        public int? IdSchedaEstremiDocumento => GetInt(Constants.IdSchedaEstremiDocumento);
        public string IntestazioneCertificatoInvio => GetString(Constants.IntestazioneCertificatoInvio);
        public string WarningDimensioneMassimaAllegati => String.Format(GetStringOrDefault(Constants.WarningDimensioneMassimaAllegati, Constants.WarningDimensioneMassimaAllegatiDefault), (int)(DimensioneMassimaAllegati / 1048576));
        public int DimensioneMassimaAllegati => GetInt(Constants.DimensioneMassimaAllegati).GetValueOrDefault(Constants.DimensioneMassimaAllegatiDefault);
        public string DescrizioneDelegaATrasmettere => GetString(Constants.DescrizioneDelegaATrasmettere);
        public string UsernameUtenteAnonimo => GetString(Constants.UsernameUtenteAnonimo);
        public string PasswordUtenteAnonimo => GetString(Constants.PasswordUtenteAnonimo);
        public string CiviciNumerici => GetString(Constants.CiviciNumerici);
        public string EsponentiNumerici => GetString(Constants.EsponentiNumerici);
        public string UrlLogo => GetString(Constants.UrlLogo);
        public bool NascondiNoteMovimento => GetString(Constants.NascondiNoteMovimento) == "1";
        public bool IntegrazioniNoUploadAllegati => GetString(Constants.IntegrazioniNoUploadAllegati) == "1";
        public bool IntegrazioniNoUploadRiepiloghiSchedeDinamiche => GetString(Constants.IntegrazioniNoUploadRiepiloghiSchedeDinamiche) == "1";
        public bool IntegrazioniNoInserimentoNote => GetString(Constants.IntegrazioniNoInserimentoNote) == "1";
        public bool IntegrazioniNoNomiAllegati => GetString(Constants.IntegrazioniNoNomiAllegati) == "1";
        public bool TecnicoInSoggettiCollegati => GetString(Constants.TecnicoInSoggettiCollegati) == "1";
        public int FlagSchedeDinamicheFirmateInRiepilogo => GetInt(Constants.FlagSchedeDinamicheFirmateInRiepilogo) ?? 0;
        public string UrlAuthenticationOverride => GetString(Constants.UrlAuthenticationOverride);
        public string AidaSmartCrossLoginUrl => GetString(Constants.AidaSmartCrossLoginUrl);
        public string AidaSmartUrlNuovaDomanda => GetString(Constants.AidaSmartUrlNuovaDomanda);
        public string AidaSmartUrlIstanzeInSospeso => GetString(Constants.AidaSmartUrlIstanzeInSospeso);
        public bool VisuraNascondiStatoIstanza => GetString(Constants.VisuraNascondiStatoIstanza) == "1";
        public bool VisuraNascondiResponsabili => GetString(Constants.VisuraNascondiResponsabili) == "1";
        public bool NascondiRigeneraRiepilogo => GetString(Constants.NascondiRigeneraRiepilogo) == "1";
        public string NomeFileRicevuta => GetStringOrDefault(Constants.NomeFileRicevuta, Constants.NomeFileRicevutaDefault);
        public string DescrizioneFileRicevuta => GetStringOrDefault(Constants.DescrizioneFileRicevuta, Constants.DescrizioneFileRicevutaDefault);
        public bool AbilitaTemplateDomanda => GetString(Constants.AbilitaTemplateDomanda) == "1";
        public bool VisuraMostraPosizioneArchivio => GetString(Constants.IstanzePresentatePosizioneArchivio) == "1";
        public bool QuestionarioSoddisfazioneAttivo => GetString(Constants.QuestionarioSoddisfazioneAttivo) == "1";
        public bool ForzaStepLocalizzazioniSit => GetString(Constants.ForzaStepLocalizzazioniSit) == "1";
    }
}
