using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.DTO.AllegatiDomandaOnline;
using Init.SIGePro.Manager.DTO.Common;
using Init.SIGePro.Manager.DTO.Interventi;
using Init.SIGePro.Manager.Logic.AttraversamentoAlberoInterventi.VerificaAttivazione;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sigepro.net.WebServices.WsAreaRiservata.WcfServices.Interventi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWsInterventi" in both code and config file together.
    [ServiceContract]
    public interface IWsInterventi
    {
        [OperationContract]
        NodoAlberoInterventiDto GetAlberaturaDaIdNodo(string token, int codiceIntervento);

        [OperationContract]
        List<AlberoProcDocumentiCat> GetCategorieAllegatiInterventoChePermettonoUpload(string token, string software);

        [OperationContract]
        int? GetCodiceOggettoWorkflowDaCodiceIntervento(string token, int idIntervento);

        [OperationContract]
        InterventoDto GetDettagliIntervento(string token, int codiceIntervento, AmbitoRicerca ambitoRicercaDocumenti, bool leggiNoteEstese);

        [OperationContract]
        List<AllegatoInterventoDomandaOnlineDto> GetDocumentiDaCodiceIntervento(string token, int codiceIntervento, AmbitoRicerca ambitoRicercaDocumenti);

        [OperationContract]
        int? GetIdCertificatoDiInvioDomandaDaIdIntervento(string token, int idIntervento);

        [OperationContract]
        string GetIdDrupalDaCodiceIntervento(string token, int codiceIntervento);

        [OperationContract]
        int? GetIdRiepilogoDomandaDaIdIntervento(string token, int idIntervento);

        [OperationContract]
        List<int> GetListaIdNodiPadreIntervento(string token, int codiceIntervento);

        [OperationContract]
        int GetLivelloDiAccessoIntervento(string token, int codiceIntervento);

        [OperationContract]
        List<InterventoDto> GetSottonodiIntervento(string token, string software, int idNodo, AmbitoRicerca ambitoRicerca);

        [OperationContract]
        List<InterventoDto> GetSottonodiInterventoDaIdAteco(string token, string software, int idNodoPadre, int idAteco, AmbitoRicerca ambitoRicerca);

        [OperationContract]
        NodoAlberoInterventiDto GetStrutturaAlberoInterventi(string token, string software);

        [OperationContract]
        bool HaPresentatoDomandePerIntervento(string token, int idIntervento, string codiceFiscaleRichiedente);

        [OperationContract]
        bool InterventoSupportaRedirect(string token, int idIntervento);

        [OperationContract]
        List<InterventoBreveDto> RicercaTestualeInterventi(string token, string software, string matchParziale, int matchCount, string modoRicerca, string tipoRicerca, AmbitoRicerca ambitoRicerca);

        [OperationContract]
        RisultatoVerificaAccessoIntervento VerificaAccessoIntervento(string token, LivelloAutenticazioneBOEnum livelloAutenticazione, int codiceIntervento);
    }
}
