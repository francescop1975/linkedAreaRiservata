using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.SIGePro.Manager.DTO.Endoprocedimenti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.EndoAcquisiti
{
    public interface IEndoAcquisitiService
    {
        void SalvaEndoAcquisiti(int idDomanda, IEnumerable<DatiEndoprocedimentoPresente> listaEndoAcquisiti);

        IEnumerable<string> VerificaCorrettezzaListaEndoAcquisiti(IEnumerable<DatiEndoprocedimentoPresente> listaEndoAcquisiti);

        Dictionary<int, IEnumerable<TipiTitoloDto>> TipiTitoloWhereCodiceInventarioIn(IEnumerable<int> codiciInventario);

        void AllegaFileAEndoAcquisito(int idDomanda, int codiceInventario, BinaryFile file, bool verificaFirma);

        TipiTitoloDto GetTipoTitoloById(int codiceTipoTitolo);
    }
}
