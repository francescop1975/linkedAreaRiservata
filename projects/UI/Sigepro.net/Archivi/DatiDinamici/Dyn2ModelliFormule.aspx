<%@ Page Language="C#" MasterPageFile="~/SigeproNetMaster.master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Dyn2ModelliFormule.aspx.cs"
    Inherits="Sigepro.net.Archivi.DatiDinamici.Dyn2ModelliFormule" Title="Formula scheda" %>

<%@ Register TagPrefix="init" Namespace="Init.Utils.Web.UI" Assembly="Init.Utils" %>
<%@ Register TagPrefix="init" Namespace="SIGePro.WebControls.UI" Assembly="SIGePro.WebControls" %>
<%@ Register TagPrefix="init" Namespace="SIGePro.WebControls.Ajax" Assembly="SIGePro.WebControls" %>
<%@ MasterType VirtualPath="~/SigeproNetMaster.master" %>

<asp:Content runat="server" ContentPlaceHolderID="headPagina">

    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/CodeMirror/lib/codemirror.js") %>"></script>
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/js/CodeMirror/lib/codemirror.css") %>" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/CodeMirror/mode/clike/clike.js") %>"></script>
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/js/CodeMirror/theme/neat.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/stili/dyn2-modelli-formule.css") %>" />

    <style type="text/css">
        /*.activeline 
      {
      	background: #f0fcff !important;
      	z-index: -2;
      }*/
        .descrizione-editor {
            position: absolute;
            padding: 4px 12px 4px 12px;
            background-color: #f7f7f7;
            border: 1px solid #dfdfdf;
            border-bottom: 0px;
            display: none;
            top: -28px;
            z-index: 99;
        }

            .descrizione-editor.focused {
                display: block;
            }

        .activeLine {
            background-color: rgba(200,236,255, 0.3) !important;
            margin-left: -6px !important;
            padding-left: 6px !important;
            margin-top: -1px !important;
            padding-top: 1px !important;
            margin-bottom: -1px !important;
            padding-bottom: 1px !important;
        }
    </style>

    <script type="text/javascript">

        var _formulaCodeMirror = null;
        var _usingCodeMirror = null;
        var _editorUsingVisibile = true;

        $(function () {

            function resetActiveLines() {
                _formulaCodeMirror.setLineClass(_formulaCodeMirror.activeLineTracker, null);
                _usingCodeMirror.setLineClass(_usingCodeMirror.activeLineTracker, null);
            }

            let codeMirrorOptions = {
                mode: "text/x-csharp",
                lineNumbers: true,
                theme: 'neat',
                tabMode: 'default',
                tabSize: 4,
                smartIndent: false,
                indentUnit: 4,
                indentWithTabs: true,
                enterMode: 'keep',
                onKeyEvent: (instance, e) => {
                    
                    if (e.type === 'keyup' && e.key === "Enter") {
                        const prevText = _formulaCodeMirror.getLine(_formulaCodeMirror.getCursor().line - 1);
                        if (isDichiarazioneFunzione(prevText)) {
                            aggiornaListaMetodi();
                        }
                    }
                },
                onCursorActivity: function (element) {
                    resetActiveLines();
                    element.activeLineTracker = element.setLineClass(element.getCursor().line, "activeLine");
                },
                extraKeys: {
                    "F11": function (element) {

                        var scroller = element.getScrollerElement();

                        if (scroller.className.search(/\bCodeMirror-fullscreen\b/) === -1) {
                            scroller.className += " CodeMirror-fullscreen";
                            scroller.style.height = "100%";
                            scroller.style.width = "100%";
                        } else {
                            scroller.className = scroller.className.replace(" CodeMirror-fullscreen", "");
                            scroller.style.height = '';
                            scroller.style.width = '';
                        }
                        element.refresh();
                    },
                    "Esc": function () {
                        var scroller = _formulaCodeMirror.getScrollerElement();
                        if (scroller.className.search(/\bCodeMirror-fullscreen\b/) !== -1) {
                            scroller.className = scroller.className.replace(" CodeMirror-fullscreen", "");
                            scroller.style.height = '';
                            scroller.style.width = '';
                            element.refresh();
                        }
                    }
                }
            }

            function creaDivDescrizione(testoDescrizione, parentElement) {
                var el = document.createElement('div');
                el.className = 'descrizione-editor';
                el.innerText = testoDescrizione;

                parentElement.appendChild(el);
                parentElement.style.position = 'relative';

                return el;
            }


            let ddl = document.getElementById('ctl00_ContentPlaceHolder1_ddlEvento__DropDownList');
            var text = ddl.options[ddl.options.selectedIndex].text;

            let testo = `Formula per ${text}`;
            let titoloEditorUsing = creaDivDescrizione('Blocchi using condivisi tra tutte le formule', document.getElementById('<%=txtUsing.ClientID%>').parentElement);
            let titoloEditorFormula = creaDivDescrizione(testo, document.getElementById('<%=txtScript.ClientID%>').parentElement);

            _formulaCodeMirror = CodeMirror.fromTextArea(document.getElementById('<%= txtScript.ClientID%>'), codeMirrorOptions);
            _usingCodeMirror = CodeMirror.fromTextArea(document.getElementById('<%= txtUsing.ClientID%>'), codeMirrorOptions);

            _usingCodeMirror.setOption('onFocus', (instance, event) => {
                titoloEditorUsing.classList.add('focused');
            });

            _usingCodeMirror.setOption('onBlur', (instance, event) => {
                titoloEditorUsing.classList.remove('focused');
            });

            _formulaCodeMirror.setOption('onFocus', (instance, event) => {
                titoloEditorFormula.classList.add('focused');
            });

            _formulaCodeMirror.setOption('onBlur', (instance, event) => {
                titoloEditorFormula.classList.remove('focused');
            });


            _formulaCodeMirror.activeLineTracker = _formulaCodeMirror.setLineClass(0, "activeLine");
            _usingCodeMirror.activeLineTracker = _usingCodeMirror.setLineClass(0, '');
            _usingCodeMirror.setSize('100%', '100px');
            /*
             * Gestione visualizzazione dell'editor per i blocchi using
             */
            function resizeEditor() {

                let margin = 2;

                let winHeight = $(window).height();
                let elTop = $(_formulaCodeMirror.getWrapperElement()).offset().top;
                let newHeight = winHeight - elTop - margin;

                _formulaCodeMirror.setSize('100%', newHeight.toString() + 'px');
            }


            function aggiornaVisualizzazioneEditor() {
                _editorUsingVisibile = $('#<%=chkNascondiUsing.ClientID%>').is(':checked');

                if (_editorUsingVisibile) {
                    _usingCodeMirror.getWrapperElement().style.display = 'block';
                } else {
                    _usingCodeMirror.getWrapperElement().style.display = 'none';
                }
                resizeEditor();
            }

            let chkMostraUsing = $('#<%=chkNascondiUsing.ClientID%>');
            chkMostraUsing.on('click', aggiornaVisualizzazioneEditor);

            aggiornaVisualizzazioneEditor();

            $(window).on('resize', aggiornaVisualizzazioneEditor);

            setTimeout(() => {
                $('.esito-salvataggio').hide('slow');
            }, 2000);

            // Combo delle funzioni
            const comboFunzioni = document.getElementById('funzioni');
            comboFunzioni.addEventListener('change', (e) => {
                if (comboFunzioni.value) {
                    const riga = parseInt(comboFunzioni.value);
                    const y = _formulaCodeMirror.charCoords({ line: riga, ch: 0 }, "local").y;
                    const margin = 12;
                    _formulaCodeMirror.setCursor(riga,0);
                    _formulaCodeMirror.scrollTo(0, y - margin);
                    setTimeout(() => _formulaCodeMirror.focus(), 50);
                    comboFunzioni.value = '';
                }
            });
            aggiornaListaMetodi();
        });

        const isDichiarazioneFunzione = (val) => {
            return (
                val.startsWith('public ') ||
                val.startsWith('private ') ||
                val.startsWith('protected ')
            ) && (val.indexOf('(') !== -1 && val.indexOf(')') !== -1);
        };

        const estraiNomeFunzione = (val) => {
            var splitted = val.split(' ');

            if (splitted.length < 3) return '';

            var nome = splitted[2];

            if (nome.indexOf('(') !== -1) {
                nome = nome.slice(0, nome.indexOf('('));
            }

            return nome;
        }

        function aggiornaListaMetodi() {
            const candidati = _formulaCodeMirror.getValue()
                .split('\n')
                .map((x, i) => ({ index: i, text: x.trim() }))
                .filter(l => isDichiarazioneFunzione(l.text))
                .map((x) => ({ index: x.index, text: estraiNomeFunzione(x.text) }))
                .filter(l => l.text.length > 0);

            candidati.sort((a, b) => a.text.localeCompare(b.text));

            const funzioni = document.getElementById('funzioni');
            funzioni.innerHTML = '';
            funzioni.append(document.createElement('option'));
            funzioni.style.display = candidati.length ? 'block' : 'none';

            for (let candidato of candidati) {
                var option = document.createElement('option');
                option.innerText = candidato.text;
                option.value = candidato.index;
                funzioni.append(option);
            }
        }

        function mostraEsempi() {
            showpopup("PopupFormuleDatiDinamici.aspx");
        }

        function inserisciAssegnazione() {
            showpopup("PopupInserisciAssegnazione.aspx?token=<%=Token%>&idmodello=<%=IdModello%>&software=<%=Software %>");
        }

        function inserisciMostraCampoDyn() {
            showpopup("PopupVisualizzaCampo.aspx?token=<%=Token%>&idmodello=<%=IdModello%>&software=<%=Software %>");
        }

        function inserisciMostraCampoStatico() {
            showpopup("PopupVisualizzaCampo.aspx?token=<%=Token%>&idmodello=<%=IdModello%>&software=<%=Software %>&campiStatici=true");
        }


        function showpopup(url) {
            var winWidth = "600";
            var winHeight = "550";
            var w = window.open(url, "", "centerscreen=yes,width=" + winWidth + ",height=" + winHeight + ",scrollbars=yes,location=no");
        }

        function insertText(value) {
            _formulaCodeMirror.replaceSelection(value);
        }

        function copyToClipboard(sender, ctrlId) {
            var el = document.getElementById(ctrlId);
            var varName = el.value.replace(/ /g, "_");
            varName = varName.replace(/\'/g, "_");
            varName = varName.replace(/`/g, "_");

            var data = "var " + varName + " = ModelloCorrente.TrovaCampo(\"" + el.value + "\");";

            //var data = "\"" + el.value + "\"";
            _formulaCodeMirror.replaceSelection(data);
            _formulaCodeMirror.focus();
        }



    </script>


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:MultiView runat="server" ID="multiView" ActiveViewIndex="1">
        <asp:View runat="server" ID="loginView">
        </asp:View>
        <asp:View runat="server" ID="detailsView">
            <%--<fieldset>--%>

            <div class="esito-salvataggio">
                <div runat="server" id="divFormulaSalvata" class="alert alert-success formula-salvata" visible="false">
                    <b>Formula salvata correttamente
                    </b>
                </div>

                <div runat="server" id="divFormulaCompilata" class="alert alert-success formula-salvata" visible="false">
                    <b>Formula compilata senza errori
                    </b>
                </div>
            </div>

            <div class="d2mf">

                <div class="lista-funzionalita">
                    <init:LabeledDropDownList runat="server" ID="ddlEvento" CssClass="evento" Descrizione="Evento" Item-AutoPostBack="true" OnValueChanged="ddlEvento_ValueChanged" />

                    <div class="mostra-using">
                        <asp:CheckBox runat="server" ID="chkNascondiUsing" Text="Mostra using" TextAlign="Right" />
                    </div>
                    <select id="funzioni"></select>
                    <div class="comandi">
                        <a href="javascript:inserisciAssegnazione()">Inserisci formula di assegnazione</a>
                        <a href="javascript:inserisciMostraCampoDyn()">Visualizza/Nascondi campo</a>
                        <a href="javascript:inserisciMostraCampoStatico()">Visualizza/Nascondi testo</a>
                        <a href="javascript:mostraEsempi();">Esempi di codice</a>
                    </div>

                    <div class="comandi">
                        <asp:LinkButton runat="server" ID="cmdSalva" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i> Salva" IdRisorsa="" OnClick="cmdSalva_Click" />
                        <asp:LinkButton runat="server" ID="cmdCompila" Text="<i class='fa fa-check' aria-hidden='true'></i> Verifica compilazione" OnClick="cmdCompila_Click" />
                        <asp:LinkButton runat="server" ID="cmdVisualizzaClasse" Text="<i class='fa fa-search' aria-hidden='true'></i> Visualizza codice" OnClick="cmdVisualizzaClasse_Click" />
                        <%--<asp:Button runat="server" ID="cmdCompilaFrontoffice" Text="Verifica compilazione FO" OnClick="cmdCompilaFrontoffice_Click" />--%>
                        <asp:LinkButton runat="server" ID="cmdChiudi" Text="<i class='fa fa-times' aria-hidden='true'></i> Chiudi" IdRisorsa="CHIUDI" OnClick="cmdChiudi_Click" />
                    </div>
                </div>

                <div class="editor-codice">
                    <div>
                        <asp:TextBox runat="server" ID="txtUsing" Columns="100" Rows="6" TextMode="MultiLine" CssClass="CampoFormula" />
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtScript" Columns="100" Rows="25" TextMode="MultiLine" CssClass="CampoFormula" />
                    </div>

                </div>

            </div>

            <%--</fieldset>--%>
        </asp:View>
    </asp:MultiView>
</asp:Content>
