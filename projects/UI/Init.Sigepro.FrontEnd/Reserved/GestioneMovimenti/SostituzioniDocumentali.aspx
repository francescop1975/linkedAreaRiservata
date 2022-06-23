﻿<%@ Page Title="Sostituzioni documentali" Language="C#" MasterPageFile="~/AreaRiservataMaster.Master" AutoEventWireup="true" CodeBehind="SostituzioniDocumentali.aspx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.GestioneMovimenti.SostituzioniDocumentali" %>

<%@ Register TagPrefix="uc1" Src="~/Reserved/GestioneMovimenti/SostituzioniDocumentaliGrid.ascx" TagName="SostituzioniDocumentaliGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPagina" runat="server">
    <script type="text/javascript">

        function confermaCanellazione() {
            return confirm("Eliminare il file sostitutivo? L\'operazione non potrà essere annullata.");
        }

        $(function () {
            $('.upload-documento-sostitutivo').hide();

            $('.cmd-sostituisci-documento').on('click', function (e) {

                var el = $(this),
                    parent = el.closest('.documento-sostituibile'),
                    azioniPanel = parent.find('.azioni'),
                    uploadPanel = el.data('upload-panel');

                if (!uploadPanel) {
                    uploadPanel = parent.find('.upload-documento-sostitutivo');
                    el.data('upload-panel', uploadPanel);
                }

                uploadPanel.modal('show');
                e.preventDefault();
            });


            document.querySelectorAll('.upload-file-sostitutivo').forEach((item) => {
                item.addEventListener('change', () => {
                    const cmd = item.closest('.modal').querySelector('.cmd-upload-file-sostitutivo');
                    if (cmd) {
                        cmd.click();
                    }
                });
            });
        });


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="inputForm">

        <h3>I seguenti allegati della pratica possono essere sostituiti</h3>

        <uc1:SostituzioniDocumentaliGrid runat="server"
            ID="sostituzioniDocumentaliGrid"
            OnAnnullaSostituzioneDocumentale="OnAnnullaSostituzioneDocumentale"
            OnSostituisciDocumento="OnSostituisciDocumento" />

        <asp:Repeater runat="server" ID="rptDocumentiEndoSostituibili" OnItemDataBound="rptDocumentiEndoSostituibili_ItemDataBound">
            <ItemTemplate>
                <h4>
                    <asp:Literal runat="server" Text='<%#Eval("Descrizione") %>' />
                </h4>

                <uc1:SostituzioniDocumentaliGrid runat="server"
                    ID="sostituzioniDocumentaliEndoGrid"
                    OnAnnullaSostituzioneDocumentale="OnAnnullaSostituzioneDocumentale"
                    OnSostituisciDocumento="OnSostituisciDocumento" />
            </ItemTemplate>

        </asp:Repeater>

        <div>
            <asp:Button runat="server" CssClass="btn btn-default" ID="cmdTornaIndietro" Text="Torna indietro" OnClick="cmdTornaIndietro_Click" />
            <asp:Button runat="server" CssClass="btn btn-primary" ID="cmdProcedi" Text="Procedi" OnClick="cmdProcedi_Click" />
        </div>
    </div>
</asp:Content>
