<%@ Page Language="C#" MasterPageFile="~/AreaRiservataMaster.Master" AutoEventWireup="true" CodeBehind="Scadenzario.aspx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.Scadenzario" Title="Lista scadenze" %>

<%@ Register TagPrefix="cc1" Namespace="Init.Sigepro.FrontEnd.WebControls.Common" Assembly="Init.Sigepro.FrontEnd.WebControls" %>

<asp:Content runat="server" ContentPlaceHolderID="headPagina">
    <style>
        h4 {
            font-size: 16px;
            margin-bottom: 0;
            margin-top: 0;
            font-weight: bold;
        }

        .soggetto-pratica {
            margin-bottom: 16px;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#<%=dgScadenze.ClientID%>").DataTable({
                "aaSorting": [] // lascio l'ordinamento naturale
            });
        });
    </script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="dgScadenze" runat="server" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="CodiceScadenza"
        OnRowCommand="dgScadenze_RowCommand"
        OnSelectedIndexChanged="dgScadenze_SelectedIndexChanged"
        GridLines="None"
        CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Numero Istanza">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkDettaglio" Text='<%# Bind("NumeroIstanza") %>' CommandArgument='<%# Bind("Uuid") %>' CommandName="DettaglioIstanza"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:BoundField DataField="ModuloSoftware" HeaderText="Sportello" HtmlEncode="false" />

            <asp:TemplateField HeaderText="Soggetti">
                <ItemTemplate>
                    <div class="soggetto-pratica" runat="server" visible='<%#Eval("DatiRichiedente").ToString().Trim().Length > 0%>'>
                        <h4>Richiedente</h4>
                        <asp:Literal runat="server" Text='<%#Eval("DatiRichiedente") %>' />
                    </div>

                    <div class="soggetto-pratica" runat="server" visible='<%#Eval("DatiAzienda").ToString().Trim().Length > 0%>'>
                        <h4>Azienda</h4>
                        <asp:Literal runat="server" Text='<%#Eval("DatiAzienda") %>' />
                    </div>

                    <div class="soggetto-pratica" runat="server" visible='<%#Eval("DatiAzienda").ToString().Trim().Length > 0%>'>
                        <h4>Tecnico</h4>
                        <asp:Literal runat="server" Text='<%#Eval("DatiTecnico") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>



            <asp:BoundField DataField="DescrStatoIstanza" HeaderText="Stato Istanza" />
            <asp:BoundField DataField="DatiMovimento" HeaderText="Movimento precedente" />
            <asp:TemplateField HeaderText="Movimento in scadenza">
                <ItemTemplate>
                    <b>
                        <asp:Literal runat="server" Text='<%#Eval("DescrMovimentoDaFare") %>' /></b>
                    <asp:Literal runat="server" Text='<%#Eval("DataScadenzaStr", "<br/>Scadenza: {0}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField Text="Effettua movimento" CommandName="Select" />
        </Columns>
        <EmptyDataTemplate>
            Non sono state trovate scadenze
        </EmptyDataTemplate>
    </asp:GridView>

</asp:Content>
