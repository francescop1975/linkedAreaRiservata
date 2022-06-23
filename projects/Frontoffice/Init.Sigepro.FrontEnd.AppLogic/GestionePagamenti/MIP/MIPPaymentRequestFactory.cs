using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.Pagamenti;
using Init.Sigepro.FrontEnd.Pagamenti.MIP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.MIP
{
    public class MIPPaymentRequestFactory : IMIPPaymentRequestFactory
    {
        private static class Constants
        {
            public const string SpecializzazioneSettingsKey = "MIPPaymentRequestFactory.SpecializzazioneMIP";
        }

        private readonly PagamentiSettings _settings;
        private readonly ISalvataggioDomandaStrategy _salvataggioDomandaStrategy;
        private readonly IAuthenticationDataResolver _authenticationDataResolver;

        public MIPPaymentRequestFactory(IPagamentiSettingsReader settingsReader, ISalvataggioDomandaStrategy salvataggioDomandaStrategy, IAuthenticationDataResolver authenticationDataResolver)
        {
            this._settings = settingsReader.GetSettings();
            this._salvataggioDomandaStrategy = salvataggioDomandaStrategy;
            this._authenticationDataResolver = authenticationDataResolver;
        }

        public PaymentRequest Create(IniziaPagamentoRequest request)
        {
            if (ConfigurationManager.AppSettings[Constants.SpecializzazioneSettingsKey] == "siena")
            {
                var domanda = this._salvataggioDomandaStrategy.GetById(request.RiferimentiDomanda.IdDomanda);
                var utenteLogin = this._authenticationDataResolver.DatiAutenticazione.DatiUtente;
                var richiedenteDomanda = domanda.ReadInterface.Anagrafiche.GetRichiedente();

                var richiestaSiena = new PaymentRequest(this._settings, request);

                richiestaSiena.UserDataExt = new UserDataExtType
                {
                    UtenteLogin = new UtenteLoginType
                    {
                        IdentificativoUtente = utenteLogin.Codicefiscale,
                        TipoIdentificativo = utenteLogin.Tipoanagrafe,
                        Cognome = utenteLogin.Nominativo,
                        Nome = utenteLogin.Nome
                    },
                    UtenteMandante = new UtenteType
                    {
                        IdentificativoUtente = richiedenteDomanda.Codicefiscale,
                        TipoIdentificativo = richiedenteDomanda.TipoPersona == GestionePresentazioneDomanda.GestioneAnagrafiche.TipoPersonaEnum.Fisica ? "F" : "G",
                        Cognome = richiedenteDomanda.Nominativo,
                        Nome = richiedenteDomanda.Nome
                    }
                };

                richiestaSiena.ServiceData = new PaymentRequestServiceData
                {
                    IDServizio = this._settings.IDServizio,
                    AnnoDocumento = DateTime.Now.Year.ToString(),
                    NumeroDocumento = domanda.ReadInterface.AltriDati.IdentificativoDomanda,
                    DescrizioneDocumento = $"Pagamento oneri per la pratica {domanda.ReadInterface.AltriDati.IdentificativoDomanda}"
                };


                return richiestaSiena;
            }

            return new PaymentRequest(this._settings, request);
        }
    }
}
