﻿namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.EditOggetti
{
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
    using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDocumenti;
    using Init.Sigepro.FrontEnd.AppLogic.ReadInterface;
    using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
    using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
    using Init.Sigepro.FrontEnd.QsParameters;
    using Ninject;
    using System;
    using System.Linq;
    using System.Web;

    public partial class Edit : ReservedBasePage
    {
        [Inject]
        protected IReadFacade ReadFacade { get; set; }

        [Inject]
        protected PathUtils _pathUtils { get; set; }

        [Inject]
        protected IOggettiService _oggettiService { get; set; }

        [Inject]
        protected IUrlDownloadOggettiService _urlDownloadService { get; set; }

        private int IdDomanda
        {
            get { return Convert.ToInt32(Request.QueryString[PathUtils.UrlParameters.IdPresentazione]); }
        }

        private string ReturnTo
        {
            get { return Request.QueryString[PathUtils.UrlParameters.ReturnTo].ToString(); }
        }

        private int IdAllegato
        {
            get { return Convert.ToInt32(Request.QueryString[PathUtils.UrlParameters.IdAllegato]); }
        }

        private int? CodiceOggetto
        {
            get
            {
                var x = Request.QueryString[PathUtils.UrlParameters.CodiceOggetto];

                if (String.IsNullOrEmpty(x))
                    return (int?)null;

                return Convert.ToInt32(x);
            }
        }

        private bool PdfCompilabile
        {
            get
            {
                var x = Request.QueryString[PathUtils.UrlParameters.PdfCompilabile];

                return String.IsNullOrEmpty(x) || x == PathUtils.UrlParametersValues.True;
            }
        }

        private string TipoAllegato
        {
            get
            {
                return Request.QueryString[PathUtils.UrlParameters.TipoAllegato];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string GetReturnToUrl()
        {
            return this.ReturnTo;
        }

        public string GetUrlDownloadJnlp()
        {
            var idModello = 0;


            if (CodiceOggetto.HasValue)
            {
                idModello = CodiceOggetto.Value;
            }
            else
            {
                var allegato = GetAllegato();

                idModello = allegato.CodiceOggettoModello.Value;
            }

            var url = UrlBuilder.Url("~/Reserved/InserimentoIstanza/EditOggetti/get-jnlp.ashx", qs =>
            {
                qs.Add(new QsAliasComune(this.IdComune));
                qs.Add(new QsSoftware(this.Software));
                qs.Add(new QsIdDomandaOnline(this.IdDomanda));
                qs.Add("IdModello", idModello);
                qs.Add("codiceoggetto", this.CodiceOggetto);
                qs.Add(new QsTimestamp());
                qs.Add("IdAllegato", this.IdAllegato);
                qs.Add("TipoAllegato", this.TipoAllegato);
            });

            return ResolveClientUrl(url);
        }


        public string GetUrlDownload()
        {
            var path = String.Empty;
            var codiceOggetto = 0;


            if (CodiceOggetto.HasValue)
            {
                codiceOggetto = CodiceOggetto.Value;
            }
            else
            {
                var allegato = GetAllegato();

                codiceOggetto = allegato.CodiceOggettoModello.Value;
            }

            if (PdfCompilabile)
            {
                path = this._urlDownloadService.GetUrlDownloadPdfCompilabile(codiceOggetto, this.IdDomanda);
            }
            else
            {
                path = this._urlDownloadService.GetUrlDownload(codiceOggetto);
            }

            return this.ToAbsoluteUrl(path.Substring(1));
        }

        private DocumentoDomanda GetAllegato()
        {
            var listaDocumenti = this.ReadFacade.Domanda.Documenti.Intervento.Documenti.Union(this.ReadFacade.Domanda.Documenti.Endo.Documenti);

            var allegato = listaDocumenti.Where(x => x.Id == this.IdAllegato).FirstOrDefault();
            return allegato;
        }

        private string GetNomeFile()
        {
            if (CodiceOggetto.HasValue)
            {
                return Server.UrlEncode(_oggettiService.GetNomeFile(CodiceOggetto.Value));
            }
            else
            {

                var allegato = GetAllegato();

                return Server.UrlEncode(_oggettiService.GetNomeFile(allegato.CodiceOggettoModello.Value));
            }
        }

        public string GetUrlUpload()
        {
            var nomeFile = GetNomeFile();
            var path = this._pathUtils.Reserved.InserimentoIstanza.EditOggetti.GetUrlUpload(this.IdAllegato, this.TipoAllegato, this.IdDomanda, nomeFile, CodiceOggetto).Substring(1);

            return this.ToAbsoluteUrl(path);
        }

        private string ToAbsoluteUrl(string relative)
        {
            var pre = HttpContext.Current.Request.Url.Scheme
                        + "://"
                        + HttpContext.Current.Request.Url.Authority
                        + HttpContext.Current.Request.ApplicationPath;

            return pre + relative.Replace("~", String.Empty);
        }
    }
}