﻿using Init.Sigepro.FrontEnd.AppLogic.AllegatiDomanda;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Helper.FileUploadHandlers
{
    /// <summary>
    /// Summary description for DownloadHandler
    /// </summary>
    public class DownloadHandler : DatiDinamiciFileUploadHandlerBase
    {
        [Inject]
        public IAllegatiDomandaFoRepository AllegatiDomandaFoRepository { get; set; }

        private int CodiceOggetto
        {
            get
            {
                return Convert.ToInt32(this.Context.Request.QueryString["CodiceOggetto"]);
            }
        }


        public override void DoProcessRequestInternal()
        {
            try
            {
                var file = AllegatiDomandaFoRepository.LeggiAllegato(IdDomanda, CodiceOggetto);

                if (file == null)
                    throw new Exception("File " + CodiceOggetto + " non trovato");

                Context.Response.Clear();
                Context.Response.ContentType = file.MimeType;
                Context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + file.FileName + "\"");
                Context.Response.BinaryWrite(file.FileContent);
            }
            catch (Exception ex)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "text/plain";
                Context.Response.Write("Errore durante la lettura del file specificato: " + ex.Message);
            }

        }
    }
}