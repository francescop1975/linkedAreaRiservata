using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloItCityService;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Init.SIGePro.Protocollo.ProtocolloItCityService.RecapitoInterno;

namespace Init.SIGePro.Protocollo.ItCity.RicercaUO
{
    public class RicercaUOServiceWrapper: ServiceWrapperBase
    {
        public RicercaUOServiceWrapper(string url, LoginWsInfo loginInfo, ProtocolloLogs logs, ProtocolloSerializer serializer) : base(url, loginInfo, logs, serializer)
        {

        }

        public RicercaUOOutput CercaUOPerChiaveAletrnativa( ChiaveAlternativa chiaveAlternativa )
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    var request = new RecapitoInterno
                    {
                        ChiaveComposta = chiaveAlternativa
                    };

                    var response = ws.RicercaUO(base.LoginInfo.Username, base.LoginInfo.Password, base.LoginInfo.Identificativo, request);

                    if (response.ExitCode != 0)
                    {
                        throw new Exception(response.ExitMessage);
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"RICERCA DELLA UO FALLITA: {ex.Message}", ex);
            }
        }
    }
}
