using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.GestioneQrCode
{
    public interface ISostituzioneSegnapostoQrCode
    {
        string ProcessaCertificato(int codiceIstanza, string htmlCertificato);
    }
}
