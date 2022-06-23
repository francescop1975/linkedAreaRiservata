using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.Authentication;
using Init.SIGePro.Manager.WsSigeproSecurity;
using Init.SIGePro.Utils;
using PersonalLib2.Data;
using Init.SIGePro.Verticalizzazioni;
using Init.SIGePro.Manager.Utils;
using System.Web;
using Init.Utils;
using Init.SIGePro.Manager.Properties;

namespace Init.SIGePro.Authentication
{

    /// <summary>
    /// Gestore dell'autenticazione degli utenti. 
    /// TODO: implementare un metodo che elimini tutti i token espirati
    /// </summary>
    public class AuthenticationManager
    {


        /// <summary>
        /// Autentica un utente in base a username e password
        /// </summary>
        /// <param name="alias">Codice del comune con cui leggere in Comunisecurity</param>
        /// <param name="userId">Username dell'utente</param>
        /// <param name="password">Password dell'utente</param>
        /// <param name="adminOption">Flag per stabilire se si tratta di un'autenticazione effettuata da un utente del comune (FALSE) oppure da un utente esterno (TRUE)</param>
        /// <returns>Classe <see cref="AuthenticationInfo"/> che contiene i dati relativi all'autenticazione o null se l'autenticazione non � andata a buon fine</returns>
        public static AuthenticationInfo Login(string alias, string userId, string password, bool adminOption)
        {
            if (adminOption)
                return Login(alias, userId, password, ContextType.ExternalUsers);
            else
                return Login(alias, userId, password, ContextType.Operatore);
        }

        public static AuthenticationInfo GetTokenApplicativo(string alias)
        {
            return Login(alias, Settings.Default.SigeproSecurityUsername, Settings.Default.SigeproSecurityPassword, ContextType.ExternalUsers);
        }

        /// <summary>
        /// Verifica la validit� di un token passato.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static AuthenticationInfo CheckToken(string token)
        {
            var ai = CheckToken(token, TipiAmbiente.DOTNET);

            HttpContextAuthenticationInfoResolver.SetAuthenticationInfo(ai);

            return ai;
        }

        /// <summary>
        /// Restituisce le informazioni utili per la connessione al database ricevendo in ingresso il token e l'ambiente
        /// </summary>
        /// <param name="token">Token per il quale ricavare le informazioni per la connessione al database</param>
        /// <param name="ambiente">Ambiente per il quale ricavare le informazioni per la connessione al database</param>
        /// <returns></returns>
        public static AuthenticationInfo CheckToken(string token, TipiAmbiente ambiente)
        {
			using (var cp = new CodeProfiler("CheckToken"))
			{
				IAuthenticationInfoRepository repository = new AuthenticationInfoRepositoryFactory().Create();

				return repository.GetByToken(token, ambiente);
			}
        }


        /// <summary>
        /// Autentica un utente in base a username e password
        /// </summary>
        /// <param name="alias">Codice del comune con cui leggere in Comunisecurity</param>
        /// <param name="userId">Username dell'utente<  /param>
        /// <param name="password">Password dell'utente</param>
        /// <param name="adminOption">Flag per stabilire se si tratta di un'autenticazione effettuata da un utente del comune (FALSE) oppure da un utente esterno (TRUE)</param>
        /// <returns>Classe <see cref="AuthenticationInfo"/> che contiene i dati relativi all'autenticazione o null se l'autenticazione non � andata a buon fine</returns>
        public static string LoginSSO(string alias, Anagrafe anagrafica )
        {
			if (string.IsNullOrEmpty(alias))
				throw new Exception("Non � stato specificato il parametro [alias]");

			if (anagrafica == null)
				throw new Exception("Non � stato specificato il parametro [anagrafica]");

			if (string.IsNullOrEmpty(anagrafica.CODICEFISCALE) && string.IsNullOrEmpty(anagrafica.PARTITAIVA))
				throw new Exception("E' stata passata un'anagrafica senza Codice Fiscale e Partita IVA. Uno di questi due parametri � obbligatorio per eseguire l'autenticazione");

			// verifico che il codice fiscale sia scritto in maiuscolo
			if (!String.IsNullOrEmpty(anagrafica.CODICEFISCALE))
				anagrafica.CODICEFISCALE = anagrafica.CODICEFISCALE.ToUpper();

			if (!String.IsNullOrEmpty(anagrafica.PARTITAIVA))
				anagrafica.PARTITAIVA = anagrafica.PARTITAIVA.ToUpper();

			var ccnReq = new GetDbConnectionInfoRequest{
				alias = alias,
				ambiente = AmbienteType.DOTNET
			};

            var sigeproSecurityProxy = new SigeproSecurityProxy();
            var cnnInfo = sigeproSecurityProxy.GetDbConnectionInfo(ccnReq);

			using (var db = cnnInfo.CreateDatabase())
			{
				string idComune = cnnInfo.idComune;

				// Verifico che la verticalizzaizone sia attiva
                var verticalizzazioneSSO = new VerticalizzazioneAutenticazioneSso(alias, "TT");
				if (!verticalizzazioneSSO.Attiva)
					throw new Exception("Non � possibile utilizzare questo metodo se non � attiva la modalit� di autenticazione SSO");

				anagrafica.IDCOMUNE = idComune;

				var anagrafeMgr = new AnagrafeMgr(db);

				anagrafeMgr.EscludiControlliSuAnagraficheDisabilitate = true;
				anagrafeMgr.RicercaSoloCF_PIVA = true;

				var filtro = new Anagrafe
				{
					IDCOMUNE = anagrafica.IDCOMUNE,
					CODICEFISCALE = anagrafica.CODICEFISCALE,
					PARTITAIVA = anagrafica.PARTITAIVA,
					NOMINATIVO = anagrafica.NOMINATIVO,
					NOME = anagrafica.NOME,
					FLAG_DISABILITATO = "0",
				};

				filtro = anagrafeMgr.Extract(filtro);

				if (string.IsNullOrEmpty(filtro.CODICEANAGRAFE))
				{
					//l'anagrafica non esiste e deve essere inserita
					anagrafica.PASSWORD = RandomPassword.Generate(6);
					filtro = anagrafeMgr.Insert(anagrafica);
				}
				else
				{
					//l'anagrafica � stata trovata, potrebbe non avere una password che va eventualmente impostata
					if (string.IsNullOrEmpty(filtro.PASSWORD))
					{
						filtro.PASSWORD = RandomPassword.Generate(6);
						filtro = anagrafeMgr.Update(filtro);
					}
				}

				string codUtente = String.IsNullOrEmpty(filtro.CODICEFISCALE) ? filtro.PARTITAIVA : filtro.CODICEFISCALE;

				return Login(alias, codUtente, filtro.PASSWORD, ContextType.Anagrafe).Token;
			}
        }



        /// <summary>
        /// Effettua l'autenticazione di nua coppia username/password in base ai parametri specificati
        /// </summary>
        /// <param name="alias">alias del comune</param>
        /// <param name="userId">user id</param>
        /// <param name="password">password</param>
        /// <param name="tipoContesto">contesto da cui estrarre le informazioni sulla connessione</param>
        /// <returns></returns>
        public static AuthenticationInfo Login(string alias, string userId, string password, ContextType tipoContesto)
        {
			IAuthenticationInfoRepository repository = new AuthenticationInfoRepositoryFactory().Create();

			return repository.GetByLoginInfo(alias, userId, password, tipoContesto);
        }

        /// <summary>
        /// Richiede la lista dei parametri dell'installazione alla SigeproSecurity Java
        /// </summary>
        /// <param name="param">se omesso riporta le informazioni per tutti i parametri, altrimenti serve a specificare il singolo parametro che si vuole rileggere</param>
        /// <returns></returns>
        public static ApplicationInfoType[] GetApplicationInfo()
        {
			
            return new SigeproSecurityProxy().GetApplicationInfo();
        }

		/// <summary>
		/// Richiede la lista dei parametri dell'installazione alla SigeproSecurity Java
		/// </summary>
		/// <param name="param">se omesso riporta le informazioni per tutti i parametri, altrimenti serve a specificare il singolo parametro che si vuole rileggere</param>
		/// <returns></returns>
		public static string GetApplicationInfoValue(string param)
		{
			if (string.IsNullOrEmpty(param))
				return String.Empty;

            var cfgValue = ConfigurationManager.AppSettings[param];

            if (!String.IsNullOrEmpty(cfgValue))
            {
                return cfgValue;
            }

			var appInfo = GetApplicationInfo();

			var res = from ait in appInfo
					  where ait.param.ToUpper() == param
					  select ait.value;

			if (res.Count() == 0)
				return String.Empty;

			return res.ElementAt(0);
		}

    }
}


