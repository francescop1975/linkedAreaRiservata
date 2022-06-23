<%@ Page Title="Percorsi google maps" Language="C#" MasterPageFile="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" AutoEventWireup="true" CodeBehind="gestione-percorsi-google-maps.aspx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.gestione_percorsi_google_maps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Google places API -->
    <script type="text/javascript"
        src='https://maps.googleapis.com/maps/api/js?key=<%=this.MapsApiKey %>&libraries=places'></script>


    <style>
        #map {
            width: 100%;
            height: 400px;
        }

        #waypointTemplate {
            display: none;
        }
    </style>

    <script type="module">
        import { MapIntegration } from '<%=ResolveClientUrl("~/js/app/percorsi-google-maps/app.js")%>';
        import { AppMapEventsEnum } from '<%=ResolveClientUrl("~/js/app/percorsi-google-maps/app-map.js")%>';

        $(function () {
            const map = new MapIntegration({
                startPoint: document.getElementById('startPoint'),
                endPoint: document.getElementById('endPoint'),
                mapElement: document.getElementById('map'),
                tabellaIndicazioni: document.getElementById('tabellaIndicazioni'),
                templateWaypoint: document.getElementById('waypointTemplate'),
                contenitoreWaypoints: document.getElementById('waypointsList'),
                <%=LimitiMappa%>
                contenitoreIndicazioni: document.getElementById('contenitoreIndicazioni'),
                templateElementoIndicazioni: document.getElementById('templateElementoIndicazioni'),
                contenitorePercorsoCalcolato: document.getElementById('<%=hidContenitorePercorso.ClientID%>')
            });

            const contenitoreSteps = document.getElementById('<%=hidContenitoreSteps.ClientID%>');

            function parseRoute(route) {

                contenitoreSteps.value = '';

                route.legs.forEach((leg) => {
                    leg.steps.forEach((step) => {
                        contenitoreSteps.value += `${step.distance.value}@${encodeURIComponent(step.instructions)}\n`;
                    });
                });

                console.log('contenitoreSteps.value', contenitoreSteps.value);
            }

            function elaboraPercorso() {
                const percorso = JSON.parse(atob(document.getElementById('<%=hidContenitorePercorso.ClientID%>').value));

                console.log("percorso-modificato", percorso.percorso);

                parseRoute(percorso.percorso.routes[0]);
            }

            <%if (!this.PercorsoCalcolato){%>
                map.inizializza();
            <%} else { %>
                map.inizializza(JSON.parse(atob(document.getElementById('<%=hidContenitorePercorso.ClientID%>').value)));

                elaboraPercorso();
            <%}%>


            map.aggiungiHandler(AppMapEventsEnum.PercorsoModificato, (evento) => { elaboraPercorso() });



            $('#aggiungiWaypoint').on('click', () => map.aggiungiWaypoint());
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="stepContent" runat="server">


    <div class="row">
        <div class="col-sm-4">
            <div class="percorso">
                <h2>Percorso
                </h2>

                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <label for="exampleInputEmail1">Partenza</label>
                            <div class="input-group">
                                <input type="text" class="form-control" id="startPoint" aria-describedby="emailHelp"
                                    placeholder="Indirizzo di partenza"
                                    value="">

                                <div class="input-group-btn">
                                    <button type="button" class="btn btn-default" id="aggiungiWaypoint">
                                        +
                                    </button>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>




                <div id="waypointsList">
                </div>


                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <label for="exampleInputEmail1">Arrivo</label>
                            <input type="text" class="form-control" id="endPoint" aria-describedby="emailHelp"
                                placeholder="Indirizzo di arrivo"
                                value="">
                        </div>
                    </div>
                </div>

            </div>

        </div>
        <div class="col-sm-8">
            <div id="map"></div>
        </div>
    </div>


    <table class="table table-striped">
        <thead>
            <tr>

                <!--
                <th>Località</th>
                <th>Comune</th>
                <th>Provincia</th>
                <th>Via</th>
                -->
                <th>Indicazioni</th>
                <th>Distanza</th>
            </tr>
        </thead>
        <tbody id="contenitoreIndicazioni">
        </tbody>
    </table>

    <asp:HiddenField runat="server" ID="hidContenitoreSteps" />
    <asp:HiddenField runat="server" ID="hidContenitorePercorso" />


    <!-- template da riutilizzare per gli waypoints-->
    <table style='display: none'>
        <tbody id="templateElementoIndicazioni">
            <tr>
                <!--
                        <td class="nome-citta">
                        </td>
                        <td class="nome-comune">
                        </td>
                        <td class="nome-provincia">
                        </td>
                        <td class="nome-via">
                        </td>
                    -->
                <td class="indicazioni"></td>
                <td class="distanza-percorsa"></td>
            </tr>
        </tbody>

    </table>

    <div id="waypointTemplate">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="exampleInputEmail1">Punto intermedio</label>
                    <div class="input-group">
                        <input type="text" class="form-control" aria-describedby="emailHelp"
                            placeholder="Punto intermedio" value="">

                        <div class="input-group-btn">
                            <button type="button" class="btn btn-default">
                                -
                            </button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
