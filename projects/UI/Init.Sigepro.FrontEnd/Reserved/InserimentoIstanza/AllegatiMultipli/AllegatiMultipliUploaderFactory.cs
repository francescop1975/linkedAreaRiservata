using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Allegati;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.AllegatiMultipli
{
    public class AllegatiMultipliUploaderFactory
    {
        IAllegatiMultipliUploader _allegetiIntervento;
        IAllegatiMultipliUploader _allegetiEndo;

        public AllegatiMultipliUploaderFactory(int idDomanda, AllegatiInterventoService allegatiInterventoService, IAllegatiEndoprocedimentiService allegatiEndoService)
        {
            this._allegetiEndo = new AllegatiMultipliEndo(allegatiEndoService, idDomanda);
            this._allegetiIntervento = new AllegatiMultipliIntervento(allegatiInterventoService, idDomanda);
        }

        public IAllegatiMultipliUploader Get(ProveninzaAllegatoEnum provenienza)
        {
            if (provenienza == ProveninzaAllegatoEnum.Intervento)
            {
                return this._allegetiIntervento;
            }

            return this._allegetiEndo;
        }
    }
}