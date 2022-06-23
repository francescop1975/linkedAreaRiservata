using Init.SIGePro.Protocollo.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.ProtocolloServices.MailTipo
{
    public interface IMailTipoService
    {
        ProtocolloLogs Logger { get; }

        MailTipoType GetMailTipo();
    }
}
