<%@ Page Title=" " Language="C#" MasterPageFile="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" AutoEventWireup="true" CodeBehind="gestione-localizzazioni-modena.aspx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.localizzazioni_modena.gestione_localizzazioni_modena" %>

<%@ MasterType VirtualPath="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.2/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="https://cdn.rawgit.com/openlayers/openlayers.github.io/master/en/v5.3.0/css/ol.css" type="text/css" />
    <!-- stili generale per progetti mappe openlayers del comune di Modena (minificato - produzione) -->
    <%--<link rel="stylesheet" href='<%=ResolveClientUrl("~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/css/stile_base_template.css") %>' type="text/css" />--%>
    <!-- stili di progetto -->
    <link rel="stylesheet" href='<%=ResolveClientUrl("~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/css/current_project_bootstrap.css") %>' type="text/css" />

    <style>
        .badge-danger {
            background-color: #d9534f !important;
        }

        .contenitore-mappa label {
            margin-top: 5px;
        }

        .ol-closer {
            width: 10px;
            display: block;
            height: 12px;
        }

            .ol-closer::before {
                content: "X";
            }

        .panel-heading h5 {
            margin: 0;
        }

        .card-body b {
            display: block;
            text-align: center;
            padding-top: 10px;
            padding-bottom: 10px;
            font-weight: normal;
            text-transform: uppercase;
        }

        .card-body .list-group-item {
            border-color: rgba(0,0,0,.125) !important;
            border-top: 1px solid;
            border-left: 0px;
            border-right: 0px;
            border-bottom: 0px;
            padding-top: 10px;
            margin-right: 15px;
            padding-bottom: 10px;
            margin-left: 15px;
        }

            .card-body .list-group-item:first-child {
                border-radius: 0;
            }

            .card-body .list-group-item:last-child {
                border-radius: 0;
            }

        #conferma_selezione {
            border-top-left-radius: 0;
            border-top-right-radius: 0;
            border-bottom-right-radius: 4px;
            border-bottom-left-radius: 4px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="stepContent" runat="server">

    <!-- Vista Dettaglio/modifica vie -->

    <%--<h3>Seleziona particelle</h3>--%>
    <div class="contenitore-mappa">
        <div class="row">
            <div class="col-sm-9 col-md-10 col-lg-8 col-xl-7">
                <div class="form-group row">

                    <%--                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lbTornaAllaLista" CssClass="btn btn-default" OnClick="cmdTornaAllaLista_Click">
                                    <i class="glyphicon glyphicon-chevron-left"></i>
                                    Torna alla lista
                        </asp:LinkButton>
                    </div>--%>

                    <div class="col-md-2">
                        <label class="col-form-label pull-right" for="lista_fogli">
                            Foglio
                        </label>
                    </div>

                    <div class="col-md-2">
                        <select id="lista_fogli" class="form-control align_right selectpicker"
                            data-live-search="true" data-width="10em">
                        </select>
                    </div>

                    <div class="col-md-2">
                        <label class="col-form-label pull-right" for="lista_mappali_select" id="lista_mappali_label">
                            Mappale
                        </label>
                    </div>


                    <div class="col-md-2" id="particella_area">
                        <select id="lista_mappali_select" class="form-control align_right selectpicker"
                            data-live-search="true" data-width="10em" style="display: none">
                        </select>
                        <select id="lista_mappali_alt_text" class="form-control align_right selectpicker"
                            data-width="10em">
                            <option value="">--</option>
                        </select>
                    </div>

                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addPartkeyManually">
                            Aggiungi Particella non in mappa
                        </button>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-sm-8 col-md-9 col-lg-9">
                <div id="map" class="map"></div>
            </div>
            <div class="col-sm-4 col-md-3 col-lg-3">

                <div class="panel panel-default" id="selection_block" style="display: none;">

                    <div class="panel-heading">
                        <span id="foglio_selezionato"></span>
                        <a class="badge badge-danger pull-right" href="#" id="annulla_selezione">X</a>
                    </div>

                    <div class="card-body" style="overflow-y: auto; overflow-x: hidden; /* display: none; */height: 20em;">
                        <!-- LISTA PARTICELLE GRAFICHE SELEZIONATE -->
                        <b id="titolo_particelle_grafiche">Particelle Grafiche</b>
                        <ul id="lista_particelle_selezionate" class="list-group list-group-flush dropdown"></ul>
                        <b id="titolo_particelle_manuali">Particelle non in mappa</b>
                        <ul id="lista_particelle_create" class="list-group list-group-flush dropdown"></ul>
                    </div>

                    <!-- BOTTONI "conferma" e "aggiungi"--->
                    <ul class="nav nav-pills nav-fill ">
                        <li class="nav-item" style="width: 100%; margin-bottom: 0">
                            <asp:Button runat="server" CssClass="btn btn-primary" Style="display: none" ID="cmdConferma" Text="Conferma" OnClick="cmdConferma_Click" />
                            <div class="btn btn-primary" href="#" id="conferma_selezione" style="display: none; width: 100%">Conferma</div>
                        </li>
                    </ul>
                    <asp:HiddenField runat="server" ID="hidValoriSelezionati" />
                </div>
            </div>
        </div>

    </div>

    <div class="modal fade" id="addPartkeyManually" tabindex="-1" role="dialog" aria-labelledby="addPartkeyManuallyLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="addPartkeyManuallyLabel">Aggiungi Particella non presente in mappa</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table table">
                        <thead>
                            <tr>
                                <th scope="col">Comune</th>
                                <th scope="col">Sezione</th>
                                <th scope="col">Foglio</th>
                                <th scope="col">Particella</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>F257</td>
                                <td>_</td>
                                <td>
                                    <span id="foglio_selezionato_form_add"></span>
                                </td>
                                <td>
                                    <input type="text" id="nuova_particella_input" name="nuova_particella" value="" placeholder="" maxlength="5">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Annulla</button>
                    <button type="button" class="btn btn-primary" id="button_confirm_modal_particella">Aggiungi</button>

                </div>
            </div>
        </div>
    </div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"
        integrity="sha384-wHAiFfRlMFy6i5SRaxvfOCifBUQy1xHdJ/yoi7FRNXMRBu5WHdZYu1hA6ZOblgut"
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.2/js/bootstrap-select.min.js"></script>

    <!-- libreria per retrocompatibilita con Internet Explorer e Android 4.x -->
    <script crossorigin="anonymous" src="https://polyfill.io/v3/polyfill.min.js?features=default%2Cfetch%2CrequestAnimationFrame%2CElement.prototype.classList%2CURL%2CArray.prototype.includes%2CString.prototype.padStart%2CString.prototype.padEnd"></script>

    <!-- LIBRERIA OPENLAYERS E PROJ4JS -->
    <script type="text/javascript" src="https://cdn.rawgit.com/openlayers/openlayers.github.io/master/en/v5.3.0/build/ol.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/proj4js/2.5.0/proj4.js"></script>


    <%--            <!-- FILE DI CONFIGURAZIONE APPLICAZIONE -->
            <script type="text/javascript" src="resources/config_modena_bootstrap.js"></script>--%>

    <!-- INIZIO FILES JS SORGENTI (sviluppo) -->
    <%-- <%=LoadScripts(new[] {
                "~/reserved/inserimentoistanza/localizzazioni-modena/resources/config_modena_bootstrap.js",
                "~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/js/commonFunctions/wrappers.js",
                "~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/js/commonFunctions/commonUtils.js",
                "~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/js/commonFunctions/gisUtils.js",
                "~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/js/commonFunctions/catastoUtils.js",
                "~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/js/projectFunctions.js",
                "~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/js/main.js",
            }) %>--%>
    <script type="module">
                //importo funzioni necessarie da moduli
                //import { run } from '<%=ResolveClientUrl("~/reserved/inserimentoistanza/localizzazioni-modena/resources/src/js/main.mjs")%>';
        import { run } from "./resources/src/js/main.mjs"
        import { eliminaPartKeyGraficoDaSelezione, eliminaPartKeyManualeDaSelezione } from "./resources/src/js/projectFunctions.mjs";


        function postToHandler(localizzazioni) {
            var handler = '<%=ResolveClientUrl("~/reserved/inserimentoistanza/localizzazioni-modena/handlers/localizzazionihandler.asmx") + "/ModificaLocalizzazioni" + "?idcomune=" + this.IdComune + "&software=" + this.Software %>';
            var data = {
                idDomanda: '<%=this.IdDomanda%>',
                opzioniDefault: {
                    idStradarioDefault: <%=this.IdStradarioDefault%>,
                    idCatastoDefault: '<%=this.IdCatastoDefault%>',
                    nomeCatastoDefault: '<%=this.NomeCatastoDefault%>'
                },
                localizzazioni: localizzazioni,
            };

            mostraModalCaricamento();

            $.ajax({
                url: handler,
                method: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data)
            })
                .done(function () {
                    location.replace(window.location);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {

                    nascondiModalCaricamento();

                    console.error(jqXHR, textStatus, errorThrown);
                });
        }

        // Inizializzazione delle particelle già selezionate:
        // window.arrayParticelleInput = ["F257   18  181", "F257   18  187", "F257   18  206"];
        function txtToMappale(x) {
            const codCatastale = x.slice(0, 4);
            const foglio = x.slice(4, 9).trim();
            const particella = x.slice(9, 14).trim();

            return {
                codCatastale: codCatastale,
                foglio: foglio,
                particella: particella
            };
        }

        const arrayParticelleInput = [<%= GetArrayJsPerInizializzazione() %>];

        const callBack = function (arrayParticelleGrafiche, arrayParticelleManuali) {
            const mappedGrafiche = arrayParticelleGrafiche.map((x) => txtToMappale(x));
            const mappedManuali = arrayParticelleManuali.map((x) => txtToMappale(x));

            console.log('mappedGrafiche', mappedGrafiche);
            console.log('mappedManuali', mappedManuali);

            //postToHandler(mapped);

            $('#<%=hidValoriSelezionati.ClientID%>').val(
                JSON.stringify({
                    particelleGrafiche: mappedGrafiche,
                    particelleManuali: mappedManuali
                })
            );

            document.getElementById('<%=cmdConferma.ClientID%>').click();
        };


        $(() => {
            //definisco funzioni necessarie per eliminazione particelle dalla lista
            //ed avvio
            window.eliminaPartKeyGraficoDaSelezione = eliminaPartKeyGraficoDaSelezione;
            window.eliminaPartKeyManualeDaSelezione = eliminaPartKeyManualeDaSelezione;

            run(arrayParticelleInput, callBack);

            //$('#addPartkeyManually').appendTo($(document.forms[0]));
        });
    </script>
</asp:Content>
