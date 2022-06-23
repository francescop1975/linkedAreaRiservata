﻿using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.AmbitoRicercaIntervento;
using Init.Sigepro.FrontEnd.AppLogic.WsInterventi;
using Init.SIGePro.Manager.DTO.Interventi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi
{
    public interface IInterventiRepository
    {
        string EstraiDescrizioneEstesa(int idIntervento);
        NodoAlberoInterventiDto GetAlberaturaNodoDaId(string aliasComune, int idNodo);
        NodoAlberoInterventiDto GetAlberoInterventi(string aliasComune, string software);
        InterventoDto GetDettagliIntervento(string aliasComune, int idNodo, IAmbitoRicercaIntervento ambitoRicercaDocumenti, bool leggiNoteEstese);
        IEnumerable<InterventoDto> GetSottonodi(string aliasComune, string software, int idnodo, IAmbitoRicercaIntervento ambitoRicerca);
        IEnumerable<InterventoDto> GetSottonodiDaIdAteco(string aliasComune, string software, int idNodoPadre, int idAteco, IAmbitoRicercaIntervento ambitoRicerca);
        IEnumerable<InterventoBreveDto> RicercaTestuale(string aliasComune, string software, string matchParziale, int matchCount, string modoRicerca, string tipoRicerca, IAmbitoRicercaIntervento ambitoRicerca);
        List<int> GetIdNodiPadre(string aliasComune, int idNodo);
        int? GetCodiceOggettoCertificatoDiInvioDaIdIntervento(int idIntervento);
        int? GetidDocumentoRiepilogoDaIdIntervento(int idIntervento);
        int? GetCodiceOggettoWorkflow(int idIntervento);
        bool EsistonoVociAttivabiliTramiteAreaRiservata(string idComune, string software);
        RisultatoVerificaAccessoIntervento VerificaAccessoIntervento(int idIntervento, LivelloAutenticazioneEnum livelloAutenticazione, bool utenteTester);
        string GetNomeLivelloAutenticazionePerInterventi(int idIntervento);
        bool InterventoSupportaRedirect(int codiceIntervento);
        bool HaPresentatoDomandePerIntervento(int idIntervento, string codiceFiscale);
    }
}
