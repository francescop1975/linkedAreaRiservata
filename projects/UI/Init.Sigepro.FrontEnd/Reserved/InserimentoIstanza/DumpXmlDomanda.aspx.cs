using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class DumpXmlDomanda : IstanzeStepPage
    {
        [Inject]
        protected ISalvataggioDomandaStrategy SalvataggioDomandaStrategy { get; set; }
        [Inject]
        protected IIstanzaSigeproAdapterService istanzaSigeproAdapterService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "text/plain;charset=UTF-8";

            if (!String.IsNullOrEmpty(Request.QueryString["vbg"]))
            {
                var pratica = SalvataggioDomandaStrategy.GetById(IdDomanda);
                var xml = this.istanzaSigeproAdapterService.ToIstanzaBackoffice(pratica.ReadInterface).ToXmlModelloRiepilogo();
                Response.Write(xml);
            }
            else
            {
                var domanda = SalvataggioDomandaStrategy.GetAsXml(IdDomanda);
                Response.BinaryWrite(domanda);
            }

            Response.End();
        }
    }
}