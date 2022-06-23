<%@ Page Title="Dettagli firma digitale" Language="C#" MasterPageFile="~/AreaRiservataMaster.Master" AutoEventWireup="true" CodeBehind="download-firmato.aspx.cs" Inherits="Init.Sigepro.FrontEnd.oggetti.download_firmato" %>
<%@ MasterType VirtualPath="~/AreaRiservataMaster.Master" %>
<%@ Register TagPrefix="ar" Namespace="Init.Sigepro.FrontEnd.WebControls.FormControls" Assembly="Init.Sigepro.FrontEnd.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="inputForm">

	<asp:Repeater runat="server" ID="rptEsitiVerificaFirma">
		<ItemTemplate>
			<fieldset>
				<legend>Certificato di firma <%# (Container.ItemIndex + 1)%></legend>
				<ar:LabeledLabel runat="server" id="lblSoggetto" Label="Soggetto" Value='<%# Bind("Soggetto") %>' />
				<ar:LabeledLabel runat="server" id="lblEmittente" Label="Emittente" Value='<%# Bind("Emittente") %>' />
				<ar:LabeledLabel runat="server" id="lblValidoDal" Label="Valido Dal" Value='<%# Bind("ValidoDal") %>'/>
				<ar:LabeledLabel runat="server" id="lblValidoAl" Label="Valido Al" Value='<%# Bind("ValidoAl") %>'/>
				<ar:LabeledLabel runat="server" id="lblSeriale" Label="Seriale" Value='<%# Bind("Seriale") %>'/>
				<ar:LabeledLabel runat="server" id="LabeledLabel1" Label="Firma valida" Value='<%# Eval("EsitoVerificaFirma").ToString() == "True" ? "Si" : "No" %>'/>
				<ar:LabeledLabel runat="server" id="LabeledLabel2" Label="Revocato" Value='<%# Eval("EsitoVerificaRevoca").ToString() == "True" ? "No" : "Si" %>'/>
			</fieldset>
			<fieldset>
				<legend>Verifica revoca certificati</legend>
				<asp:Repeater runat="server" ID="rptVerificaCertificati" DataSource='<%# Bind("DettaglioVerificaRevoca") %>'>
					<HeaderTemplate>
						<table class="table table-striped">
							<thead>
								<tr>
									<th>Soggetto</th>
									<th>Emittente</th>
									<th>Verifica validità al tempo della firma</th>
									<th>Stato</th>
									<th>Data di revoca</th>
								</tr>
							</thead>
							<tbody>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td><asp:Literal ID="Literal0" runat="server" Text='<%# Bind("Soggetto") %>' /></td>
							<td><asp:Literal ID="Literal1" runat="server" Text='<%# Bind("Emittente") %>' /></td>
							<td><asp:Literal ID="Literal2" runat="server" Text='<%# Eval("VerificaValiditaAlTempoDellaFirma").ToString() == "True" ? "Valido" : "Non valido" %>' /></td>
							<td><asp:Literal ID="Literal3" runat="server" Text='<%# Eval("Revocato").ToString() == "True" ? "Revocato" : "Valido" %>' /></td>
							<td><asp:Literal ID="Literal4" runat="server" Text='<%# Eval("DataDiRevoca") %>' /></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
							</tbody>
						</table>
					</FooterTemplate>
				</asp:Repeater>
			</fieldset>
		
		</ItemTemplate>
	</asp:Repeater>

		<fieldset>	
			<div>
				<asp:Button runat="server" ID="lnkDownloadFileNoFirma" CssClass="btn btn-primary" Text="Scarica file senza firma digitale" OnClick="lnkDownloadFileNoFirma_Click" />
				<asp:Button runat="server" ID="lnkDownloadFile" CssClass="btn btn-primary" Text="Scarica file originale" OnClick="lnkDownloadFile_Click" />
				<asp:Button runat="server" ID="cmdClose" CssClass="btn btn-default" Text="Chiudi" OnClick="cmdClose_Click" />
			</div>
		</fieldset>
	</div>
</asp:Content>
