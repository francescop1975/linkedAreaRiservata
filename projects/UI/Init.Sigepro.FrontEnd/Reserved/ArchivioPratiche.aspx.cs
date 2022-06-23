﻿using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Utils;
using log4net;
using Ninject;
using System;
using System.Configuration;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class ArchivioPratiche : ReservedBasePage
    {
        [Inject]
        public IVisuraService _visuraRepository { get; set; }


        ILog m_logger = LogManager.GetLogger(typeof(IstanzePresentate));

        protected bool Popup
        {
            get
            {
                var qs = Request.QueryString["popup"];

                return !String.IsNullOrEmpty(qs);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.MostraIntestazione = !Popup;
            this.Master.MostraFooter = !Popup;

            if (!IsPostBack)
            {
                FiltriVisura.IdComune = IdComune;
                FiltriVisura.Software = Software;
                dglistaPratiche.IdComune = IdComune;
                dglistaPratiche.Software = Software;
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            var richiesta = FiltriVisura.GetRichiestaListaPratiche(base.UserAuthenticationResult.DatiUtente);

            try
            {
                //richiesta.LimiteRecords = GetLimiteRecords();

                var listaPratiche = _visuraRepository.GetListaPratiche(richiesta);

                multiView.ActiveViewIndex = 1;

                dglistaPratiche.DataSource = listaPratiche;
                dglistaPratiche.DataBind();
            }
            catch (RecordCountException ex)
            {
                m_logger.ErrorFormat("La ricerca nell'archivio istanze ha restituito un numero troppo elevato di records: {0} \r\n Richiesta: {1}", ex.ToString(), StreamUtils.SerializeClass(richiesta));
                Errori.Add("La ricerca ha restituito un numero troppo elevato di risultati. Aggiungere uno o più filtri per limitare il numero di risultati");
            }
            catch (Exception ex)
            {
                m_logger.ErrorFormat("Errore durante la ricerca delle istanze presentate: {0} \r\n\r\n Richiesta: {1}", ex.ToString(), StreamUtils.SerializeClass(richiesta));
                Errori.Add(ex.Message);
            }
        }

        private int GetLimiteRecords()
        {
            const string APP_SETTINGS_KEY = "limiteRecordsArchivioPratiche";
            var str = ConfigurationManager.AppSettings[APP_SETTINGS_KEY];

            if (string.IsNullOrEmpty(str))
                return 200;

            int limite = 0;

            if (!Int32.TryParse(str, out limite))
                return 200;

            return limite;
        }

        public string GetNavigateUrlFormatString()
        {
            var url = UrlBuilder.Url("~/Reserved/DettaglioIstanzaExArchivio.aspx", qs =>
            {
                qs.Add(new QsAliasComune(this.IdComune));
                qs.Add(new QsSoftware(this.Software));

                if (Popup)
                {
                    qs.Add("popup", 1);
                }

            });
            return url + "&uuid-pratica={0}";
        }

        protected void cmdNewSearch_Click(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = 0;
        }
    }
}
