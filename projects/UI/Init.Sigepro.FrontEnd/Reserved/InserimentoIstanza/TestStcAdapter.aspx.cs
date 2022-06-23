﻿using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Ninject;
using System;
using System.Xml.Serialization;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class TestStcAdapter : IstanzeStepPage
    {
        [Inject]
        protected IIstanzaStcAdapter _stcAdapter { get; set; }

        [Inject]
        protected ISalvataggioDomandaStrategy LogicaCaricamento { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Adapt();
        }

        private void Adapt()
        {
            var domanda = LogicaCaricamento.GetById(IdDomanda);

            var dettaglioPratica = _stcAdapter.Adatta(domanda);

            using (var fs = new System.IO.MemoryStream())
            {
                var xs = new XmlSerializer(dettaglioPratica.GetType());
                xs.Serialize(fs, dettaglioPratica);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=istanza.xml");
                Response.BinaryWrite(fs.ToArray());
                Response.End();
            }
        }
    }
}