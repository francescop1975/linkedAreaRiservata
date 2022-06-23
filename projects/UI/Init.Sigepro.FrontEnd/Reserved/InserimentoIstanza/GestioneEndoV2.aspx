﻿<%@ Page Title="Titolo" Language="C#" MasterPageFile="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master"
    AutoEventWireup="true" CodeBehind="GestioneEndoV2.aspx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.GestioneEndoV2" %>

<%@ MasterType VirtualPath="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" %>
<%@ Register TagPrefix="uc1" TagName="GrigliaEndo" Src="~/Reserved/InserimentoIstanza/GestioneEndoV2GrigliaEndo.ascx" %>
<%@ Register TagPrefix="ar" Namespace="Init.Sigepro.FrontEnd.WebControls.FormControls" Assembly="Init.Sigepro.FrontEnd.WebControls" %>


<asp:Content runat="server" ContentPlaceHolderID="head">

    <style>
        /*.gruppoEndo {font-size: 11px;text-transform: uppercase}*/
        #alberoEndo ul {
            margin: 0px;
            padding: 0px;
        }

        #alberoEndo li {
            margin-left: 20px;
            list-style-type: none;
            margin-bottom: 5px;
        }

        #alberoEndo > ul > li {
            margin-left: 5px;
            padding-bottom: 16px;
        }

            #alberoEndo > ul > li > ul {
                /*margin-top: 5px;*/
            }

        #alberoEndo ul {
            margin-top: 5px;
        }

        #alberoEndo li > input[type=checkbox] {
        }

        #alberoEndo span {
            font-weight: bold;
        }

        #alberoEndo .fa-question-circle {
            cursor: pointer;
            color: cornflowerblue;
        }

        .endo label {
            font-weight: normal;
            display: inline-block;
            margin-left: 6px;
        }

        .etichetta {
            font-weight: bold;
            padding-bottom: 8px;
            padding-top: 8px;
        }

        .modal-dialog {
            width: 60%;
        }

        #accordion h3 {
            font-size: 18px;
            background-image: none;
            background-color: #f5f5f5;
            border: 1px solid #ddd;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        #accordion .ui-widget-content {
            border: 1px solid #ddd;
        }
    </style>

    <%=LoadScript("~/js/app/gestione-endo-v2.js") %>

    <script type="text/javascript">
        function inizializzaContenutoAccordion(elName) {
            $(elName + ' #accordion').accordion({ header: "h3", heightStyle: 'content' });
            //$(elName + ' #accordion table TR:even').addClass('AlternatingItemStyle');
            //$(elName + ' #accordion table TR:odd').addClass('ItemStyle');
        }

        $(function () {

            var modal = $("#<%=bmDettagliEndo.ClientID%>");

            $('.albero-endo').alberoEndoprocedimenti();

            function onDettagliEndoLoaded() {

            }

            function mostraDettagliEndo(sender, id) {
                var url = '<%= ResolveClientUrl("~/Public/MostraDettagliEndo.aspx") + "?idComune=" + IdComune + "&software=" + Software + "&_ts=" + DateTime.Now.Millisecond + "&Id="%>' + id;

                sender.removeClass('fa-question-circle').addClass('fa-spinner').addClass('roteante');

                modal.find(".modal-body>div").load(url, null, function (responseText, textStatus, XMLHttpRequest) {
                    sender.removeClass('roteante')
                        .removeClass('fa-spinner')
                        .addClass('fa-question-circle');

                    $(this).find('#accordion').accordion({ header: "h3", heightStyle: 'content' });

                    modal.modal('show');
                });
            }


            $('.fa-question-circle').click(function () {
                var idEndo = $(this).data('idEndo');

                mostraDettagliEndo($(this), idEndo);
            });

            <%if (SelezioneEsclusivaEndoAttivabili){%>
                $(".endo-attivabili .endo>span>input[type=checkbox]").on("click", function () {

                    var isChecked = this.checked;

                    $(".endo-attivabili .endo>span>input[type=checkbox]").prop("checked", false);

                    this.checked = isChecked;
                })
            <%}%>


            // Deselezionando un endo che contiene sub endo si devono deselezionare anche tutti i sub endo che ci sono sotto
            document.querySelectorAll('.endo>span>input[type=checkbox]').forEach(cb => {
                cb.addEventListener('click', (e) => {
                    if (!cb.checked) {
                        cb.closest('.endo').querySelectorAll('.chk-sub-endo>input[type=checkbox]').forEach(cb2 => cb2.checked = false);
                    } else {
                        // Se seleziono un endo e uno o più sub endo sono richiesti allora devo selezionarli
                        cb.closest('.endo').querySelectorAll('.chk-sub-endo>input[type=checkbox]').forEach(cb2 => {

                            if (cb2.closest('span').dataset.richiesto === 'True') {
                                cb2.checked = true;
                            }                            
                        });
                    }
                });
            });

            // Selezionando un subendo devo selezionare anche l'endo padre
            document.querySelectorAll('.dettaglio-sub-endo input[type=checkbox]').forEach(cb => {
                cb.addEventListener('click', (e) => {

                    let checkEndo = cb.closest('.endo').querySelector('input[type=checkbox]');

                    if (cb.checked && !checkEndo.checked) {
                        checkEndo.checked = true;
                    }

                    if (!cb.checked){
                        // Se il subendo è richiesto allora non è possibile deselezionarlo
                        if (cb.closest('span').dataset.richiesto === 'True') {
                            cb.checked = true;
                        }  
                    }
                });
            });

            // Se seleziono un endo che ha subendo non obbligatori ma non seleziono nessun subendo al salvataggio
            // devo mostrare un messaggio di errore
            document.querySelector('.vai-avanti').addEventListener('click', (e) => {


                const endoSelezionati = [...document.querySelectorAll('.endo>span>input[type=checkbox]')].filter(x => x.checked).map(x => x.closest('.endo'));
                const conSubEndoNonSelezionati = endoSelezionati.map(it => ({
                    endo: it.querySelector(':scope > span > label').innerText,
                    ctrl: it.querySelector(':scope > span > input[type=checkbox]'),
                    subendo: [...it.querySelectorAll('.dettaglio-sub-endo .chk-sub-endo>input[type=checkbox]')]
                }))
                    .filter(x => x.subendo?.length)
                    .filter(x => x.subendo.filter(y => y.checked).length === 0);

                if (conSubEndoNonSelezionati.length) {
                    // TODO: Mostrare errore
                    conSubEndoNonSelezionati[0].ctrl.focus();
                    document.getElementById('nome-endo-da-selezionare').innerText = conSubEndoNonSelezionati[0].endo;
                    $('#<%=bmSelezionareSubEndo.ClientID%>').modal('show');
                    e.preventDefault();
                }

            });

            // Inizializzo eventuali endo impostati in passaggi precedenti
            const endoAttivati = window._endoAttivati;

            if (endoAttivati && _endoAttivati.endo.length > 0) {
                // Ripristino gli endo di primo livello
                endoAttivati.endo
                    .filter(x => x.idPadre === null)
                    .forEach(endoPrimoLivello => {
                        document.querySelectorAll(`.endo[data-id-endo*="${endoPrimoLivello.id}"]`)
                                .forEach(divEndo => {
                                    divEndo.querySelector('input[type=checkbox]').checked = true;

                                    let closest = divEndo.closest('.albero-chiuso');

                                    while (closest) {
                                        closest.classList.remove('albero-chiuso');
                                        closest.classList.add('albero-aperto');

                                        closest = closest.closest('.albero-chiuso');
                                    }
                                }
                        );
                    });

                // Ripristino i subprocedimenti al caricamento della pagina
                endoAttivati.endo
                    .filter(x => x.idPadre !== null)
                    .forEach(endoSecondoLivello => {
                        document.querySelectorAll(`.endo[data-id-endo*="${endoSecondoLivello.idPadre}"] .sub-endo[data-id-endo*="${endoSecondoLivello.id}"`)
                            .forEach(divEndo => {
                                divEndo.querySelector('input[type=checkbox]').checked = true;
                            }
                        );
                    });
            }


        });

    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="stepContent" runat="server">


    <div class="gestione-endo">
        <asp:MultiView runat="server" ID="multiView" ActiveViewIndex="0">
            <asp:View runat="server" ID="listaEndoView">
                <asp:Panel runat="server" ID="pnlEndoPrincipale" CssClass="pannello-endo endo-principale">
                    <fieldset class="blocco aperto">
                        <legend>
                            <asp:Literal runat="server" ID="ltrTitoloEndoPrincipale" Text="Procedimento principale" />
                        </legend>
                        <uc1:GrigliaEndo runat="server" ID="grigliaEndoPrincipale" />
                    </fieldset>
                </asp:Panel>


                <asp:Panel runat="server" ID="pnlEndoAttivati" CssClass="pannello-endo endo-attivati">
                    <fieldset class="blocco aperto">
                        <legend>
                            <asp:Literal runat="server" ID="ltrTitoloEndoAttivati" Text="Procedimenti attivati" />
                        </legend>
                        <uc1:GrigliaEndo runat="server" ID="grigliaEndoAttivati" />
                    </fieldset>
                </asp:Panel>


                <asp:Panel runat="server" ID="pnlEndoAttivabili" CssClass="pannello-endo endo-attivabili">
                    <fieldset class="blocco aperto">
                        <legend>
                            <asp:Literal runat="server" ID="ltrTitoloEndoAttivabili" Text="Procedimenti attivabili" /></legend>
                        <uc1:GrigliaEndo runat="server" ID="grigliaEndoAttivabili" />
                    </fieldset>
                </asp:Panel>


                <asp:Panel runat="server" ID="pnlAltriEndo" CssClass="pannello-endo altri-endo">
                    <fieldset class="blocco aperto">
                        <legend>
                            <asp:Literal runat="server" ID="ltrTitoloAltriEndo" Text="Altri endoprocedimenti attivati"></asp:Literal>
                        </legend>
                        <uc1:GrigliaEndo runat="server" ID="grigliaAltriEndo"/>
                    </fieldset>
                </asp:Panel>

                <asp:Button runat="server" CssClass="btn btn-default" ID="cmdAttivaAltriEndo" Text="Altri endoprocedimenti" OnClick="cmdAttivaAltriEndo_click" />
            </asp:View>

            <asp:View runat="server" ID="altriEndoView">

                <fieldset class="blocco aperto">
                    <legend>
                        <asp:Literal runat="server" ID="ltrTitoloAltriEndoAttivabili" Text="Altri endoprocedimenti attivabili"></asp:Literal>
                    </legend>
                    <uc1:GrigliaEndo runat="server" ID="grigliaAltriEndoAttivabili" />
                </fieldset>

                <div class="bottoni">
                    <asp:Button runat="server" ID="cmdTornaAllaLista" Text="Torna alla lista degli endoprocedimenti" OnClick="cmdTornaAllaLista_click" />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>

    <ar:BootstrapModal runat="server" ID="bmDettagliEndo" ShowOkButton="false" Title="Dettagli endoprocedimento"></ar:BootstrapModal>

    <ar:BootstrapModal runat="server" ID="bmSelezionareSubEndo" ShowOkButton="false" ShowKoButton="true" KoText="Chiudi" Title="Non è possibile proseguire">
        <ModalBody>
            Per proseguire è necessario selezionare una delle voci dell'endoprocedimento
            "<b><span id="nome-endo-da-selezionare"></span></b>"
        </ModalBody>
    </ar:BootstrapModal>

</asp:Content>
