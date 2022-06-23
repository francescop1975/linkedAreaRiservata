using Init.SIGePro.Protocollo.Acaris.Document.CSIPiemonte;
using Init.SIGePro.Protocollo.AcarisDocumentServicePort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Document
{
    public interface IDocument
    {
        InfoRichiestaCreazione Documento { get; }

        List<Annotazione> Annotazioni { get; }

    }
}
