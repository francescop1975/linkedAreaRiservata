using Init.SIGePro.Authentication;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Mercati;
using SIGePro.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigepro.net.DatiDinamici.Mercati
{
    public partial class mercati_dyn2_modelli : BasePage
    {
        public int CodiceMercato => Convert.ToInt32(Request.QueryString["codicemercato"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.TabSelezionato = IntestazionePaginaTipiTabEnum.Scheda;

            if (!IsPostBack)
            {
                BindTitoloPagina();
            }
        }

        private void BindTitoloPagina()
        {
            var mercato = new MercatiMgr(this.Database).GetById(this.IdComune, this.CodiceMercato);

            this.Title = $"Schede di \"{mercato.Descrizione}\"";
        }

        public List<ElementoListaModelli> GetListaModelli()
        {
             //new IstanzeDyn2ModelliTMgr(Database).GetModelliIstanza(IdComune, Convert.ToInt32(CodiceIstanza), CodiceMovimento);

            return new MercatiDyn2ModelliService(Database, this.IdComune).GetModelliCollegati(CodiceMercato);
        }

        public ModelloDinamicoBase GetModelloDinamicoDaId(int idModello, int indice)
        {
            var dap = new MercatiDyn2DataAccessFactory(Database, this.IdComune, this.CodiceMercato);
            var loader = new ModelloDinamicoLoader(dap, IdComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Backoffice);
            return new ModelloDinamicoMercato(loader, idModello, indice, false);
        }

        public void AggiungiScheda(int idModello)
        {
            new MercatiDyn2ModelliService(Database, this.IdComune).AggiungiModelloAMercato(CodiceMercato, idModello);
        }

        public void EliminaScheda(int idModello)
        {
            new MercatiDyn2ModelliService(Database, this.IdComune).EliminaScheda(CodiceMercato, idModello);
        }

        public List<int> GetListaIndiciScheda(int idModello)
        {
            var mgr = new MercatiDyn2ModelliService(Database, IdComune);
            return mgr.GetListaIndiciScheda(this.CodiceMercato, idModello);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetListaModelliDisponibili(string token, int codice, string partial, bool cercaTT, string codiceMovimento = "")
        {
            var authInfo = AuthenticationManager.CheckToken(token);

            if (authInfo == null)
                return new object[] { new { label = -1, value = "Token non valido" } };

            using (var db = authInfo.CreateDatabase())
            {
                return new MercatiDyn2ModelliService(db, authInfo.IdComune).GetModelliNonUtilizzati(codice, partial, cercaTT)
                                                     .Select(x => new { label = x.Descrizione, value = x.Codice });
            }
        }


        public bool VerificaEsistenzaStorico(int idModello)
        {
            return false;
        }

        public void Close(object sender, EventArgs e)
        {
            base.CloseCurrentPage();
        }
    }
}