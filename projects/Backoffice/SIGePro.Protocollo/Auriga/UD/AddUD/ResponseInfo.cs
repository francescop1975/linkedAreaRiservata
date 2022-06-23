using Init.SIGePro.Protocollo.WsDataClass;
using System;

namespace Init.SIGePro.Protocollo.Auriga.UD.AddUD
{
    public class ResponseInfo: ProxyResponseInfo
    {

        public ServiceResponseInfo ServiceResponse;

        public DatiProtocolloRes ToDatiProtocolloRes()
        {
            if(!String.IsNullOrEmpty(this.WsError)) {
                return new DatiProtocolloRes
                {
                    Errore = new ErroreProtocollo
                    {
                        Descrizione = this.WsError
                    }
                };
            }

            if (this.ServiceResponse == null || this.ServiceResponse.RegistrazioneDataUD == null)
            {
                throw new InvalidOperationException("Non è possibile richiamare il metodo ToDatiProtocolloRes senza aver prima valorizzato ServiceResponse");
            }

            return new DatiProtocolloRes
            {
                AnnoProtocollo = this.ServiceResponse.RegistrazioneDataUD[0].AnnoReg,
                DataProtocollo = null,
                IdProtocollo = this.ServiceResponse.IdUD,
                NumeroProtocollo = this.ServiceResponse.RegistrazioneDataUD[0].NumReg,
                Warning = this.WarningMessage,
                Errore = null
            };
        }
    }
}
