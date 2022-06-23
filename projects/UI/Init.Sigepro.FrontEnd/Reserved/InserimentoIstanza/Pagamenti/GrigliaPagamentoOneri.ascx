﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GrigliaPagamentoOneri.ascx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Pagamenti.GrigliaPagamentoOneri" %>
<%@ Register TagPrefix="cc1" Assembly="Init.Utils" Namespace="Init.Utils.Web.UI" %>

<thead>
	<tr>
		<th><%=EtichettaColonnaCausale%></th>
		<th style="text-align: center">Note</th>
		<th style="text-align: center">Pagamento</th>
		<th style="text-align: center">Tipo pagamento</th>
		<th style="text-align: center">Data</th>
		<th style="text-align: center">Riferimenti Pagamento</th>
		<th style="text-align: center">Importo</th>
	</tr>
</thead>
<asp:Repeater runat="server" ID="rptOneri" OnItemDataBound="rptOneri_ItemDataBound">
	<ItemTemplate>
		<tr class="riga-onere">
			<td>
				<asp:HiddenField runat="server" ID="hidIdOnere" Value='<%# Eval("CodiceCausale")%>' />
				<asp:HiddenField runat="server" ID="hidCodiceEndoOIntervento" Value='<%# Eval("CodiceEndoOInterventoOrigine")%>' />
				<span class="descrizioneCausale"><asp:Literal runat="server" Text='<%# Eval("Causale")%>' id="lblNomeOnere" /></span>
				<div class="descrizioneIntervento">[<%# Eval("EndoOInterventoOrigine")%>]</div>
			</td>
			<td class="helpNoteOnereRow">
				<a href="#" class="helpNoteOnere">
					<img src="../../../Images/help.png" alt="l'onere contiene note" />
				</a>
				<span class="noteOnere"><%# Eval("Note")%></span>
			</td>
			<td style="text-align:center;width: 140px;">
                <asp:DropDownList runat="server" id="ddlPagamento" class="ddlPagamento form-control" data-pagamento-completato='<%#Eval("PagamentoCompletato")%>'>
                    <asp:ListItem Value="1" Text="Online" />
                    <asp:ListItem Value="2" Text="Effettuato" />
                    <asp:ListItem Value="0" Text="Non dovuto" />
                </asp:DropDownList>
			</td>
			<td style="text-align: center"><asp:DropDownList runat="server" ID="ddlTipoPagamento" CssClass="estremiPagamento ddlTipoPagamento form-control" DataTextField="Descrizione" DataValueField="Codice"  /></td>
			<td style="text-align: center">
				<asp:TextBox runat="server" ID="txtDataPagamento" Type="date" CssClass="estremiPagamento txtDataPagamento form-control" Text='<%# Eval("DataPagamento", "{0:yyyy-MM-dd}") %>' style="line-height: unset;" />
			</td>
			<td><asp:TextBox runat="server" ID="txtNumeroOperazione" CssClass="estremiPagamento txtNumeroOperazione form-control"  Text='<%# Eval("RiferimentoPagamento") %>'/></td>
			<td style="text-align:right">
				<cc1:FloatTextBox runat="server" ID="txtImporto" CssClass="importoPagato estremiPagamento form-control" ValoreFloat='<%# Convert.ToSingle(Eval("ImportoPagato"))%>' Columns="8" ReadOnly='<%# Convert.ToSingle(DataBinder.Eval(Container.DataItem,"Importo")) > 0.0f %>' />
			</td>
		</tr>
	</ItemTemplate>
</asp:Repeater>