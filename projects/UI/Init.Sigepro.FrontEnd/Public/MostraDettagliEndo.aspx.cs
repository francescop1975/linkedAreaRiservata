﻿using Init.Sigepro.FrontEnd.AppLogic.GestioneConversioneFiles;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.SIGePro.Manager.DTO.Common;
using Init.SIGePro.Manager.DTO.Endoprocedimenti;
using Init.SIGePro.Manager.DTO.Normative;
using Ninject;
using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Init.Sigepro.FrontEnd.Public
{
    public partial class MostraDettagliEndo : Ninject.Web.PageBase
    {
        [Inject]
        public ICartRepository _cartRepository { get; set; }
        [Inject]
        public IEndoprocedimentiService _endoprocedimentiRepository { get; set; }

        [Inject]
        public IUrlDownloadOggettiService _urlDownloadService { get; set; }

        public virtual string IdComune
        {
            get
            {
                return Request.QueryString["IdComune"];
            }
        }

        public int Id
        {
            get { return Convert.ToInt32(Request.QueryString["Id"]); }
        }

        public bool Print
        {
            get
            {
                var qs = Request.QueryString["print"];

                if (String.IsNullOrEmpty(qs))
                    return false;

                if (qs.ToUpper() == "TRUE")
                    return true;

                return false;
            }
        }

        public bool MostraBottoneStampa
        {
            get
            {
                var qs = Request.QueryString["MostraBottoneStampa"];

                if (String.IsNullOrEmpty(qs))
                    return false;

                if (qs.ToUpper() == "TRUE")
                    return true;

                return false;
            }
        }

        private bool Popup
        {
            get
            {
                var qs = Request.QueryString["popup"];

                if (String.IsNullOrEmpty(qs))
                    return false;

                if (qs.ToUpper() == "TRUE")
                    return true;

                return false;
            }
        }

        private bool FromAreaRiservata
        {
            get
            {
                var fromAreaRiservata = Request.QueryString["fromAreaRiservata"];

                if (String.IsNullOrEmpty(fromAreaRiservata))
                    return true;

                return fromAreaRiservata.ToUpper() == "TRUE";
            }
        }

        public EndoprocedimentoDto DataSource { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var urlCart = _cartRepository.GetUrlSchedaCARTEndo(Id);

            if (!IsPostBack)
                DataBind();
        }

        public override void DataBind()
        {
            DataSource = _endoprocedimentiRepository.GetById(Id, FromAreaRiservata ? AmbitoRicerca.AreaRiservata : AmbitoRicerca.FrontofficePubblico);
            Page.Title = DataSource.Descrizione;
            base.DataBind();


            pnlSchedaCart.Visible = false;

            var urlCart = _cartRepository.GetUrlSchedaCARTEndo(Id);

            if (!String.IsNullOrEmpty(urlCart))
            {
                pnlSchedaCart.Visible = true;
                BindSchedaCart(urlCart);
            }
        }


        private void BindSchedaCart(string urlCart)
        {
            // used to build entire input
            StringBuilder sb = new StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];

            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCart);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            var respEncoding = Encoding.GetEncoding(response.CharacterSet);

            // we will read data via the response stream
            var resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = respEncoding.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            string content = sb.ToString();

            var startIdx = content.IndexOf("<body>");

            if (startIdx > 0)
            {
                startIdx += 6;

                var endIdx = content.IndexOf("</body>");

                if (endIdx > 0)
                    content = content.Substring(startIdx, endIdx - startIdx);
            }

            content = "<div id='datiCart'>" + content + "</div>";

            ltrSchedaCart.Text = content;
        }


        public string GetUrlStampaPagina()
        {
            var req = HttpContext.Current.Request;
            var downloadUrl = req.Url.Scheme + "://" + req.Url.Host + ":" + req.Url.Port;

            if (!String.IsNullOrEmpty(req.ApplicationPath))
                downloadUrl += req.ApplicationPath;

            if (!downloadUrl.EndsWith("/"))
                downloadUrl += "/";

            downloadUrl += "Public/MostraDettagliEndo.aspx?idComune=" + IdComune + "&Id=" + Id + "&Print=true";

            return downloadUrl;
        }

        public string GetUrlDownloadPagina()
        {
            var downloadUrl = GetUrlStampaPagina();

            return GetBaseUrl() + "Public/DownloadPage.ashx" + "?IdComune=" + IdComune + "&url=" + Server.UrlEncode(downloadUrl);
        }

        public string GetLinkNormativa(object objNormativa)
        {
            var normativa = (NormativaDto)objNormativa;
            var link = normativa.Link;

            if (String.IsNullOrEmpty(link))
                return String.Empty;

            if (!link.ToUpper().StartsWith("HTTP"))
                link = "http://" + link;

            return GetLinkEsterno(link);
        }


        public string GetLinkEsterno(object objLink)
        {
            if (objLink == null)
                return String.Empty;

            var strLink = objLink.ToString();

            if (String.IsNullOrEmpty(strLink))
                return String.Empty;

            var baseUrl = GetBaseUrl();

            baseUrl += "images/link_icon.png";

            var fmtString = "<a href='{0}' target='_blank'><img src='{1}' alt='Link esterno'/></a>";

            return string.Format(fmtString, strLink, baseUrl);
        }

        protected string GetBaseUrl()
        {
            var req = HttpContext.Current.Request;
            var baseUrl = req.Url.Scheme + "://" + req.Url.Host + ":" + req.Url.Port;

            if (!String.IsNullOrEmpty(req.ApplicationPath))
                baseUrl += req.ApplicationPath;

            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";

            return baseUrl;
        }

        public string GetLinkModello(object objIdOggetto, object objFormatiDownload)
        {
            if (objIdOggetto == null)
                return String.Empty;

            var baseUrl = GetBaseUrl();
            var formatoDownload = objFormatiDownload == null ? String.Empty : objFormatiDownload.ToString();

            if (String.IsNullOrEmpty(formatoDownload))
                formatoDownload = "pdf";

            var formatiSupportati = formatoDownload.Split(',');

            var sb = new StringBuilder();

            for (int i = 0; i < formatiSupportati.Length; i++)
            {
                var formato = formatiSupportati[i].ToUpperInvariant();
                var isPdfCompilabile = formato == "PDFC";
                var codiceOggetto = Convert.ToInt32(objIdOggetto);

                if (String.IsNullOrEmpty(formato))
                {
                    continue;
                }

                formato = isPdfCompilabile ? "PDF" : formato;

                var urlIcona = $"{baseUrl}images/{formato}16x16.gif";

                if (!Enum.TryParse(formato, out FormatoConversioneEnum formatoEnum))
                {
                    continue; // il formato non è supportato
                }

                var urlOggetto = this.Page.ResolveClientUrl(this._urlDownloadService.GetUrlDownloadConvertito(codiceOggetto, formatoEnum));

                sb.Append($"<a href='{urlOggetto}'><img src='{urlIcona}' /></a>");
            }

            return sb.ToString();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (Popup || Print)
                base.Render(writer);
            else
                pnlDatiEndo.RenderControl(writer);
        }
    }
}
