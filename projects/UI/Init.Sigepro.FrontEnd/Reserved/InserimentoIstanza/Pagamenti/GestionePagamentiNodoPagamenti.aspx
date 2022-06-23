﻿<%@ Page Title="Titolo" Language="C#" MasterPageFile="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master"
    AutoEventWireup="true" CodeBehind="GestionePagamentiNodoPagamenti.aspx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Pagamenti.GestionePagamentiNodoPagamenti" %>

<%@ MasterType VirtualPath="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" %>
<%@ Register TagPrefix="uc1" TagName="GrigliaPagamentoOneri" Src="~/Reserved/InserimentoIstanza/Pagamenti/GrigliaPagamentoOneri.ascx" %>
<%@ Register TagPrefix="ar" Namespace="Init.Sigepro.FrontEnd.WebControls.FormControls" Assembly="Init.Sigepro.FrontEnd.WebControls" %>

<%@ Register TagPrefix="cc1" Namespace="Init.Utils.Web.UI" Assembly="Init.Utils" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stepContent" runat="server">
    <style>
        #tabellaEndo .descrizioneEndo, #tabellaEndo .descrizioneIntervento {
            font-size: 0.8em;
            display: block;
            margin-top: 5px;
            margin-left: 10px;
        }

        #tabellaEndo .rigaTotaleImporti > td {
            text-align: right;
            font-weight: bold;
        }

        #tabellaEndo .helpNoteOnereRow {
            text-align: center;
        }

        .importoOnere {
            text-align:right;
        }

            .importoOnere > span {
                width: 100%;
                text-align: right;
                display: block;
            }

        .tabellaRiepilogoOneri {
            width: 80%;
            margin: 0 auto;
        }

            .tabellaRiepilogoOneri tr > th:nth-child(2) {
                text-align: right;
            }

        .riga-totale {
            font-weight: bold;
        }
    </style>

    <%=LoadScripts(new[]{
        "~/js/app/pagamenti/tabellariepilogooneri.js",
        "~/js/app/pagamenti/gestionePagamenti.js",
    }) %>

    <script type="text/javascript">
        $(function () {
            var gp = new GestionePagamenti();

            gp.init();
        });
    </script>

    <table id="tabellaEndo" class="table">
        <uc1:GrigliaPagamentoOneri runat="Server" ID="grigliaOneriIntervento" />
        <uc1:GrigliaPagamentoOneri runat="Server" ID="grigliaOneriEndo" />
        <tr class="rigaTotaleImporti">
            <td colspan="6">
                <b>TOTALE DA PAGARE</b>
            </td>
            <td>€ <span id="totaleDaPagare"></span>
            </td>
        </tr>
    </table>

    <ar:BootstrapModal runat="server" ID="testoNote" ExtraCssClass="modal-note-oneri" Title="Note onere"></ar:BootstrapModal>


    <div class="panel panel-primary" id="totaleDaPagareOnline">
        <div class="panel-heading">
            <h3 class="panel-title">Totale da pagare online
            </h3>
        </div>

        <div class="panel-body">
            <div id="riepilogoOneriOnline">
            </div>
        </div>
    </div>

    <div class="panel panel-primary" id="dichiarazioneAssenzaOneri">

        <div class="panel-heading">
            <h3 class="panel-title">Dichiarazione assenza oneri da pagare
            </h3>
        </div>

        <div class="panel-body">
            <asp:CheckBox runat="server" ID="chkAssenzaOneri" class="chkAssenzaOneri" OnCheckedChanged="chkAssenzaOneri_CheckedChanged" />
            <span style="font-weight: bold">
                <%= TestoDichiarazioneAssenzaOneri%></span>
        </div>
    </div>



    <div class="panel panel-primary" id="bloccoCaricamentoBollettino">

        <div class="panel-heading">
            <h3 class="panel-title">Oneri pagati
            </h3>
        </div>

        <div class="panel-body">
            <div id="riepilogoOneriPagati">
            </div>
        </div>
        
        <div class="panel-heading">
            <h3 class="panel-title"><%=TitoloCaricamentoBollettino %>
            </h3>
        </div>

        <div class="panel-body">
            <asp:MultiView runat="server" ID="mvCaricamentoBollettino" ActiveViewIndex="0">
                <asp:View runat="server" ID="uploadView">

                    <script type="text/javascript">
                        $(() => {
                            document.getElementById('<%=fuCaricaFile.ClientID%>').addEventListener('change', () => {
                                document.getElementById('<%=cmdUpload.ClientID%>').click();
                            });
                        });

                    </script>

                    <%=DescrizioneCaricamentoBollettino %>

                    <div class="form-group">
                        <asp:Label runat="server" ID="lblUploadFile" Text="Seleziona il file da allegare:"
                            AssociatedControlID="fuCaricaFile" />

                        <asp:FileUpload runat="server" ID="fuCaricaFile" CssClass="form-control" />
                    </div>

                    <asp:Button runat="server" CssClass="btn btn-primary vbg-show-spinner" ID="cmdUpload" Text="Allega file" OnClick="cmdUpload_Click" OnClientClick="onCaricamentoAllegato()" />
                </asp:View>
                <asp:View runat="server" ID="dettagliouploadView">
                    <div class="descrizioneStep">
                        <%=DescrizioneCaricamentoBollettinoEffettuato %>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" ID="lblFileCaricato" Text="File allegato:" AssociatedControlID="hlFileCaricato" />
                        <asp:HyperLink runat="server" ID="hlFileCaricato" CssClass="form-control" />
                    </div>

                    <asp:Literal runat="server" ID="lblErroreFirma">
                            <div class="alert alert-danger" role="alert">
							    &nbsp;Attenzione, il file non è stato firmato digitalmente
                            </div>
                    </asp:Literal>

                    <asp:Button runat="server" ID="cmdRimuovi" Text="Rimuovi file" CssClass="btn btn-primary vbg-show-spinner" OnClick="cmdRimuovi_Click"
                        OnClientClick="return confirm('Rimuovere il file selezionato\?')" />
                </asp:View>
            </asp:MultiView>
        </div>

    </div>

</asp:Content>
