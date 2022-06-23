﻿<%@ Page Language="C#" MasterPageFile="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" AutoEventWireup="true" CodeBehind="GestioneDatiDinamici.aspx.cs"
    Inherits="Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.GestioneDatiDinamici" Title="Untitled page" %>

<%@ MasterType VirtualPath="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" %>
<%@ Register TagPrefix="dd" Namespace="Init.SIGePro.DatiDinamici.WebControls" Assembly="SIGePro.DatiDinamici" %>
<%@ Register TagPrefix="d2" Src="~/Reserved/InserimentoIstanza/DatiDinamici/PaginatoreSchedeDinamiche.ascx" TagName="PaginatoreSchedeDinamiche" %>
<%@ Register TagPrefix="ar" Assembly="Init.Sigepro.FrontEnd.WebControls" Namespace="Init.Sigepro.FrontEnd.WebControls.FormControls" %>

<%@ Register TagPrefix="cc1" Namespace="Init.Utils.Web.UI" Assembly="Init.Utils" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/lib/jquery.form.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/lib/jquery.tooltip.fix.js")%>'></script>

    <script type="text/javascript">
        $(function () {
            $(".righeMultiple").closest(".tabellaDatiDinamici").css("width", "100%");
        });
    </script>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="stepContent" runat="server">


    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div id="datiDinamici" class="gestione-dati-dinamici">

        <asp:MultiView runat="server" ID="multiView" ActiveViewIndex="0">
            <asp:View runat="server" ID="selezioneView">

                <div class="attributi-allegato legenda">
                    <div>
                        <div class="help-block">
                            <i class="glyphicon glyphicon-pencil"></i>= Scheda non ancora compilata
                        </div>
                        <div class="help-block">
                            <i class="glyphicon glyphicon-ok"></i>= Scheda compilata con successo
                        </div>
                    </div>
                </div>
                <div class="lista-schede-richieste">
                    <asp:Panel runat="server" ID="pnlSchedaCittadiniExtracomunitari">
                        <ul>
                            <li>
                                <b>
                                    <asp:Literal runat="server" ID="lblTestoSchedaCittadiniExtracomunitari" />
                                </b>
                                <ul>
                                    <li class="elementoListaDatiDinamici <%= GetClasseCssSchedaDinamicaCompilata( IsSchedaDinamicaEcCompilata ) %>">

                                        <%if (IsSchedaDinamicaEcCompilata)
                                            {%>
                                        <i class="glyphicon glyphicon-ok"></i>
                                        <%}
                                            else
                                            { %>
                                        <i class="glyphicon glyphicon-pencil"></i>
                                        <%} %>

                                        <asp:LinkButton runat="server" ID="lnkEcSelezioneSchedaIntervento" OnClick="OnSchedaSelezionata" />
                                        <asp:Literal runat="server" ID="ltrEcAsterisco" />
                                    </li>
                                </ul>
                            </li>

                        </ul>
                    </asp:Panel>

                    <ul>
                        <asp:Repeater runat="server" ID="rptSchedeIntervento">
                            <HeaderTemplate>
                                <li>
                                    <% if (MostraTitoloSchedeIntervento)
                                    { %>
                                    <b><%=TestoSchedeDellIntervento %> "<%=ReadFacade.Domanda.AltriDati.Intervento%>"</b>
                                    <%} %>
                                    <ul>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <li class="elementoListaDatiDinamici <%# DataBinder.Eval( Container.DataItem , "Compilata" )%>">
                                    <asp:Literal runat="server" ID="ltrCompilata" Text='<%# String.Format("<i class=\"glyphicon glyphicon-{0}\"></i>", (DataBinder.Eval( Container.DataItem , "Compilata" ).ToString() == "compilato") ? "ok": "pencil")%>' />
                                    <asp:LinkButton runat="server" ID="lnkSelezioneSchedaIntervento" Text='<%# Eval("Descrizione") %>' CommandArgument='<%#Eval("Codice") %>' OnClick="OnSchedaSelezionata" />
                                    <asp:Literal runat="server" ID="ltrAsterisco" Text='<%# !Convert.ToBoolean(Eval("Facoltativa")) ? "*" : " " %>' />
                                </li>
                            </ItemTemplate>

                            <FooterTemplate>
                                </ul>
							    </li>		
                            </FooterTemplate>
                        </asp:Repeater>


                        <asp:Repeater runat="server" ID="rptEndoprocedimenti">
                            <ItemTemplate>
                                <li>
                                    <b>
                                        <asp:Literal runat="server" ID="ltrNomeEndo" Text='<%# Eval("DescrizioneIntervento") %>' /></b>
                                    <ul>
                                        <asp:Repeater runat="server" ID="rptSchedeEndo" DataSource='<%# Eval("Schede") %>'>
                                            <ItemTemplate>
                                                <li class="elementoListaDatiDinamici <%# DataBinder.Eval( Container.DataItem , "Compilata" ) %>">
                                                    <asp:Literal runat="server" ID="ltrCompilata" Text='<%# String.Format("<i class=\"glyphicon glyphicon-{0}\"></i>", (DataBinder.Eval( Container.DataItem , "Compilata" ).ToString() == "compilato") ? "ok": "pencil")%>' />
                                                    <asp:LinkButton runat="server" ID="lnkSelezioneSchedaIntervento" Text='<%# Eval("Descrizione") %>' CommandArgument='<%#Eval("Codice") %>' OnClick="OnSchedaSelezionata" />
                                                    <asp:Literal runat="server" ID="ltrAsterisco" Text='<%# !Convert.ToBoolean(Eval("Facoltativa")) ? "*" : " " %>' />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </li>
                            </ItemTemplate>

                        </asp:Repeater>
                    </ul>
                </div>

                <i><%=TestoCompilazioneSchedeObbligatorie %></i>
            </asp:View>

            <asp:View runat="server" ID="dettaglioView">
<%--                <script type="text/javascript" defer src='<%= ResolveClientUrl("~/reserved/inserimentoistanza/datidinamici/GetterSetterDatiDinamici.js")%>'></script>
                <script type="text/javascript" defer src='<%= ResolveClientUrl("~/reserved/inserimentoistanza/datidinamici/D2FocusManager.js")%>'></script>
                <script type="text/javascript" defer src='<%= ResolveClientUrl("~/reserved/inserimentoistanza/datidinamici/D2PannelloErrori.js")%>'></script>
                <script type="text/javascript" defer src='<%= ResolveClientUrl("~/reserved/inserimentoistanza/datidinamici/DatiDinamiciExtender.js")%>'></script>
                <script type="text/javascript" defer src='<%= ResolveClientUrl("~/reserved/inserimentoistanza/datidinamici/DescrizioneControllo.js")%>'></script>
                
                <script type="text/javascript" src='<%= ResolveClientUrl("~/reserved/inserimentoistanza/datidinamici/jQuery.searchDatiDinamici.js")%>'></script>
                <script type="text/javascript" src='<%= ResolveClientUrl("~/reserved/inserimentoistanza/datidinamici/jquery.uploadDatiDinamici.js")%>'></script>--%>
                <script type="text/javascript">

                    function fixAggiungiRiga() {
                        var button = $('tr.bloccoMultiploAggiungiRiga>td>input');
                        var el = $('<i />', { 'class': 'ion-android-add-circle' });

                        button.addClass('aggiungiRiga');
                        button.before(el);
                    }

                    function fixAggiungiBlocco() {
                        var button = $('#datiDinamici input[name*=btnAggiungi][type=submit][value=\'Aggiungi\']');

                        var el = $('<i />', { 'class': 'ion-android-add-circle' });

                        button.addClass('aggiungiRiga');
                        button.before(el);
                    }

                    function fixEliminaRiga() {
                        var button = $('.divEliminazioneBlocco>a');
                        var el = $('<i />', { 'class': 'ion-android-remove-circle' });

                        button.addClass('eliminaRiga');
                        button.before(el);

                    }

                    $(function () {

                        var pannelloErrori = $('#<%=pnlErrori.ClientID %>'),
                            idBottoneSalvataggio = $('.d2-bottone-salvataggio'),
                            bottoniRichiestaConferma = $('.d2-richiedi-conferma');

                        $('.d2-corpo-modello').DatiDinamiciExtender({
                            pannelloErrori: pannelloErrori,
                            bottoniSalvataggio: idBottoneSalvataggio,
                            bottoniConferma: bottoniRichiestaConferma
                        });

                        fixAggiungiRiga();
                        fixAggiungiBlocco();
                        fixEliminaRiga();

                        $(".d2DateTextBox").on("change", function () {
                            $(this).trigger("changeDate");
                        });
                    });

                </script>


                <d2:PaginatoreSchedeDinamiche runat="server" ID="paginatoreSchedeDinamiche" OnIndiceSelezionato="OnIndiceSelezionato" OnNuovaScheda="OnNuovaScheda" OnEliminaScheda="OnSchedaEliminata" />
                <div class='<%= renderer.DataSource.ModelloMultiplo ? "contenutoScheda" : String.Empty %>'>
                    <h2>
                        <asp:Label runat="server" ID="lblTitoloModello" CssClass="" />
                    </h2>
                    <asp:Panel runat="server" ID="pnlErrori" CssClass="alert alert-danger" Style="display: none"></asp:Panel>
                    <dd:ModelloDinamicoRenderer ID="renderer" runat="server" />

                    <div class="bottoni-scheda">
                        <asp:Button runat="server" ID="cmdSalvaEResta" Text="Salva" CssClass="btn btn-primary d2-bottone-salvataggio" OnClick="cmdSalvaeResta_Click" />
                        <asp:Button runat="server" ID="cmdSalva" Text="Salva e torna alla lista delle schede" CssClass="btn btn-primary d2-bottone-salvataggio" OnClick="cmdSalva_Click" />
                        <asp:Button runat="server" ID="cmdChiudi" Text="Torna alla lista delle schede senza salvare" CssClass="d2-richiedi-conferma btn btn-default" OnClick="cmdChiudi_Click" />
                    </div>
                </div>

                <ar:BootstrapModal runat="server" ExtraCssClass="d2-bootstrap-modal" ID="d2BootstrapModal" Title="Informazioni" ShowOkButton="false">
                    <ModalBody>
                        &nbsp;
                    </ModalBody>
                </ar:BootstrapModal>
            </asp:View>


        </asp:MultiView>

    </div>

</asp:Content>
