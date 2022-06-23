using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.WsVbgDatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici
{
    public interface IDatiDinamiciService
    {
        IEnumerable<IstanzeDyn2Dati> GetDyn2DatiByCodiceIstanza(int idDomanda);
        IEnumerable<DecodificaDto> GetDecodificheAttive(string tabella);
        void RecuperaDocumentiIstanzaCollegata(int codiceIstanzaOrigine, int idDomandaDestinazione);
    }
}
