﻿using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.WebConfig;
using Init.Sigepro.FrontEnd.AppLogic.SigeproSecurityService;
using Init.Sigepro.FrontEnd.Infrastructure.Caching;
using log4net;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg
{
    public class SigeproSecurityProxy
    {
        private static class Constants
        {
            public const string BindingName = "defaultServiceBinding";
            public const string ConfigSectionName = "sigepro/frontEnd";
            public const string ApplicationInfoCacheKey = "SigeproSecurityProxy.ApplicationInfoCacheKey";
        }

        private class CredenzialiApplicazione
        {
            public readonly string Username;
            public readonly string Password;

            public CredenzialiApplicazione(string username, string password)
            {
                this.Username = username;
                this.Password = password;
            }
        }

        private readonly CredenzialiApplicazione _credenzialiApplicazione;
        private readonly string _url;

        public SigeproSecurityProxy()
        {
            var cfg = this.GetConfigurazione();

            var username = cfg.SigeproSecurity.Username;
            var password = cfg.SigeproSecurity.Password;
            var url = cfg.SigeproSecurity.LoginServiceUrl;

            Condition.Requires(username, "username").IsNotNullOrEmpty();
            Condition.Requires(password, "password").IsNotNullOrEmpty();
            Condition.Requires(url, "url").IsNotNullOrEmpty();

            this._credenzialiApplicazione = new CredenzialiApplicazione(username, password);

            this._url = url;
        }

        private readonly ILog _log = LogManager.GetLogger(typeof(SigeproSecurityProxy));

        public string GetApplicationToken(string aliasComune)
        {
            return this.GetToken(aliasComune, this._credenzialiApplicazione.Username, this._credenzialiApplicazione.Password, ContestoType.APP);
        }

        public string GetTokenAnonimo(string alias, string usernameAnonimo, string passwordAnonimo)
        {
            var token = this.GetToken(alias, usernameAnonimo, passwordAnonimo, ContestoType.UTE);

            this.ImpostaLivelloDiAutenticazione(token, LivelloAutenticazioneEnum.Anonimo);

            return token;
        }

        public string GetTokenPartnerApp(string token)
        {
            try
            {
                return this.CallService(ws =>
                {
                    var response = ws.GetTokenPartnerApp(new GetTokenPartnerAppRequest
                    {
                        token = token
                    });

                    if (response == null)
                    {
                        throw new InvalidOperationException("GetTokenPartnerApp response is null");
                    }

                    return response.tokenPartnerApp;
                });
            }
            catch (Exception ex)
            {
                this._log.ErrorFormat("Errore durante la chiamata a GetTokenPartnerApp con token={1}: {0}", ex.ToString(), token);

                throw;
            }
        }

        public string GetToken(string alias, string username, string password, ContestoType contesto)
        {
            using (var ws = this.CreateClient())
            {
                var req = new LoginRequest
                {
                    alias = alias,
                    username = username,
                    password = password,
                    ipAddress = "127.0.0.1",
                    contesto = contesto
                };

                try
                {
                    this._log.DebugFormat("Lettura di un nuovo token con le credenziali:\nusername={0}\npassword={1}\ncontesto={2}\nalias={3}\nip={4}",
                                            req.username,
                                            req.password,
                                            req.contesto,
                                            req.alias,
                                            req.ipAddress);

                    var scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(this.CreateHeader());

                    var res = ws.Login(req);

                    this._log.DebugFormat("Nuovo token = {0}", res.token);

                    return res.token;
                }
                catch (Exception ex)
                {
                    // TODO: log dell'errore
                    this._log.ErrorFormat("Errore durante il login:{0}\r\n Username: {1}, Password: {2}, Contesto: {3}, Alias: {4} ", ex.ToString(), req.username, req.password, req.contesto, req.alias);

                    throw;
                }
            }
        }

        internal bool IsComuneAttivo(string alias)
        {
            return this.CallService(client =>
            {
                try
                {
                    var l = client.GetSecurityList(new GetSecurityListRequest
                    {
                        alias = alias,
                        tipo = ComunisecurityAttiviType.ATTIVI,
                        tipoSpecified = true
                    });

                    if (l.Length > 0 && l[0].attivo)
                    {
                        return true;
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    this._log.Error($"Errore durante la verifica dell'attivazione dell'alias {alias} ");

                    return false;
                }

            });
        }

        private void ImpostaLivelloDiAutenticazione(string token, LivelloAutenticazioneEnum livello)
        {
            using (var ws = this.CreateClient())
            {
                try
                {
                    this._log.DebugFormat("Impostazione del livello di autenticazione {0} per il token {1}", livello, token);

                    var request = new SetAuthLevelRequest
                    {
                        token = token,
                        authlevel = ((int)livello).ToString()
                    };

                    var scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(this.CreateHeader());

                    ws.SetAuthLevel(request);
                }
                catch (Exception ex)
                {
                    ws.Abort();

                    this._log.ErrorFormat("Errore durante l'impostazione del livello di autenticazione: {0} per il token {1}: {2} ", livello, token, ex.ToString());

                    throw;
                }
            }
        }

        public CheckTokenResponse CheckToken(string token)
        {
            return this.CheckToken(token, true);
        }

        public CheckTokenResponse CheckToken(string token, bool leggiTokenInfo)
        {
            using (var ws = this.CreateClient())
            {
                try
                {
                    this._log.DebugFormat("Verifica del token {0} con tokenInfo = {1}", token, leggiTokenInfo);

                    var req = new CheckTokenRequest
                    {
                        token = token,
                        tokenInfo = leggiTokenInfo
                    };

                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(this.CreateHeader());

                    var res = ws.CheckToken(req);

                    this._log.DebugFormat("Esito della verifica: {0} ", res.valid);

                    return res;
                }
                catch (Exception ex)
                {
                    // TODO: log dell'errore
                    this._log.ErrorFormat("Errore durante la verifica del token {0}\r\n {1}", token, ex.ToString());

                    throw;
                }
            }
        }

        public LivelloAutenticazioneEnum GetLivelloAutenticazione(string token)
        {
            using (var ws = this.CreateClient())
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(this.CreateHeader());

                    var livelloAutenticazione = ws.GetAuthLevel(new GetAuthLevelRequest
                    {
                        token = token
                    });

                    var lvl = String.IsNullOrEmpty(livelloAutenticazione.authlevel) ? 1 : Convert.ToInt32(livelloAutenticazione.authlevel);

                    return (LivelloAutenticazioneEnum)lvl;
                }
                catch (Exception ex)
                {
                    this._log.ErrorFormat("Errore durante la chiamata a GetApplicationInfo {0}", ex.ToString());

                    throw;
                }
            }
        }

        internal ApplicationInfoType[] GetApplicationInfo()
        {
            if (!CacheHelper.KeyExists(Constants.ApplicationInfoCacheKey))
            {
                var appInfo = this.GetApplicationInfoInternal();

                CacheHelper.AddEntry(Constants.ApplicationInfoCacheKey, appInfo);
            }

            return CacheHelper.GetEntry<ApplicationInfoType[]>(Constants.ApplicationInfoCacheKey);
        }

        private ApplicationInfoType[] GetApplicationInfoInternal()
        {
            using (var ws = this.CreateClient())
            {
                try
                {
                    var req = new GetApplicationInfoRequest
                    {
                        param = String.Empty
                    };
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(this.CreateHeader());

                    return ws.GetApplicationInfo(req);
                }
                catch (Exception ex)
                {
                    this._log.ErrorFormat("Errore durante la chiamata a GetApplicationInfo {0}", ex.ToString());

                    throw;
                }
            }
        }

        private MessageHeader CreateHeader()
        {
            return UserNameSecurityTokenHeader.FromUserNamePassword(this._credenzialiApplicazione.Username, this._credenzialiApplicazione.Password);
        }

        private sigeproSecurityClient CreateClient()
        {
            var endPoint = new EndpointAddress(this._url);

            var binding = new BasicHttpBinding(Constants.BindingName);

            return new sigeproSecurityClient(binding, endPoint);
        }

        protected ConfigurazioneFrontEndWebConfig GetConfigurazione()
        {
            ConfigurazioneFrontEndWebConfig config = (ConfigurazioneFrontEndWebConfig)System.Configuration.ConfigurationManager.GetSection(Constants.ConfigSectionName);

            return config;
        }

        internal IDbConnectionInfo GetDbConnection(string alias)
        {
            var cacheKey = $"SigeproSecurityProxy.GetDbConnection.{alias}";

            return CacheHelper.GetOrAdd(cacheKey, () => this.CallService(ws =>
            {
                var response = ws.GetDbConnectionInfo(new GetDbConnectionInfoRequest
                {
                    alias = alias,
                    ambiente = AmbienteType.DOTNET
                });

                return new DbConnectionInfo
                {
                    ConnectionString = response.connectionString,
                    ProviderName = response.provider,
                    Username = response.dbUser,
                    Password = response.dbPassword
                };
            }));
        }

        public void Logout(string token)
        {
            using (var ws = this.CreateClient())
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(this.CreateHeader());

                    ws.Logout(new LogoutRequest
                    {
                        token = token
                    });
                }
                finally
                {
                    ws.Abort();
                }

            }
        }

        public T CallService<T>(Func<sigeproSecurityClient, T> operation)
        {
            using (var ws = this.CreateClient())
            {
                try
                {
                    OperationContextScope scope = new OperationContextScope(ws.InnerChannel);

                    OperationContext.Current.OutgoingMessageHeaders.Add(this.CreateHeader());

                    return operation(ws);
                }
                catch (Exception)
                {
                    ws.Abort();

                    throw;
                }
            }
        }
    }
}
