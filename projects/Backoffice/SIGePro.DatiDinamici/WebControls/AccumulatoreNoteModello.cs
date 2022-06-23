using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Init.SIGePro.DatiDinamici.WebControls
{
    public class AccumulatoreNoteModello
    {
        private static class Constants
        {
            public const string ChiaveWebConfigFixStili = "AccumulatoreNoteModello.RenderFixStili";
            public const string PrefissoElementoNote = "d2-nota-compilazione";
            public const string HttpContextInstanceKey = "AccumulatoreNoteModello.ContextInstance";
            public const string CssClass = "d2-note-compilazione";
        }

        public class ElementoListaValori
        {
            public readonly string Etichetta;
            public readonly IEnumerable<string> ListaValori;

            public ElementoListaValori(string etichetta, IEnumerable<string> listaValori)
            {
                Etichetta = etichetta;
                ListaValori = listaValori;
            }
        }

        List<string> _noteCompilazione = new List<string>();
        List<string> _chiaviListaValori = new List<string>();
        List<ElementoListaValori> _listaValoriCampi = new List<ElementoListaValori>();

        int IdProssimaNota => _noteCompilazione.Count + 1;

        public string TitoloSezioneListaValori { get; private set; }
        public string TitoloSezioneNoteCompilazione { get; private set; }

        public AccumulatoreNoteModello(string titoloSezioneNoteCompilazione= "Note di compilazione", string titoloSezioneValoriCampi = "Possibili valori campi")
        {
            this.TitoloSezioneListaValori = titoloSezioneValoriCampi;
            this.TitoloSezioneNoteCompilazione = titoloSezioneNoteCompilazione;
        }

        public static void InitContextInstance()
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            HttpContext.Current.Items[Constants.HttpContextInstanceKey] = new AccumulatoreNoteModello();
        }

        public static AccumulatoreNoteModello GetContextInstance()
        {
            if (HttpContext.Current == null)
            {
                return null;
            }

            return (AccumulatoreNoteModello)HttpContext.Current.Items[Constants.HttpContextInstanceKey];
        }

        public string ToHtml()
        {
            return RenderNoteCompilazione() + "<br />" +
                    RenderListaValori() + 
                    RenderFixStili();
        }

        private string RenderFixStili()
        {
            const string css = @"<style>
ul.d2-elementi-lista-valori {
    list-style-type: none;
    /*background-color: #fafafa;
    border-left: 1px solid #ddd;*/
    padding: 5px;
    padding-bottom: 0;
    padding-left: 20px;
    margin-bottom: 2px;
}

    ul.d2-elementi-lista-valori > li {
        background-repeat: no-repeat;
        /*margin-left: -16px;
        padding-left: 16px;*/
        padding-bottom: 5px;
        /*line-height: 18px;*/
    }

        ul.d2-elementi-lista-valori > li > .nota-possibili-valori {
            font-style: italic;
            white-space: nowrap;
            font-size: 0.6em;
        }

            ul.d2-elementi-lista-valori > li > .nota-possibili-valori .id-riferimento-valori {
                font-weight: bold;
                font-size: 1.0em;
            }

        ul.d2-elementi-lista-valori > li.d2-elemento-lista-selezionato {
            /*background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAtUlEQVQ4y2P4//8/AyWYYYQZMG3atP+rV6/+T5YB+/fvB6pm+B8YGEi6ATDNIFxfX4/bAH9///9z5879j66Zj48PbkBWVhZuAwQFBcGKjh49ClYEoqWlpeGaXV1d/1++fBm3AVZWVmCFMjIy/0E2g2iYZnt7+/+fPn36jzcWQIpgGpAxSJyoaHz//v1/JycnFM2Ojo5YbcYZC8+fP/9vaWkJ1gyiQXySE9KTJ0/AzgbRgz8vAACU9sOwJSGWUAAAAABJRU5ErkJggg==');
            font-weight: bold;*/
        }
</style>";

            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings[Constants.ChiaveWebConfigFixStili]))
            {
                return "";
            }

            return css;
        }

        private string RenderListaValori()
        {
            if (_chiaviListaValori.Count == 0)
            {
                return String.Empty;
            }

            using (var stringWriter = new StringWriter())
            using (var tw = new HtmlTextWriter(stringWriter))
            {
                var rootElement = new HtmlGenericControl("div");
                rootElement.Attributes.Add("class", Constants.CssClass);
                var titoloSezione = new HtmlGenericControl("h2");

                titoloSezione.InnerHtml = this.TitoloSezioneListaValori;

                rootElement.Controls.Add(titoloSezione);

                var indiceNota = 1;

                //foreach (var campo in this._listaValoriCampo.Keys)
                for (int i = 0; i < _chiaviListaValori.Count; i++)
                {
                    var campo = _listaValoriCampi[i];
                    var parent = new HtmlGenericControl("div");
                    var titoloCampo = new HtmlGenericControl("h3");
                    var ul = new HtmlGenericControl("ul");

                    parent.Controls.Add(titoloCampo);
                    parent.Controls.Add(ul);

                    titoloCampo.InnerHtml = $"<span class='riferimento-valori'>V{indiceNota}</span>: {campo.Etichetta}";

                    indiceNota++;

                    foreach (var valore in campo.ListaValori)
                    {
                        var li = new HtmlGenericControl("li");

                        li.InnerHtml = $"<span>{valore}</span>";
                        //li.Attributes.Add("id", Constants.PrefissoElementoNote + (i + 1).ToString());

                        ul.Controls.Add(li);
                    }

                    rootElement.Controls.Add(parent);
                }
                

                rootElement.RenderControl(tw);

                return stringWriter.ToString();
            }
        }

        private string RenderNoteCompilazione()
        {
            using (var stringWriter = new StringWriter())
            using (var tw = new HtmlTextWriter(stringWriter))
            {
                var rootElement = new HtmlGenericControl("div");
                rootElement.Attributes.Add("class", Constants.CssClass);
                var titoloSezione = new HtmlGenericControl("h2");

                titoloSezione.InnerHtml = this.TitoloSezioneNoteCompilazione;

                rootElement.Controls.Add(titoloSezione);

                var ol = new HtmlGenericControl("ul");

                for (int i = 0; i < _noteCompilazione.Count; i++)
                {
                    var nota = _noteCompilazione[i];
                    var li = new HtmlGenericControl("li");

                    li.InnerHtml = $"<span><span class='riferimento-nota'>N{(i+1)}</span>:<br/> {nota}</span>";
                    li.Attributes.Add("id", Constants.PrefissoElementoNote + (i + 1).ToString());

                    ol.Controls.Add(li);
                }

                rootElement.Controls.Add(ol);

                rootElement.RenderControl(tw);

                return stringWriter.ToString();
            }
        }

        //public void AnalizzaControllo(Control campoRoot)
        //{
        //    foreach (var controllo in campoRoot.Controls.Cast<Control>())
        //    {
        //        if (controllo is IDatiDinamiciControl)
        //        {
        //            var campo = (IDatiDinamiciControl)controllo;
        //            var note = campo.Note;

        //            if (!String.IsNullOrEmpty(note))
        //            {
        //                campo.IdRiferimentoNote = IdProssimaNota;

        //                _note.Add(note);
        //            }
        //        }
        //        else
        //        {
        //            foreach (var figlio in controllo.Controls.Cast<Control>())
        //            {
        //                AnalizzaControllo(figlio);
        //            }
        //        }
        //    }
        //}

        public void ValutaNote(CampoDinamicoBase campo)
        {
            if (!String.IsNullOrEmpty(campo.Descrizione?.Trim()))
            {
                _noteCompilazione.Add(campo.Descrizione);

                campo.IdRiferimentoNote = _noteCompilazione.Count;
            }
        }
        /*
        internal void EstendiNota(int idx, string noteEstese)
        {
            if (idx < 0 || idx >= this._noteCompilazione.Count)
            {
                return;
            }

            this._noteCompilazione[idx] += noteEstese;
        }
        */

        public int AggiungiValoriCampo(string nomeCampo, string etichetta, IEnumerable<string> listaValori)
        {
            var idx = this._chiaviListaValori.IndexOf(nomeCampo);

            if (idx != -1)
            {
                return idx + 1;
            }

            idx = this._chiaviListaValori.Count;
            this._chiaviListaValori.Add(nomeCampo);
            this._listaValoriCampi.Insert(idx, new ElementoListaValori(etichetta, listaValori));

            return idx + 1;
        }
    }
}
