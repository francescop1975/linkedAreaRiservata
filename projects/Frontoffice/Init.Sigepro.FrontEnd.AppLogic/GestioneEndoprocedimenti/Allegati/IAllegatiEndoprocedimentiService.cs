namespace Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Allegati
{
    using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDocumenti;
    using Init.SIGePro.Manager.DTO.Endoprocedimenti;
    using System.Collections.Generic;

    public interface IAllegatiEndoprocedimentiService
    {
        DocumentoDomanda GetById(int idDomanda, int idDocumento);
        void AggiungiAllegatoAEndo(int idDomanda, int idAllegato, Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.BinaryFile file);
        void AggiungiAllegatoLibero(int idDomanda, int codiceEndo, string descrizione, Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.BinaryFile file, bool verificaFirma);
        void EliminaOggettoUtente(int idDomanda, int idAllegato);
        void SincronizzaAllegati(int idDomanda);
        IEnumerable<AllegatiPerEndoprocedimentoDto> GetDatiProcedimenti(List<int> codiciEndoSelezionati);
    }
}
