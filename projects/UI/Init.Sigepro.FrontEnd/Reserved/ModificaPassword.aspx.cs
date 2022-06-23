using Init.Sigepro.FrontEnd.AppLogic.GestioneAnagrafiche;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class ModificaPassword : ReservedBasePage
    {
        [Inject]
        public IAnagraficheService _anagrafeRepository { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string idComune = UserAuthenticationResult.IdComune;
                int codiceAnagrafe = UserAuthenticationResult.DatiUtente.Codiceanagrafe.GetValueOrDefault(-1);
                string vecchiaPassword = txtVecchiaPassword.Text;
                string nuovaPassword = txtNuovaPassword.Text;
                string confermaPassword = txtConfermaNuovaPassword.Text;

                _anagrafeRepository.ModificaPassword(idComune, codiceAnagrafe, vecchiaPassword, nuovaPassword, confermaPassword);

                ClientScript.RegisterStartupScript(this.GetType(), "startup", "alert('Password modificata correttamente')", true);
            }
            catch (Exception ex)
            {
                Errori.Add(ex.Message);
            }
        }
    }
}
