using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsVbgDatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.Manager.DTO.DatiDinamici;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici
{
    public class DatiDinamiciService : IDatiDinamiciService
    {
        WsDatiDinamiciServiceCreator _serviceCreator;

        internal DatiDinamiciService(WsDatiDinamiciServiceCreator serviceCreator)
        {
            this._serviceCreator = serviceCreator;
        }

        public IEnumerable<DecodificaDto> GetDecodificheAttive(string tabella)
        {
            using (var ws = this._serviceCreator.CreateClient())
            {
                try
                {
                    return ws.Service.GetDecodificheAttive(ws.Token, tabella);
                }
                catch (Exception)
                {
                    ws.Service.Abort();
                    throw;
                }
            }
        }

        public IEnumerable<IstanzeDyn2Dati> GetDyn2DatiByCodiceIstanza(int idDomanda)
        {
            using (var ws = this._serviceCreator.CreateClient())
            {
                try
                {
                    return ws.Service.GetDyn2DatiByCodiceIstanza(ws.Token, idDomanda);
                }
                catch (Exception)
                {
                    ws.Service.Abort();
                    throw;
                }
            }
        }

        public void RecuperaDocumentiIstanzaCollegata(int codiceIstanzaOrigine, int idDomandaDestinazione)
        {
            using (var ws = this._serviceCreator.CreateClient())
            {
                try
                {
                    ws.Service.RecuperaDocumentiIstanzaCollegata(ws.Token, codiceIstanzaOrigine, idDomandaDestinazione);
                }
                catch (Exception)
                {
                    ws.Service.Abort();
                    throw;
                }
            }
        }
    }
}
