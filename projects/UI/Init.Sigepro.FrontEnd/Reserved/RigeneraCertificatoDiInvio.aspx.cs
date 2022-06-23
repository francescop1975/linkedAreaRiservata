using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio;
using Init.Sigepro.FrontEnd.AppLogic.STC;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class RigeneraCertificatoDiInvio : ReservedBasePage
    {
        private static class Constants
        {
            public const string ParametroIdDomandaBackoffice = "IdDomandaBackoffice";
            public const string ParametroAllegaAPratica = "allega";
        }

        private string IdDomandaBackoffice
        {
            get { return Request.QueryString[Constants.ParametroIdDomandaBackoffice]; }
        }

        private bool AllegaAPratica
        {
            get
            {
                if (String.IsNullOrEmpty(Request.QueryString[Constants.ParametroAllegaAPratica]))
                {
                    return true;
                }
                return Request.QueryString[Constants.ParametroAllegaAPratica].ToString() != "0";
            }
        }

        [Inject]
        protected CertificatoDiInvioService _certificatoDiInvioService { get; set; }
        [Inject]
        protected IStcService _stcService { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            //var domanda = SalvataggioDomandaStrategy.GetById( IdDomanda );

            if (!_stcService.PraticaEsisteNelBackend(IdDomandaBackoffice))
                throw new Exception("La pratica " + IdDomandaBackoffice + " non esiste nel backend");

            var cert = _certificatoDiInvioService.GeneraCertificatoDiInvio(Convert.ToInt32(IdDomandaBackoffice), this.AllegaAPratica);

            Response.Clear();
            Response.ContentType = cert.MimeType;
            Response.AddHeader("content-disposition", "attachment;filename=\"" + cert.FileName + "\"");
            Response.BinaryWrite(cert.FileContent);
            Response.End();
        }
    }
}