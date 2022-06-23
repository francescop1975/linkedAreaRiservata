﻿

function GestionePagamenti() {

    'use strict';

    let chkAssenzaOneri;
    let bloccoCaricamentoBollettino;
    let Constants = {
        IdValoreNonSelezionato: "",
        IdValoreNonPagato: "0",
        IdValorePagaOnline: "1",
        IdValoreGiaPagato: "2"
    };
    let tabellaRiepilogoOneriOnline = new TabellaRiepilogoOneri('#riepilogoOneriOnline', 'Totale da pagare online');
    let tabellaRiepilogoOneriPagati = new TabellaRiepilogoOneri('#riepilogoOneriPagati', 'Oneri già pagati');


    function nascondiEstremiPagamento(element) {
        var row = element.parent().parent();

        row.find('.estremiPagamento').hide();
        row.find('.ddlTipoPagamento').val('');
        row.find('.txtDataPagamento').val('');
        row.find('.txtNumeroOperazione').val('');

        row.find('.importoPagato').show();
    }

    function nascondiEstremiPagamentoEImporto(element) {
        var row = element.parent().parent();

        nascondiEstremiPagamento(element);

        row.find('.importoPagato').hide();
    }

    function mostraEstremiPagamento(chkElement) {
        chkElement.parent().parent().find('.estremiPagamento').show();
    }

    function roundNumber(number, digits) {
        var multiple = Math.pow(10, digits),
            rndedNum = Math.round(number * multiple) / multiple;

        return rndedNum;
    }

    function mostraONascondiTabellaRiepilogoOneriOnline(listaOneriOnline) {
        tabellaRiepilogoOneriOnline.render(listaOneriOnline);
    }

    function mostraONascondiBloccoCaricamentoBollettino(nascondi) {
        if (nascondi) {
            bloccoCaricamentoBollettino.fadeOut();
        } else {
            bloccoCaricamentoBollettino.fadeIn();
        }
    }

    function mostraONascondiTabellaRiepilogoOneriPagati(listaOneriPagati) {
        tabellaRiepilogoOneriPagati.render(listaOneriPagati);

        mostraONascondiBloccoCaricamentoBollettino(listaOneriPagati.length === 0);
    }

    function mostraONascondiCheckOneriNonPagati(mostra) {

        var totaleDaPagare = parseFloat($('#totaleDaPagare').html());

        if (!mostra || totaleDaPagare > 0) {
            // uncheck della checkbox e
            // mostra il div di caricamento riepilogo oneri
            if (chkAssenzaOneri.is(':checked')) {
                chkAssenzaOneri.click();
            }

            // nascondi checkbox
            $('#dichiarazioneAssenzaOneri').fadeOut();
        } else {
            $('#dichiarazioneAssenzaOneri').fadeIn();
        }
    }

    function ricalcolaTotali() {
        let totale = 0.0;
        let listaOneriOnline = [];
        let listaOneriPagati = [];

        $('.ddlPagamento').each(function (idx, element) {
            let valore = $(element).val();
            let tr = $(element).closest('tr');
            let txtImportoPagato = tr.find('.importoPagato');
            let causale = tr.find('.descrizioneCausale').text();

            if (valore === Constants.IdValoreGiaPagato || valore === Constants.IdValorePagaOnline) {

                if (txtImportoPagato.val().indexOf('.') > 0 && txtImportoPagato.val().indexOf(',')) {
                    // la stringa è in formato x.xxx,zz
                    txtImportoPagato.val(txtImportoPagato.val().replace('.', ''));
                }

                var importoTmp = txtImportoPagato.val().replace(',', '.');

                const daPagare = parseFloat(importoTmp);

                const onere = {
                    causale: causale,
                    importo: daPagare
                };

                if (valore === Constants.IdValorePagaOnline) {
                    listaOneriOnline.push(onere);
                }

                if (valore === Constants.IdValoreGiaPagato) {
                    listaOneriPagati.push(onere);
                }

                totale += daPagare;
            }
        });

        $('#totaleDaPagare').html(roundNumber(totale, 2).toFixed(2).replace('.', ','));

        //mostraONascondiCheckNoOneri();
        mostraONascondiTabellaRiepilogoOneriOnline(listaOneriOnline);
        mostraONascondiTabellaRiepilogoOneriPagati(listaOneriPagati);
        mostraONascondiCheckOneriNonPagati(listaOneriOnline.length === 0 && listaOneriPagati.length === 0);
    }

    function bloccaModificheRiga(row) {
        $.each(row.find('input:not(input[type=\'hidden\']), select'), function (idx, htmlEl) {
            const el = $(htmlEl);
            let text = el.val();
            let parent = el.parent();
            let span = $('<span />');

            if (el[0].tagName === 'SELECT') {
                text = el.find('option:selected').text();
            }

            el.hide();

            span.text(text);
            span.appendTo(parent);
        });
    }


    function onPagamentoModificato(ddl) {
        let selectedValue = ddl.val()
        let pagatoOnline = ddl.data('pagamentoCompletato');

        switch (selectedValue) {
            case Constants.IdValoreNonSelezionato:
                nascondiEstremiPagamentoEImporto(ddl);
                break;

            case Constants.IdValoreNonPagato: // Non pagato
                nascondiEstremiPagamentoEImporto(ddl);
                break;

            case Constants.IdValorePagaOnline:   // Paga online

                if (pagatoOnline === 'True') {
                    mostraEstremiPagamento(ddl);
                    bloccaModificheRiga(ddl.parent().parent());

                } else {
                    nascondiEstremiPagamento(ddl);
                }
                break;

            case Constants.IdValoreGiaPagato:   // Già pagato
                mostraEstremiPagamento(ddl);
                break;
        }

        ricalcolaTotali();
    }

    function init() {
        chkAssenzaOneri = $('.chkAssenzaOneri');
        bloccoCaricamentoBollettino = $('#bloccoCaricamentoBollettino');

        $('.importoPagato').on('change', function () {

            var valore = $(this).val().replace(',', '.');

            if (valore === '' || isNaN(valore)) {
                $(this).val('0,00');
                valore = $(this).val().replace(',', '.');
            }

            valore = roundNumber(parseFloat(valore), 2).toString().replace('.', ',');

            valore = valore.indexOf(',') === -1 ? valore + ',00' : valore;

            $(this).val(valore);

            ricalcolaTotali();
        });

        const ddlPagamento = $('.ddlPagamento');

        ddlPagamento.on('change', function (e) {

            const thisElement = this;
            const $thisElement = $(this);

            if ($thisElement.val() !== "0") { // se il valore è diverso da non dovuto
                ddlPagamento.each((idx, el) => {
                    if (el !== thisElement && thisElement.value !== el.value && el.value !== "0" && el.dataset.pagamentoCompletato !== "True") {
                        el.value = thisElement.value;
                        onPagamentoModificato($(el));
                    }
                });
            }

            onPagamentoModificato($(this));
        });

        ddlPagamento.each(function (idx, el) {
            onPagamentoModificato($(el));
        });

        $('.noteOnere').each(function (idx, e) {
            var element = $(e);

            element.hide();

            if (element.html() === '') {
                element.parent().find('a').hide();
            }
        });

        $('.helpNoteOnere').click(function (e) {
            $('.modal-note-oneri .modal-body>div').html($(this).parent().find('.noteOnere').html());
            $('.modal-note-oneri').modal('show');
            e.preventDefault();
        });
    }

    return {
        init: init
    };
}
