﻿namespace Init.Sigepro.FrontEnd.Reserved.GestioneMovimenti.Helper.FileUploadHandlers
{
    /// <summary>
    /// Summary description for DeleteHandler
    /// </summary>
    public class DeleteHandler : MovimentiFileUploadHandler
    {
        public override void DoProcessRequestInternal()
        {
            SerializeResponse(new { result = "OK" });
        }
    }
}