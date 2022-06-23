using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.EndoAcquisiti;
using Init.Sigepro.FrontEnd.AppLogic.ObjectSpace.PresentazioneIstanza;
using Init.Sigepro.FrontEnd.WebControls.FormControls;
using Init.SIGePro.Manager.DTO.Endoprocedimenti;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class GestioneEndoPresenti : IstanzeStepPage
    {
        [Inject]
        public IEndoAcquisitiService _endoprocedimentiService { get; set; }


        private Dictionary<int, IEnumerable<TipiTitoloDto>> _tipiTitoloDictionary;

        public Dictionary<int, IEnumerable<TipiTitoloDto>> TipiTitoloDictionary
        {
            get
            {
                if (_tipiTitoloDictionary == null)
                {
                    var q = from r in ReadFacade.Domanda.Endoprocedimenti.Secondari
                            where r.PermetteVerificaAcquisizione
                            select r.Codice;

                    _tipiTitoloDictionary = _endoprocedimentiService.TipiTitoloWhereCodiceInventarioIn(q.ToList());
                }


                return _tipiTitoloDictionary;
            }

        }




        protected void Page_Load(object sender, EventArgs e)
        {
            // Il service si occupa del salvataggio dei dati
            Master.IgnoraSalvataggioDati = true;

            if (!IsPostBack)
                DataBind();
        }

        #region Ciclo di vita dello step

        public override void OnInitializeStep()
        {
        }

        public override bool CanEnterStep()
        {
            return ReadFacade.Domanda.Endoprocedimenti.Endoprocedimenti.Where(x => x.PermetteVerificaAcquisizione).Count() > 0;
        }


        public override bool CanExitStep()
        {
            return this.Errori.Count == 0;
        }

        public override void OnBeforeExitStep()
        {
            var listaEndoPresenti = new List<DatiEndoprocedimentoPresente>();

            foreach (RepeaterItem rptItem in rptEndo.Items)
            {
                var hidIdEndo = (HiddenField)rptItem.FindControl("hidIdEndo");
                var chkPresente = (CheckBox)rptItem.FindControl("chkPresente");
                var txtNumeroAtto = (Init.Sigepro.FrontEnd.WebControls.FormControls.TextBox)rptItem.FindControl("txtNumeroAtto");
                var txtDataAtto = (DateTextBox)rptItem.FindControl("txtDataAtto");
                var ltrNomeEndo = (Literal)rptItem.FindControl("ltrNomeEndo");
                var ddlTipiTitolo = (Init.Sigepro.FrontEnd.WebControls.FormControls.DropDownList)rptItem.FindControl("ddlTipiTitolo");
                var txtRilasciatoDa = (Init.Sigepro.FrontEnd.WebControls.FormControls.TextBox)rptItem.FindControl("txtRilasciatoDa");
                var txtNote = (Init.Sigepro.FrontEnd.WebControls.FormControls.TextBox)rptItem.FindControl("txtNote");

                if (!chkPresente.Checked)
                    continue;

                listaEndoPresenti.Add(new DatiEndoprocedimentoPresente(
                                            chkPresente.Checked,
                                            Convert.ToInt32(hidIdEndo.Value),
                                            ltrNomeEndo.Text,
                                            ddlTipiTitolo.SelectedValue,
                                            ddlTipiTitolo.SelectedItem.Text,
                                            txtNumeroAtto.Text,
                                            txtDataAtto.Text,
                                            txtRilasciatoDa.Text,
                                            txtNote.Text));
            }

            var errori = _endoprocedimentiService.VerificaCorrettezzaListaEndoAcquisiti(listaEndoPresenti);

            if (errori.Count() > 0)
            {
                this.Errori.AddRange(errori);
                return;
            }

            _endoprocedimentiService.SalvaEndoAcquisiti(IdDomanda, listaEndoPresenti);
        }
        #endregion

        public class EndoPresenteBindingItem
        {
            public int CodiceInventario { get; set; }
            public string Descrizione { get; set; }
            public bool Presente { get; set; }
            public bool TipoTitoloObbligatorio { get; set; }
            public bool TipoTitoloNonObbligatorio { get { return !TipoTitoloObbligatorio; } }
            public IEnumerable<TipiTitoloDto> TitoliSupportati { get; set; }
            public int IdTipoTitoloSelezionato { get; set; }
            public string NumeroAtto { get; set; }
            public string DataAtto { get; set; }
            public string RilasciatoDa { get; set; }
            public string Note { get; set; }
            public string StileCss { get; set; }

        }

        protected void rptEndo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            var row = (PresentazioneIstanzaDataSet.ISTANZEPROCEDIMENTIRow)e.Item.DataItem;

            var ddlTipiTitolo = (Init.Sigepro.FrontEnd.WebControls.FormControls.DropDownList)e.Item.FindControl("ddlTipiTitolo");
            var ltrTipoTitolo = (Literal)e.Item.FindControl("ltrTipoTitolo");

            var listaTipiTitolo = TipiTitoloDictionary[row.CODICEINVENTARIO].ToList();

            if (listaTipiTitolo.Count() == 0)
            {
                ddlTipiTitolo.Visible = false;
                ltrTipoTitolo.Visible = false;
            }
            else
            {
                listaTipiTitolo.Insert(0, new TipiTitoloDto { Codice = -1, Descrizione = "" });

                if (!row.IsTipoTitoloNull())
                    ddlTipiTitolo.SelectedValue = row.TipoTitolo.ToString();

                ddlTipiTitolo.DataSource = listaTipiTitolo;
                ddlTipiTitolo.DataBind();
            }
        }

        protected string VisualizzaAsterisco(int binarioDipendenzeEndoPrincipale, int binarioDipendenzeEndo)
        {
            if (binarioDipendenzeEndoPrincipale < 0)
                return String.Empty;

            if ((binarioDipendenzeEndoPrincipale & binarioDipendenzeEndo) != binarioDipendenzeEndo)
                return "*";

            return String.Empty;
        }

        private IEnumerable<TipiTitoloDto> GetTipiTitoloPerEndo(int codiceInventario)
        {
            var listaTitoli = TipiTitoloDictionary[codiceInventario].ToList();

            listaTitoli.Insert(0, new TipiTitoloDto { Codice = -1, Descrizione = String.Empty });
            listaTitoli.Sort((x, y) => x.Descrizione.CompareTo(y.Descrizione));

            return listaTitoli;
        }

        public override void DataBind()
        {
            var endoPrincipale = ReadFacade.Domanda.Endoprocedimenti.Principale;

            var q = from r in ReadFacade.Domanda.Endoprocedimenti.Secondari
                    where r.PermetteVerificaAcquisizione
                    select new EndoPresenteBindingItem
                    {
                        CodiceInventario = r.Codice,
                        DataAtto = r.Riferimenti == null || !r.Riferimenti.DataAtto.HasValue ? String.Empty : r.Riferimenti.DataAtto.Value.ToString("dd/MM/yyyy"),
                        Descrizione = VisualizzaAsterisco(endoPrincipale == null ? -1 : endoPrincipale.BinarioDipendenze, r.BinarioDipendenze) + " " + r.Descrizione,
                        IdTipoTitoloSelezionato = r.Riferimenti == null ? -1 : r.Riferimenti.TipoTitolo.Codice,
                        Note = r.Riferimenti == null ? String.Empty : r.Riferimenti.Note,
                        NumeroAtto = r.Riferimenti == null ? String.Empty : r.Riferimenti.NumeroAtto,
                        Presente = r.TipoTitoloObbligatorio ? true : r.Presente,
                        TipoTitoloObbligatorio = r.TipoTitoloObbligatorio,
                        RilasciatoDa = r.Riferimenti == null ? String.Empty : r.Riferimenti.RilasciatoDa,
                        TitoliSupportati = GetTipiTitoloPerEndo(r.Codice)
                    };

            rptEndo.DataSource = q;
            rptEndo.DataBind();
        }
    }
}
