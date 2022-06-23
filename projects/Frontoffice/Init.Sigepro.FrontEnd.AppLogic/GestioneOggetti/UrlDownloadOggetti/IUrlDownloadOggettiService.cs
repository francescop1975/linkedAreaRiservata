using Init.Sigepro.FrontEnd.AppLogic.GestioneConversioneFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti
{
    public interface IUrlDownloadOggettiService
    {
        string GetUrlDownload(int codiceOggetto);

        string GetUrlDownloadPdfCompilabile(int codiceOggetto, int idDomanda);

        string GetUrlDownloadFirmato(int codiceOggetto);

        string GetUrlDownloadConvertito(int codiceOggetto, FormatoConversioneEnum formato);

        string GetUrlDownloadCompilato(int codiceOggetto, int idDomanda, FormatoConversioneEnum formato, int? idAllegatoDomandaMd5 = null);

        CodiceOggettoDownload GetCodiceOggettoDaEncryptedString(string encryptedString);
    }
}
