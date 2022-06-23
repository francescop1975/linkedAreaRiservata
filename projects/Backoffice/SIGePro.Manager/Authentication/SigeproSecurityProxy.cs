using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Manager.WsSigeproSecurity;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Init.SIGePro.Manager.Properties;
using log4net;
using System.Web;

namespace Init.SIGePro.Manager.Authentication
{
    public class SigeproSecurityProxy
    {
        const string SERVICE_NAME = "sigeproSecurityService";

        public LoginResponse Login(LoginRequest req)
        {
            var logger = LogManager.GetLogger(typeof(SigeproSecurityProxy));

            using (var ws = new sigeproSecurityClient(SERVICE_NAME))
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

                    return ws.Login(req);
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("SigeproSecurityProxy.Login: errore duranre il login->{0}", ex.ToString());


                    throw;
                }
            }
        }

        public CheckTokenResponse CheckToken(CheckTokenRequest req)
        {
            using (var ws = new sigeproSecurityClient(SERVICE_NAME))
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

                    return ws.CheckToken(req);
                }
                catch (Exception ex)
                {
                    // TODO: log dell'errore
                    throw;
                }
            }
        }

        public GetDbConnectionInfoResponse GetDbConnectionInfo(string alias, AmbienteType ambiente = AmbienteType.DOTNET)
        {
            return GetDbConnectionInfo(new GetDbConnectionInfoRequest
            {
                alias = alias,
                ambiente = ambiente
            });
        }

        public GetDbConnectionInfoResponse GetDbConnectionInfo(GetDbConnectionInfoRequest req)
        {
            var cacheKey = $"DbConnectionInfo.{req.alias}.{req.ambiente}";

            return AddOrGetFromCache(cacheKey, () =>
            {
                using (var ws = new sigeproSecurityClient(SERVICE_NAME))
                {
                    try
                    {
                        OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                        OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

                        return ws.GetDbConnectionInfo(req);
                    }
                    catch (Exception ex)
                    {
                        ws.Abort();
                        // TODO: log dell'errore
                        throw;
                    }
                }
            });            
        }

        private static T AddOrGetFromCache<T>(string cacheKey, Func<T> createCallback)
        {
            if (HttpContext.Current.Application[cacheKey] != null)
            {
                return (T)HttpContext.Current.Application[cacheKey];
            }

            var retVal = createCallback();

            HttpContext.Current.Application[cacheKey] = retVal;

            return retVal;
        }

        internal ApplicationInfoType[] GetApplicationInfo()
        {
            var cacheKey = $"SigeproSecurityProxy.ApplicationInfo";

            return AddOrGetFromCache(cacheKey, () => {
                using (var ws = new sigeproSecurityClient(SERVICE_NAME))
                {
                    try
                    {
                        OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                        OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

                        return ws.GetApplicationInfo(new GetApplicationInfoRequest());
                    }
                    catch (Exception ex)
                    {
                        ws.Abort();
                        // TODO: log dell'errore
                        throw;
                    }
                }
            });
        }

        public string GetValoreParametro(string nomeParametro)
        {
            var req = new GetApplicationInfoRequest
            {
                param = nomeParametro
            };

            var rVal = GetApplicationInfo();
            var parametro = rVal.Where(x => x.param == nomeParametro).FirstOrDefault();

            if (parametro == null) return String.Empty;

            return parametro.value;
        }

        public SecurityListType[] GetSecurityList(GetSecurityListRequest req)
        {
            using (var ws = new sigeproSecurityClient(SERVICE_NAME))
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

                    return ws.GetSecurityList(req);
                }
                catch (Exception ex)
                {
                    // TODO: log dell'errore
                    throw;
                }
            }
        }

        /*
        public GetTokenPartnerAppResponse GetTokenDocEr(GetTokenPartnerAppRequest request)
        {
            using (var ws = new sigeproSecurityClient(SERVICE_NAME))
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

                    return ws.GetTokenPartnerApp(request);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        */
        public GetTokenPartnerAppPerComuneESoftwareResponse GetTokenDocErPerComuneeSoftware(GetTokenPartnerAppPerComuneESoftwareRequest request)
        {
            using (var ws = new sigeproSecurityClient(SERVICE_NAME))
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

                    return ws.GetTokenPartnerAppPerComuneESoftware(request);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //public static LoginSSOResponse LoginSSO(LoginSSORequest req)
        //{
        //    using (var ws = new sigeproSecurityClient(SERVICE_NAME))
        //    {
        //        try
        //        {
        //            OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

        //            OperationContext.Current.OutgoingMessageHeaders.Add(CreateHeader());

        //            return ws.LoginSSO(req);
        //        }
        //        catch (Exception ex)
        //        {
        //            // TODO: log dell'errore
        //            throw;
        //        }
        //    }
        //}

        private MessageHeader CreateHeader()
        {
            return UserNameSecurityTokenHeader.FromUserNamePassword(Settings.Default.SigeproSecurityUsername,
                                                                     Settings.Default.SigeproSecurityPassword);
        }

    }
}
