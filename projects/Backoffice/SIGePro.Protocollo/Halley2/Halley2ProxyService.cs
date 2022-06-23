using Init.SIGePro.Protocollo.Halley2.Client;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Halley2
{
    public class Halley2ProxyService
    {
        private IProtocolloSerializer _serializer;
        IProtocollazioneResolver _resolver;
        private ProtocolloHalley2Client _protocolloHalley2Client;

        public Halley2ProxyService(IProtocolloSerializer serializer,IProtocollazioneResolver resolver)
        {
            this._serializer = serializer;
            this._resolver = resolver;
            this._protocolloHalley2Client = new ProtocolloHalley2Client(resolver.ProtocollazioneUrl);

        }

        public ProtocolloHalley2Response Protocolla(ProtocolloHalley2Request request)
        {
            try
            {
                using (var ws = this._protocolloHalley2Client.CreaWebService())
                {

                    var response = ws.NuovoProtocollo(
                        this._resolver.UserName,
                        this._resolver.Password,
                        request.Segnatura,
                        request
                            .Allegati?
                            .Where(x => x.Principale)?
                            .Select(x => x.File)?
                            .FirstOrDefault(),
                        request
                            .Allegati?
                            .Where(x => !x.Principale)?
                            .Select(x => x.File)?
                            .ToArray());

                    return new ProtocolloHalley2Response(response);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
