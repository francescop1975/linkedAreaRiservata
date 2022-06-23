﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.Manager;
using SIGePro.Net;
using Init.SIGePro.DatiDinamici.WebControls.MaschereSolaLettura;
using Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Attivita;
using Ninject;
using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita;

namespace Sigepro.net.Istanze.DatiDinamici
{
	public partial class AttivitaDyn2Modelli : BasePage
	{
		#region Gestione della classe Attivita
		protected int CodiceAttivita
		{
			get { return Convert.ToInt32(Request.QueryString["CodiceAttivita"]); }
		}

		IAttivita m_attivita;
		IAttivita Attivita
		{
			get
			{
				if (m_attivita == null)
					m_attivita = new IAttivitaMgr(Database).GetById(IdComune, CodiceAttivita);

				return m_attivita;
			}
		}

		public override string Software
		{
			get
			{
				return "TT";
			}
		}
		#endregion


		#region ciclo di vita della pagina

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Master.TabSelezionato = IntestazionePaginaTipiTabEnum.Scheda;

			if (!IsPostBack)
				DataBind();
		}

		public override void DataBind()
		{
			var att = new IAttivitaMgr(Database).GetById( IdComune , CodiceAttivita );

			lblCodiceAttivita.Text = att.Id.ToString();
			lblDenominazione.Text = att.Denominazione;
			
		}

		#endregion

		[Inject]
		public ISchedeDinamicheAttivitaService _schedeDinamicheAttivitaService { get; set; }

		#region handler degli eventi del controllo di gestione modelli

		public List<ElementoListaModelli> GetListaModelli()
		{
			var modelli = new IAttivitaDyn2ModelliTMgr(Database).GetModelliCollegati(IdComune, CodiceAttivita );

			return modelli;
		}


		public ModelloDinamicoBase GetModelloDinamicoDaId(int idModello, int indice)
		{
			var dap = new AttivitaDyn2DataAccessFactory(AuthenticationInfo, this._schedeDinamicheAttivitaService, this.CodiceAttivita);
			var loader = new ModelloDinamicoLoader(dap, IdComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Backoffice);
			return new ModelloDinamicoAttivita(loader, idModello, indice, false);
		}

		public void AggiungiScheda(int idModello)
		{
			_schedeDinamicheAttivitaService.AggiungiSchedaDinamicaAdAttivita(CodiceAttivita, idModello);
		}

		public void EliminaScheda(int idModello)
		{
			_schedeDinamicheAttivitaService.EliminaModello(CodiceAttivita, idModello);
		}

		public void Close(object sender, EventArgs e)
		{
			base.CloseCurrentPage();
		}

		public List<int> GetListaIndiciScheda(int idModello)
		{
			var mgr = new IAttivitaDyn2ModelliTMgr(Database);
			return mgr.GetListaIndiciScheda(IdComune, idModello , CodiceAttivita );
		}

		public IMascheraSolaLettura GetMascheraSolaLettura(int idModello)
		{
			//var mgr = new IAttivitaDyn2DatiMgr(Database);
			var listaCampi = _schedeDinamicheAttivitaService.GetCampiModelloPresentiAncheNelleIstanze(CodiceAttivita, idModello);

			return new MascheraSolaLetturaDaId(listaCampi);
		}

		#endregion

		public string GetUrlPaginaStorico(int idModello)
		{
			string url = "~/Istanze/DatiDinamici/Storico/AttivitaDyn2Storico.aspx?Token={0}&CodiceAttivita={1}&IdModello={2}";

			return ResolveClientUrl(String.Format(url, Token, CodiceAttivita, idModello));
		}

		public bool VerificaEsistenzaStorico(int idModello)
		{
			var mgr = new IAttivitaDyn2ModelliTStoricoMgr(Database);
			int cnt = mgr.ContaRigheStorico(IdComune, idModello, CodiceAttivita);

			return cnt > 0;
		}

		public IEnumerable<KeyValuePair<string, string>> GetListaSoftwareAttivita()
		{
			// Logica estrazione del software dall'id attività:
			// - A partire dal software dell'ultima istanza dell'attività (codiceistanzaultima) leggo il valore del parametro GRUPPOSOFTWARE della verticalizzazione I_ATTIVITA
			// - Se la verticalizzazione non è attiva o se grupposoftware non è valorizzato
			//		- Restituisco la lista di tutti i software attivi nell'installazione + il software TT (con il testo "Archivi di base")
			// - Se la verticalizzazione è attiva e il parametro è valorizzato
			//		- Restituisco la lista dei sw contenuti nel parametro
			var mgr = new IAttivitaMgr(this.Database);

			var list = mgr.GetSoftwareAttiviDaIdAttivita(this.IdComune, this.IdComuneAlias, this.CodiceAttivita);

			list.Add(new Software { CODICE = "TT", DESCRIZIONE = "Archivi di base" });

			return list.Select( x => new KeyValuePair<string,string>( x.CODICE , x.DESCRIZIONE ));


		}

		[WebMethod()]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static object GetListaModelliDisponibili(string token, int codice, string partial, string software)
		{
			var authInfo = AuthenticationManager.CheckToken(token);

			if (authInfo == null)
				return new object[] { new { label = -1, value = "Token non valido" } };

			using (var db = authInfo.CreateDatabase())
			{
				return new IAttivitaDyn2ModelliTMgr(db).GetModelliNonUtilizzati(authInfo.IdComune, codice, partial, software)
													   .Select(x => new { label = x.Descrizione, value = x.Codice });
			}
		}
	}
}
