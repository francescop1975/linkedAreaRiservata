<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="allegati-pratica-antenne.ascx.cs" Inherits="Sigepro.net.Antenne.allegati_pratica_antenne" %>

<div class="card">
    <div class="card-header collapsed" id="<%=this.ClientID%>_allegati" data-toggle="collapse" data-target="#<%=this.ClientID%>_collapseAllegati" aria-expanded="false" aria-controls="collapseAllegati">
        <%=this.Titolo %>
        <span class="badge badge-pill badge-primary"><%=this.Allegati.Count() %></span>
        <i class="fa fa-chevron-down float-right"></i>
        <i class="fa fa-chevron-up float-right"></i>
    </div>

    <asp:Repeater runat="server" ID="rptAllegati">
        <HeaderTemplate>
            <ul class="list-group list-group-flush collapse" id="<%=this.ClientID%>_collapseAllegati" aria-labelledby="<%=this.ClientID%>_allegati" data-parent="#accordionAllegati">
        </HeaderTemplate>

        <ItemTemplate>
            <li class="list-group-item">

                <asp:Literal runat="server" Text='<%# Eval("Descrizione") %>' /><br />
                <small>
                    <asp:Literal runat="server" Text='<%# Eval("NomeFile") %>' />
                </small>
                <i class="fa fa-download float-right"></i>
            </li>
        </ItemTemplate>

        <FooterTemplate></ul>
        </FooterTemplate>
    </asp:Repeater>
</div>
