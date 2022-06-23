using Init.SIGePro.Protocollo.DocArea.Implementazioni.ADS.LeggiProtocollo;
using Init.SIGePro.Protocollo.ProtocolloEnumerators;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.DocArea.LeggiProtocollo
{
    public class LeggiAllegatoFactory
    {
        private ProtocolloEnum.FornitoreDocAreaEnum _fornitore;
        private string _endPointAddress;
        private string _userName;
        private string _password;
        private IProtocolloSerializer _serializer;

        public LeggiAllegatoFactory(ProtocolloEnum.FornitoreDocAreaEnum fornitore, string endPointAddress, string userName, string password, IProtocolloSerializer serializer)
        {
            if (string.IsNullOrEmpty(endPointAddress))
            {
                throw new ArgumentException($"'{nameof(endPointAddress)}' non può essere Null o vuoto", nameof(endPointAddress));
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' non può essere Null o vuoto", nameof(userName));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' non può essere Null o vuoto", nameof(password));
            }

            this._fornitore = fornitore;
            this._endPointAddress = endPointAddress;
            this._userName = userName;
            this._password = password;
            this._serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }


        public ILeggiAllegatoService GetService()
        {
            switch (this._fornitore)
            {
                case ProtocolloEnum.FornitoreDocAreaEnum.ADS:
                    return new LeggiAllegatoService(this._endPointAddress, this._userName, this._password, this._serializer);
                case ProtocolloEnum.FornitoreDocAreaEnum.DATAMANAGEMENT:
                case ProtocolloEnum.FornitoreDocAreaEnum.DATAGRAPH:
                case ProtocolloEnum.FornitoreDocAreaEnum.MAGGIOLI:
                case ProtocolloEnum.FornitoreDocAreaEnum.NON_DEFINITO:
                default:
                    throw new NotImplementedException($"Il metodo per il download di un allegato della protocollazione non è implementato dal fornitore {this._fornitore.ToString()} ");
            }
        }
    }
}
