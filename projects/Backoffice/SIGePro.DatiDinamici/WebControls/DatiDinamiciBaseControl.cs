using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using Init.SIGePro.DatiDinamici.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("Init.SIGePro.DatiDinamici.WebControls.Js.D2FocusManager.js", "text/javascript")]
[assembly: WebResource("Init.SIGePro.DatiDinamici.WebControls.Js.D2PannelloErrori.js", "text/javascript")]
[assembly: WebResource("Init.SIGePro.DatiDinamici.WebControls.Js.GetterSetterDatiDinamici.js", "text/javascript")]
[assembly: WebResource("Init.SIGePro.DatiDinamici.WebControls.Js.DatiDinamiciExtender.js", "text/javascript")]
[assembly: WebResource("Init.SIGePro.DatiDinamici.WebControls.Js.DescrizioneControllo.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("Init.SIGePro.DatiDinamici.WebControls.Js.jquery.uploadDatiDinamici.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("Init.SIGePro.DatiDinamici.WebControls.Js.jQuery.searchDatiDinamici.js", "text/javascript")]

[assembly: WebResource("Init.SIGePro.DatiDinamici.WebControls.help-icon.png", "image/png")]

namespace Init.SIGePro.DatiDinamici.WebControls
{
    [ControlValueProperty("Valore")]
    public abstract partial class DatiDinamiciBaseControl<T> : WebControl, IDatiDinamiciControl, INamingContainer where T : WebControl, new()
    {

        private static class Constants
        {
            public const string NomeCampoAttribute = "data-nome-campo";
            public const string NotificaValoreDecodificatoAttribute = "data-notifica-valore-decodificato";
            public const string EventoModificaAttribute = "data-evento-modifica";
        }

        #region properties

        public int Indice { get; set; }
        public int NumeroRiga { get; set; }
        public abstract string Valore { get; set; }

        //public int? IdRiferimentoNote {
        //    get;
        //    set;
        //}

        protected IDyn2DataAccessFactory DataAccessFactory { get; }
        protected bool IgnoraRegistrazioneJavascript { get; set; }
        protected T InnerControl { get; set; }

        // private Image ImgDescrizione { get; set; }
        private Literal _testoDescrizione { get; set; }
        private Panel _pnlDescrizione { get; set; }


        public string Descrizione
        {
            get { return _testoDescrizione.Text; }
            private set { _testoDescrizione.Text = value; }
        }

        public string Note => _pnlDescrizione.Controls.Count > 0 ? ((Literal)_pnlDescrizione.Controls[0]).Text : String.Empty;

        public string IdComune
        {
            get { return HttpContext.Current.Items["IdComune"].ToString(); }
        }
        public string Software
        {
            get { return HttpContext.Current.Items["Software"].ToString(); }
        }

        public int IdCampoCollegato
        {
            get { object o = ViewState["IdCampoCollegato"]; return o == null ? -1 : (int)o; }
            private set { ViewState["IdCampoCollegato"] = value; }
        }

        public bool RichiedeNotificaSuModificaValoreDecodificato
        {
            get { object o = ViewState["RichiedeNotificaSuModificaValoreDecodificato"]; return o == null ? false : (bool)o; }
            set { ViewState["RichiedeNotificaSuModificaValoreDecodificato"] = value; }
        }

        public string NomeCampo
        {
            get { object o = ViewState["NomeCampo"]; return o == null ? String.Empty : o.ToString(); }
            set { ViewState["NomeCampo"] = value.ToString(); }
        }

        public string Etichetta
        {
            get { object o = ViewState["Etichetta"]; return o == null ? String.Empty : o.ToString(); }
            set { ViewState["Etichetta"] = value ?? ""; }
        }

        #endregion

        #region Costruttori

        public DatiDinamiciBaseControl(CampoDinamicoBase campo) : this()
        {
            IdCampoCollegato = campo.Id;
            Descrizione = campo.Descrizione;
            DataAccessFactory = campo.DataAccessFactory;
            NomeCampo = new ControlSafeNomeCampo(campo.NomeCampo).ToString();
            Etichetta = campo.Etichetta;

            InizializzaPropertiesDaCampoDinamico(campo);
        }

        protected DatiDinamiciBaseControl()
        {
            IgnoraRegistrazioneJavascript = false;

            InnerControl = new T
            {
                ID = "_InnerControl"
            };

            /*
            ImgDescrizione = new Image
            {
                ID = "_imgDescrizione"
            };


            ImgDescrizione.Style.Add("width", "16px");
            ImgDescrizione.Style.Add("height", "16px");
            ImgDescrizione.Style.Add("vertical-align", "sub");
            ImgDescrizione.Style.Add("margin-left", "2px");
            */

            _testoDescrizione = new Literal();

            _pnlDescrizione = new Panel
            {
                ID = "_descrizione",
                CssClass = "descrizioneCampoDinamico"
            };

            _pnlDescrizione.Controls.Add(_testoDescrizione);

            Controls.Add(InnerControl);
            //Controls.Add(ImgDescrizione);
            Controls.Add(_pnlDescrizione);
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string jsKey = "RegistrazioneScript";

            if (Settings.Default.UsaJavascriptEmbedded && !Page.ClientScript.IsClientScriptIncludeRegistered(typeof(IDatiDinamiciControl), jsKey + "1"))
            {
                // Referenzio gli script javascript
                var listaFilesJs = new string[]{
                    "Init.SIGePro.DatiDinamici.WebControls.Js.D2FocusManager.js",
                    "Init.SIGePro.DatiDinamici.WebControls.Js.D2PannelloErrori.js",
                    "Init.SIGePro.DatiDinamici.WebControls.Js.GetterSetterDatiDinamici.js",
                    "Init.SIGePro.DatiDinamici.WebControls.Js.DatiDinamiciExtender.js",
                    "Init.SIGePro.DatiDinamici.WebControls.Js.DescrizioneControllo.js",
                    "Init.SIGePro.DatiDinamici.WebControls.Js.jQuery.searchDatiDinamici.js",
                    "Init.SIGePro.DatiDinamici.WebControls.Js.jquery.uploadDatiDinamici.js",
                };

                for (var i = 0; i < listaFilesJs.Length; i++)
                {
                    Page.ClientScript.RegisterClientScriptInclude(
                       typeof(IDatiDinamiciControl), jsKey + i.ToString(),
                       Page.ClientScript.GetWebResourceUrl(typeof(IDatiDinamiciControl),
                       listaFilesJs[i]));
                }

            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // ImgDescrizione.Visible = !String.IsNullOrEmpty(Descrizione);
            // _pnlDescrizione.Visible = ImgDescrizione.Visible;

            InnerControl.CssClass = "d2Control " + GetNomeTipoControllo() + " " + GetExtraCssClasses();
            InnerControl.Attributes.Add("data-d2id", IdCampoCollegato.ToString());
            InnerControl.Attributes.Add("data-d2tipo", GetNomeTipoControllo());
            InnerControl.Attributes.Add("data-d2indice", Indice.ToString());
            InnerControl.Attributes.Add(Constants.EventoModificaAttribute, GetNomeEventoModifica());
            InnerControl.Attributes.Add("data-d2clientid", ClientID);
            InnerControl.Attributes.Add(Constants.NotificaValoreDecodificatoAttribute, RichiedeNotificaSuModificaValoreDecodificato.ToString());
            InnerControl.Attributes.Add(Constants.NomeCampoAttribute, NomeCampo);
            /*
            if (ImgDescrizione.Visible)
            {
                ImgDescrizione.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), @"Init.SIGePro.DatiDinamici.WebControls.help-icon.png");
                ImgDescrizione.CssClass = "helpIcon";
            }
            */
            base.OnPreRender(e);
        }

        protected virtual string GetExtraCssClasses()
        {
            return string.Empty;
        }

        /// <summary>
        /// Restituisce il nome del tipo di controllo (ad esempio text, intTextBox, etc)
        /// </summary>
        /// <returns></returns>
        protected abstract string GetNomeTipoControllo();


        /// <summary>
        /// Restituisce il nome dell'evento che causa la modifica del campo
        /// </summary>
        /// <returns></returns>
        protected virtual string GetNomeEventoModifica()
        {
            return "blur";
        }

        protected void NascondiIconaHelp()
        {
            // ImgDescrizione.Visible = false;
            _pnlDescrizione.Visible = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //if (this.IdRiferimentoNote.HasValue)
            //{
            //    var hrefNote = new HtmlGenericControl("a");

            //    hrefNote.Attributes.Add("href", $"#{AccumulatoreNoteModello.Constants.PrefissoElementoNote}{this.IdRiferimentoNote.ToString()}");
            //    hrefNote.Attributes.Add("class", "d2-href-note");
            //    hrefNote.InnerText = $"({this.IdRiferimentoNote})";

            //    this.Controls.Add(hrefNote);
            //}
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-group feedback-container");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            var haDescrizione = !String.IsNullOrEmpty(Descrizione);

            if (haDescrizione)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "d2-input-group");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

            }

            base.RenderChildren(writer);

            if (haDescrizione)
            {

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "d2-input-group-addon");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-info-circle");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();

                writer.RenderEndTag();

                writer.RenderEndTag();
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "help-block error-feedback");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        private void InizializzaPropertiesDaCampoDinamico(CampoDinamicoBase campo)
        {
            MethodInfo mi = GetType().GetMethod("GetProprietaDesigner", BindingFlags.Static | BindingFlags.Public);

            if (mi == null)
            {
                return;
            }

            var proprieta = (ProprietaDesigner[])mi.Invoke(null, null);

            // Salvo i valori di default in un dictionary
            Dictionary<string, object> propDictionary = new Dictionary<string, object>();

            for (int i = 0; i < proprieta.Length; i++)
                propDictionary.Add(proprieta[i].NomeProprieta, proprieta[i].ValoreDefault);

            // Assegno al dictionary delle proprietà i valori letti dal db
            foreach (var it in campo.ProprietaControlloWeb)
            {
                if (!propDictionary.ContainsKey(it.Key))
                    propDictionary.Add(it.Key, it.Value);
                else
                    propDictionary[it.Key] = it.Value;
            }

            // Assegno i valori al controllo
            PropertyDescriptorCollection propCollection = TypeDescriptor.GetProperties(this);

            foreach (string key in propDictionary.Keys)
            {
                var nomeProperty = key;
                var value = propDictionary[key];

                // HACK: data la separazione dei progetti non è possibile assegnarla nel campo dinamico (anche se sarebbe più corretto)
                if (nomeProperty == "TipoNumerico" && campo is CampoDinamico)
                {
                    (campo as CampoDinamico).TipoNumerico = Convert.ToBoolean(value);
                    continue;
                }

                try
                {
                    PropertyDescriptor pd = propCollection[nomeProperty];

                    if (pd != null)
                        pd.SetValue(this, Convert.ChangeType(value, pd.PropertyType));
                }
                catch (Exception ex)
                {
                    var errMsg = String.Format("Errore durante l'assegnazione del valore \"{0}\" proprietà {1} al campo {2}: {3}", value, nomeProperty, campo.Id, ex.Message);
                    throw new Exception(errMsg, ex);
                }

            }
        }
    }
}
