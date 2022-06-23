using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsVbgDatiDinamici;
using Init.Sigepro.FrontEnd.Infrastructure.IOC;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Formule
{
    // ATTENZIONE, questa classe è utilizzata dalle formule (principalmente nel porto di Livorno)
    // Anche se i metodi risultano essere non referenziati sono tutti utilizzati
    public class FormuleDatiDinamiciService
    {
        IDatiDinamiciService _service;

        public FormuleDatiDinamiciService()
        {
            _service = FoKernelContainer.GetService<IDatiDinamiciService>();

        }

        public IEnumerable<IstanzeDyn2Dati> GetDyn2DatiByCodiceIstanza(int idDomanda)
        {
            return _service.GetDyn2DatiByCodiceIstanza(idDomanda);
        }

        public IEnumerable<DecodificaDto> GetDecodificheAttive( string tabella)
        {
            return _service.GetDecodificheAttive(tabella);
        }

        public void RecuperaDocumentiIstanzaCollegata(int codiceIstanzaOrigine, int idDomandaDestinazione)
        {
            _service.RecuperaDocumentiIstanzaCollegata(codiceIstanzaOrigine, idDomandaDestinazione);
        }
    }
}
