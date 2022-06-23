using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.AppLogic.GestioneTipiSoggetto;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService
{
  
    // Questa classe è utilizzata solamente per definire IClasseContestoModelloDinamico come
    // classe base di Istanze, altrimenti darebbe errore quando si va ad assegnare la classe 
    // nel Dyn2IstanzeManager
    public partial class Istanze : IClasseContestoModelloDinamico
    {
        public LivelloAccessoVisura GetLivelloAccesso(string codiceFiscale)
        {
            if (this.Richiedente != null && this.Richiedente.HaCodiceFiscale(codiceFiscale))
            {
                return LivelloAccessoVisura.Completo;
            }

            if (this.Professionista != null && this.Professionista.HaCodiceFiscale(codiceFiscale))
            {
                return LivelloAccessoVisura.Completo;
            }

            if (this.AziendaRichiedente != null && this.AziendaRichiedente.HaCodiceFiscale(codiceFiscale))
            {
                return LivelloAccessoVisura.Completo;
            }

            var livelloAccesso = this.Richiedenti
                                    .Where(x => x.Richiedente.CODICEFISCALE?.ToUpperInvariant() == codiceFiscale.ToUpperInvariant())
                                    .Max(x => x.TipoSoggetto.FlagLivelliVisuraPratica.GetValueOrDefault(0));

            return LivelloAccessoVisura.DaValoreFlag(livelloAccesso);
        }
        
        public string ToXmlModelloRiepilogo(string forzaNumeroIstanza = "")
        {
            var oldNumeroIstanza = this.NUMEROISTANZA;

            if (!String.IsNullOrEmpty(forzaNumeroIstanza))
            {
                this.NUMEROISTANZA = forzaNumeroIstanza;
            }

            try
            {
                using (var ms = new MemoryStream())
                {
                    var xs = new XmlSerializer(this.GetType());
                    xs.Serialize(ms, this);

                    string xml = Encoding.UTF8.GetString(ms.ToArray());// StreamUtils.StreamToString(ms);
                    xml = xml.Replace("xmlns=\"http://init.sigepro.it\"", "");

                    return xml;
                }
            }
            finally
            {
                this.NUMEROISTANZA = oldNumeroIstanza;
            }
        }
    }
}
