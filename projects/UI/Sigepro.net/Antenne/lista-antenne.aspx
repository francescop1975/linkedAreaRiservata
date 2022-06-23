<%@ Page Language="c#" AutoEventWireup="true" Inherits="Sigepro.net.Antenne.lista_antenne" CodeBehind="lista-antenne.aspx.cs" %>

<%@ Register Src="~/Antenne/elemento-lista-antenne.ascx" TagPrefix="uc1" TagName="ElementoListaAntenne" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous" />
    <link href="../fonts/font-awesome-5/css/all.min.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.4.1.slim.min.js" integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
    <title>Document</title>
    <style>
        .container > .card {
            margin-bottom: 20px;
            /*width: 18em;
            display: inline-block;
            margin-right: 20px;
            */
        }


        .accordion .card-header {
            cursor: pointer;
        }

            .accordion .card-header > i.fa-chevron-down {
                display: none;
            }

            .accordion .card-header > i.fa-chevron-up {
                display: inline;
            }

            .accordion .card-header.collapsed > i.fa-chevron-down {
                display: inline;
            }

            .accordion .card-header.collapsed > i.fa-chevron-up {
                display: none;
            }

        .accordion .list-group-item {
            cursor: pointer;
        }

            .accordion .list-group-item > .fa-download {
                display: none;
            }

            .accordion .list-group-item:hover > .fa-download {
                display: inline;
            }
    </style>
</head>

<body>
    <form id="Form1" method="post" runat="server">
        <div class="container">

            <h1>Antenna <%=Id %></h1>

            <asp:Repeater runat="server" ID="rptPratiche" OnItemDataBound="rptPratiche_ItemDataBound">
                <ItemTemplate>
                    <uc1:ElementoListaAntenne runat="server" ID="elementoListaAntenne" />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>

</html>
