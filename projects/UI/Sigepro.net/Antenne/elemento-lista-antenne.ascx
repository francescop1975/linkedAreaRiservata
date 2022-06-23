<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="elemento-lista-antenne.ascx.cs" Inherits="Sigepro.net.Antenne.elemento_lista_antenne" %>
<%@ Register Src="~/Antenne/allegati-pratica-antenne.ascx" TagPrefix="uc1" TagName="allegatipraticaantenne" %>

<div class="card">
    <div class="card-body">
        <h5 class="card-title">
            <asp:Literal runat="server" ID="ltrNumeroPratica">Pratica N.123 del 01/01/2020</asp:Literal>
        </h5>
        <h6 class="card-subtitle mb-2 text-muted">
            <asp:Literal runat="server" ID="ltrNumeroProtocollo">Protocollo N.1 del 01/01/2020</asp:Literal>
        </h6>

        <div class="card-text">
            <div class="form-group">
                <label for="exampleInputEmail1">Richiedente</label>
                <asp:TextBox runat="server" ID="txtRichiedente" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="form-group">
                <label for="exampleInputEmail1">Intervento</label>
                <asp:TextBox runat="server" ID="txtIntervento" CssClass="form-control" ReadOnly="true" />
            </div>


        </div>

        <div class="lista-allegati">

            <div class="accordion" id="accordionAllegati">

                <uc1:allegatipraticaantenne runat="server" ID="allegatiIntervento" Titolo="Allegati dell'intervento" />
                <uc1:allegatipraticaantenne runat="server" ID="allegatiEndoprocedimento" Titolo="Allegati degli endoprocedimenti" />
                <uc1:allegatipraticaantenne runat="server" ID="allegatiMovimenti" Titolo="Allegati dei movimenti" />

            </div>
        </div>
    </div>
</div>
