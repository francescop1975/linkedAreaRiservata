using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.JIride.Fascicolazione.Lettura;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Fascicolazione
{
    public class FascicolazioneService
    {
        private ProtocolloLogs _protocolloLogs;
        private ProtocolloSerializer _protocolloSerializer;

        public FascicolazioneService(ProtocolloLogs protocolloLogs, ProtocolloSerializer protocolloSerializer)
        {
            this._protocolloLogs = protocolloLogs;
            this._protocolloSerializer = protocolloSerializer;
        }

        public FascicoloOutXml FascicoloNuovo(FascicoloNuovoRequest request)
        {
            return new FascicolazioneClient(this._protocolloLogs, this._protocolloSerializer, request.Url).CreaFascicolo(request.Request, request.CodiceAmministrazione, request.Aoo);
        }

        public FascicoloOutXml LeggiFascicolo( LeggiFascicoloRequest request ) 
        {
            return new FascicolazioneClient(this._protocolloLogs, this._protocolloSerializer, request.Url).LeggiFascicolo(request.Request, request.CodiceAmministrazione, request.Aoo);
        }
    }
}
