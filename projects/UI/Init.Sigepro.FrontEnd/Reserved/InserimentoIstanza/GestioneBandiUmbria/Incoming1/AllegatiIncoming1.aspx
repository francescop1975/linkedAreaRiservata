﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Reserved/InserimentoIstanza/InserimentoIstanzaMaster.Master" AutoEventWireup="true" CodeBehind="AllegatiIncoming1.aspx.cs" Inherits="Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.GestioneBandiUmbria.Incoming1.AllegatiIncoming1" %>

<%@ Register Src="~/Reserved/InserimentoIstanza/GestioneBandiUmbria/Incoming1/AllegatiBandoIncomingBindingGrid.ascx" TagName="AllegatiBandoIncomingBindingGrid" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('.upload-form').hide();
            $('.upload-form').each(function (idx, item) {
                $(item).parent().find('.upload-button').on('click', function (e) {

                    e.preventDefault();

                    $(item).show();
                });
            });

            $('.bottone-carica').on('click', function (e) {
                $(this).hide(function () {
                    $(this).parent().find('.caricamento-in-corso').show();
                });
            });

            $('.upload-modello-compilato').hide();
            $('.download-modello-compilato').on('click', function () {
                $(this).parent().find('.upload-modello-compilato').show();
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="stepContent" runat="server">
    <div class="inputForm">

        <asp:Repeater runat="server" ID="rptAziende" OnItemDataBound="AziendaDataBound">
            <ItemTemplate>
                <uc1:AllegatiBandoIncomingBindingGrid runat="server" ID="crtlDatiAzienda" OnFileUploaded="OnFileUploaded" OnErrore="OnErrore" OnFileDeleted="OnFileDeleted" />
            </ItemTemplate>
        </asp:Repeater>


    </div>
</asp:Content>
