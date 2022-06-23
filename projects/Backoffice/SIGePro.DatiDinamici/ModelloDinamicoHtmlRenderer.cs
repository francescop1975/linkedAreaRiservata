using Init.SIGePro.DatiDinamici.WebControls;
using Init.SIGePro.DatiDinamici.WebControls.MaschereCampiNonVisibili;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Init.SIGePro.DatiDinamici
{
    public class ModelloDinamicoHtmlRenderer
    {
        ModelloDinamicoBase _modelloDinamico;

        public ModelloDinamicoHtmlRenderer(ModelloDinamicoBase modelloDinamico)
        {
            this._modelloDinamico = modelloDinamico;
        }

        public string GetHtml(ICampiNonVisibili campiNonVisibili = null) {

            if(campiNonVisibili == null)
                campiNonVisibili = CampiNonVisibili.TuttiICampiVisibili;

            var renderer = new ModelloDinamicoRenderer
            {
                ID = "renderer",
                ReadOnly = true,
                DataSource = this._modelloDinamico,
                CampiNascosti = campiNonVisibili
            };

            renderer.DataBind();

            var stringWriter = new StringWriter();
            var tw = new HtmlTextWriter(stringWriter);

            renderer.RenderControl(tw);
            renderer.CleanupSession();  // Necessario chiamarlo per evitare che vengano fatti persistere in session
                                        // alcuni dati (principalmente la visibilità campi ma potrebbero essercene altri)

            return stringWriter.ToString();
        }
    }
}
