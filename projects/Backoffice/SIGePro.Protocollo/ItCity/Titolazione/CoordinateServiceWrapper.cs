using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloItCityService;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.ItCity.Titolazione
{
    public class CoordinateServiceWrapper: ServiceWrapperBase
    {
        public CoordinateServiceWrapper(string url, LoginWsInfo loginInfo, ProtocolloLogs logs, ProtocolloSerializer serializer) : base(url, loginInfo, logs, serializer)
        {

        }

        public CoordinateTitolazioneOutput GetCoordinateTitolazione( string classifica )
        {
            try
            {

                if (String.IsNullOrEmpty(classifica))
                    throw new InvalidOperationException("Non è possibile richiamare il metoto CoordinateServiceWrapper.GetCoordinateTitolazione(string classifica) senza passare il parametro classifica!");

                int count = classifica.Count(x => x == '.');
                if( count != 2 )
                    throw new InvalidOperationException("Non è possibile richiamare il metoto CoordinateServiceWrapper.GetCoordinateTitolazione(string classifica) senza passare una classifica valida!");

                var titolo = classifica.Split('.')[0];
                var classe = classifica.Split('.')[1];
                var sottoclasse = classifica.Split('.')[2];

                using (var ws = CreaWebService())
                {
                    var request = new CoordinateTitolazione
                    {
                        Titolo = titolo,
                        Classe = classe,
                        Sottoclasse = sottoclasse
                    };

                    var response = ws.GetCoordinateTitolazione(base.LoginInfo.Username, base.LoginInfo.Password, request);

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
