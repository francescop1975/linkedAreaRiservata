using Init.SIGePro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Allegati
{
    public class AllegatoAcaris
    {
        private class Constants
        {
            public static string MetadatoFirmaDigitalePresente = "FIRMA_DIGITALE_PRESENTE";
        }

        public int CodiceOggetto { get; internal set; }
        public string NomeFile { get; internal set; }
        public string Id { get; internal set; }
        public byte[] Content { get; internal set; }
        public string MimeType { get; internal set; }
        public int ContentLenght { get; internal set; }
        public string Descrizione { get; internal set; }

        public IEnumerable<OggettiMetadati> Metadati { get; internal set; }
        public bool IsFirmato
        {
            get 
            {
                if (this.Metadati == null)
                {
                    return false;
                }

                return this.Metadati.Where(x => x.Chiave == Constants.MetadatoFirmaDigitalePresente && x.Valore == "S").Any();
            }
        }

        public AllegatoAcaris()
        {

        }

        public AllegatoAcaris(ProtocolloAllegati allegato, IEnumerable<OggettiMetadati> metadati)
        {
            if (allegato == null) 
            {
                return;
            }

            this.CodiceOggetto = Convert.ToInt32(allegato.CODICEOGGETTO);
            this.NomeFile = allegato.NOMEFILE;
            this.Content = allegato.OGGETTO;
            this.MimeType = this.DecodificaMime(allegato.MimeType);
            this.ContentLenght = this.Content == null ? 0 : this.Content.Length;
            this.Descrizione = allegato.Descrizione;
            this.Metadati = metadati;
        }

        private string DecodificaMime(string mimeType) 
        {
            if (String.IsNullOrEmpty(mimeType))
            {
                return mimeType;
            }

            if (mimeType.ToUpper().StartsWith("X-")) 
            {
                return mimeType.Substring(2);
            }

            return mimeType;
        }

    }
}
