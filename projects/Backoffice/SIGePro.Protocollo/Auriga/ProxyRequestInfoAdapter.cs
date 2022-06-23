﻿namespace Init.SIGePro.Protocollo.Auriga
{
    public class ProxyRequestInfoAdapter
    {
        protected ParametriRegoleInfo _par;

        public ProxyRequestInfoAdapter(ParametriRegoleInfo par)
        {
            this._par = par;
        }


        public ProxyRequestInfo Adatta()
        {
            return new ProxyRequestInfo
            {
                
                token = this._par.Token,
                codiceApplicazione = this._par.CodiceApplicazione,
                hash = "",
                istanzaApplicazione = this._par.IstanzaApplicazione,
                password = this._par.Password,
                userName = this._par.Username,
                xml = ""
            };
        }
    }
}
