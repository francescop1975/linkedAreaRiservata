using System;
using System.Linq;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Init.Sigepro.FrontEnd.AppLogic.Readers;
using Init.Utils;
using System.Collections.Generic;

using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Ninject;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.Sigepro.FrontEnd.AppLogic.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni;
using System.Text;
using Init.Sigepro.FrontEnd.WebControls.Common.RicercheStradario;
using Init.Sigepro.FrontEnd.Infrastructure.IOC;

namespace Init.Sigepro.FrontEnd.WebControls.Common
{
    public static class StradarioExtensions
    {
        public static StradarioDto ToStradarioDto(this Stradario x)
            {
                if (x == null)
                {
                    return null;
                }

                var sb = new StringBuilder(x.DESCRIZIONE);

                if (!String.IsNullOrEmpty(x.LOCFRAZ))
                {
                    sb.Append(" - ").AppendFormat(x.LOCFRAZ);
                }

                if (!String.IsNullOrEmpty(x.CAP))
                {
                    sb.Append(" (").Append(x.CAP).Append(")");
                }

                return new StradarioDto
                {
                    CodiceStradario = Convert.ToInt32(x.CODICESTRADARIO),
                    NomeVia = sb.ToString()
                };
            }
    }
    
    /// <summary>
	/// Descrizione di riepilogo per RicercaStradario.
	/// </summary>
	[ToolboxData("<{0}:RicercaStradario runat=server></{0}:RicercaStradario>")]
	public class RicercaStradario : SearchWebControl
	{
		[Inject]
		public IStradarioRepository _stradarioRepository { get; set; }


		private TextBox m_textBox = new TextBox();
		private DataGrid m_dataGrid = new DataGrid();
		private Button m_button = new Button();
		private Button m_cancelButton = new Button();
		private Label m_label = new Label();
		private Label m_errorLabel = new Label();


		public string CodiceComune
		{
			get
			{
				object o = this.ViewState["CodiceComune"];
				return o == null ? "" : o.ToString();
			}
			set { this.ViewState["CodiceComune"] = value; }
		}

		public int Value
		{
			get
			{
				object o = this.ViewState["Value"];
				return o == null ? -1 : (int) o;
			}
			set { this.ViewState["Value"] = value; }
		}

		public RicercaStradario()
		{
			FoKernelContainer.Inject(this);

			// inizializzazione dei controlli
			m_textBox.Text = "";
            m_textBox.Attributes.Add("placeholder","Inserire il nome della via e fare click su \"Cerca via\"");
			m_textBox.Columns = 50;
			m_label.Text = "";
			m_button.Text = "Cerca via";
			m_errorLabel.ForeColor = Color.Red;
			m_errorLabel.Text = "Indirizzo non trovato";
			m_cancelButton.Text = "Nuova ricerca";

			// inizializzazione della datagrid
			m_dataGrid.Attributes.Add("width", "100%");
			m_dataGrid.AutoGenerateColumns = false;
			m_dataGrid.DataKeyField = "CodiceStradario";

			BoundColumn descrStradarioColumn = new BoundColumn();
			descrStradarioColumn.DataField = "NomeVia";
			descrStradarioColumn.HeaderText = "Via";

			//BoundColumn localitaColumn = new BoundColumn();
			//localitaColumn.DataField = "LocFraz";
			//localitaColumn.HeaderText = "Localit&agrave;/Frazione";

			//BoundColumn capColumn = new BoundColumn();
			//capColumn.DataField = "Cap";
			//capColumn.HeaderText = "Cap";

			ButtonColumn btncolumn = new ButtonColumn();
			btncolumn.Text = "Seleziona";
			btncolumn.ButtonType = ButtonColumnType.LinkButton;
			btncolumn.CommandName = "Select";

			m_dataGrid.Columns.Add(descrStradarioColumn);
			//m_dataGrid.Columns.Add(localitaColumn);
			//m_dataGrid.Columns.Add(capColumn);
			m_dataGrid.Columns.Add(btncolumn);


			// textbox di ricerca
			m_textBox.TextChanged += new EventHandler(m_button_Click);
			m_textBox.AutoPostBack = true;

			this.Init += new EventHandler(RicercaStradario_Init);
			this.Load += new EventHandler(RicercaStradario_Load);
		}

		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			this.Controls.Add(m_textBox);
			this.Controls.Add(m_dataGrid);
			this.Controls.Add(m_button);
			this.Controls.Add(m_label);
			this.Controls.Add(m_errorLabel);
			this.Controls.Add(m_cancelButton);
		}

		private void RicercaStradario_Init(object sender, EventArgs e)
		{
			EnsureChildControls();
			m_button.Click += new EventHandler(m_button_Click);
			m_cancelButton.Click += new EventHandler(m_cancelButton_Click);
			m_dataGrid.SelectedIndexChanged += new EventHandler(m_dataGrid_SelectedIndexChanged);
		}

		private void m_button_Click(object sender, EventArgs e)
		{
            // TODO: modificare le ricerche
            var searchProvider = new CompositeRicercaIndirizzo(new List<IRicercaIndirizzo>
            {
                new RicercaEsatta(this._stradarioRepository, IdComune, CodiceComune),
                new RicercaEsattaConCodiceComune(this._stradarioRepository, IdComune),
                new RicercaParziale(this._stradarioRepository, IdComune, CodiceComune),
                new RicercaParzialeConCodiceComune(this._stradarioRepository, IdComune)
            });
            var testoStradario = m_textBox.Text;

            if (testoStradario.Length == 0) return;

            var result = searchProvider.Cerca(testoStradario);

            if (result.Count() == 0)
            {
                m_errorLabel.Visible = true;
                return;
            }


            if (result.Count() == 1)
            {
                SetStradario(result.First());
            }
            else
            {

                m_dataGrid.DataSource = result;
                m_dataGrid.DataBind();

                m_dataGrid.Visible = true;
                m_cancelButton.Visible = true;
                m_errorLabel.Visible = false;
                m_errorLabel.Visible = false;
                m_textBox.Visible = false;
                m_button.Visible = false;
            }
		}

		protected void SetStradario(StradarioDto indirizzo)
		{
			Value = Convert.ToInt32(indirizzo.CodiceStradario);
			m_label.Text = indirizzo.NomeVia;
            m_label.Text += "&nbsp;";

			m_textBox.Visible = false;
			m_button.Visible = false;
			m_errorLabel.Visible = false;
			m_dataGrid.Visible = false;
			m_label.Visible = true;
			m_cancelButton.Visible = true;
		}

		private void m_dataGrid_SelectedIndexChanged(object sender, EventArgs e)
		{
			var idStradario = Convert.ToInt32(m_dataGrid.DataKeys[m_dataGrid.SelectedIndex]);

			var indirizzo = _stradarioRepository.GetByCodiceStradario( IdComune , idStradario);

			SetStradario(indirizzo.ToStradarioDto());
		}

		private void m_cancelButton_Click(object sender, EventArgs e)
		{
			m_textBox.Visible = true;
			m_dataGrid.Visible = false;
			m_label.Visible = false;
			m_cancelButton.Visible = false;
			m_button.Visible = true;
			m_errorLabel.Visible = false;

			this.Value = -1;
		}

		private void RicercaStradario_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				m_textBox.Visible = true;
				m_dataGrid.Visible = false;
				m_label.Visible = false;
				m_cancelButton.Visible = false;
				m_button.Visible = true;
				m_errorLabel.Visible = false;
			}

            m_button.OnClientClick = "(function () {document.getElementById(\"" + m_button.ClientID + "\").value=\'Ricerca in corso ...\'; return true;}())";
		}

		protected override void Render(HtmlTextWriter writer)
		{
			RenderChildren(writer);
		}

	}
}