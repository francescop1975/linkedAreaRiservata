﻿using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using System;

namespace Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Formule
{
    public class AutenticazioneFormuleService
    {
        private readonly SigeproSecurityProxy _sigeproSecurityProxy;

        public AutenticazioneFormuleService(SigeproSecurityProxy sigeproSecurityProxy)
        {
            this._sigeproSecurityProxy = sigeproSecurityProxy;
        }

        public AutenticazioneFormuleService()
            : this(new SigeproSecurityProxy())
        {

        }

        public IFormuleAuthenticationInfo CheckToken(string token)
        {
            var checkTokenResponse = this._sigeproSecurityProxy.CheckToken(token);

            if (!checkTokenResponse.valid)
            {
                throw new Exception($"Token {token} non valido");
            }

            var dbInfo = this._sigeproSecurityProxy.GetDbConnection(checkTokenResponse.tokenInfo.alias);

            return new FormuleAuthenticationInfo(token, checkTokenResponse.tokenInfo.alias, checkTokenResponse.tokenInfo.idcomune, dbInfo);
        }
    }
}
