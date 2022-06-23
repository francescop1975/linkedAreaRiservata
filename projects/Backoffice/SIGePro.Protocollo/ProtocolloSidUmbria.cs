using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.WsDataClass;
using Init.SIGePro.Protocollo.SidUmbria;
using Init.SIGePro.Protocollo.ProtocolloFactories;
using Init.SIGePro.Protocollo.SidUmbria.Protocollazione;
using Init.SIGePro.Manager.Verticalizzazioni;
using System.Threading.Tasks;
using System.Threading;
using Init.SIGePro.Protocollo.Constants;

namespace Init.SIGePro.Protocollo
{
    public class PROTOCOLLO_SIDUMBRIA : ProtocolloBase
    {
        public override DatiProtocolloRes Protocollazione(Data.DatiProtocolloIn protoIn)
        {

            var vert = new VerticalizzazioniConfiguration(new VerticalizzazioneProtocolloSidumbria(DatiProtocollo.IdComuneAlias, DatiProtocollo.Software, DatiProtocollo.CodiceComune), _protocolloLogs);
            var datiProto = DatiProtocolloInsertFactory.Create(protoIn);

            var factoryRequest = RequestAdapterFactory.Create(datiProto, vert, this.DatiProtocollo, Provenienza);

            var request = factoryRequest.Adatta();

            var service = new ProtocolloService(vert, factoryRequest.Token, factoryRequest.Service, _protocolloLogs, _protocolloSerializer);
            service.Protocolla(request);

            identificatore response = null;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);
                _protocolloLogs.Info($"TENTATIVO N. {i + 1} DI LETTURA DEGLI ESTREMI DI PROTOCOLLO DELLA RICHIESTA {request.idRichiesta}");
                response = service.LeggiEstremi(request.idRichiesta);
                if (response != null)
                    break;
            }

            return ResponseAdapter.Adatta(response, request.idRichiesta, _protocolloLogs);
        }

        public override DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            var vert = new VerticalizzazioniConfiguration(new VerticalizzazioneProtocolloSidumbria(DatiProtocollo.IdComuneAlias, DatiProtocollo.Software, DatiProtocollo.CodiceComune), _protocolloLogs);

            var service = new ProtocolloService(vert, base.Uo, base.Ruolo, _protocolloLogs, _protocolloSerializer);
            var response = service.LeggiEstremi(idProtocollo);

            return new DatiProtocolloLetto
            {
                NumeroProtocollo = response == null ? ProtocolloSidUmbriaConstants.SeparatoreCodaNumeroProtocollo : response.numero.ToString(),
                DataProtocollo = response == null ? "" : response.data,
                AnnoProtocollo = response == null ? annoProtocollo : response.anno.ToString()
            };

        }
    }


}
