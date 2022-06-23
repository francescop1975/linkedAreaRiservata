class GestioneEndoPresenti {
    constructor(_webServiceUrl, _token, _jqRoot) {
        this._webServiceUrl = _webServiceUrl;
        this._token = _token;
        this._jqRoot = _jqRoot;
        this._chkPresente = this._jqRoot.find('.chk-presente input[type=checkbox]');
        this._ddlTipoTitolo = this._jqRoot.find('.ddl-tipo-titolo select');
        this._txtNumero = this._jqRoot.find('.txt-numero');
        this._txtData = this._jqRoot.find('.txt-data');
        this._txtRilasciatoDa = this._jqRoot.find('.txt-rilasciato-da');
        this._txtNote = this._jqRoot.find('.txt-note');
        this._txtMessaggio = this._jqRoot.find('.info-estremi-atto');
        this._boxEstremiAtto = this._jqRoot.find('.estremi-atto');
        this._chkPresente.on('change', (e) => {
            this.onCheckModificato();
        });
        this._ddlTipoTitolo.on('change', (e) => {
            this.onTipoTitoloModificato();
        });
        this._chkPresente.trigger('change');
        this._ddlTipoTitolo.trigger('change');
    }
    onTipoTitoloModificato() {
        let tipoTitolo = this._ddlTipoTitolo.val();
        if (tipoTitolo == '' || tipoTitolo == '-1') {
            this._txtMessaggio.html('');
            this.nascondiCampo(this._txtNumero);
            this.nascondiCampo(this._txtData);
            this.nascondiCampo(this._txtRilasciatoDa);
            this.nascondiCampo(this._txtNote);
            return;
        }
        this.callTitoliService(tipoTitolo)
            .then((flagsTitolo) => {
            this._txtMessaggio.html(flagsTitolo.messaggio);
            this.toggle(this._txtNumero, flagsTitolo.richiedeNumero);
            this.toggle(this._txtData, flagsTitolo.richiedeData);
            this.toggle(this._txtRilasciatoDa, flagsTitolo.richiedeRilasciatoDa);
            this.mostraCampo(this._txtNote);
        });
    }
    toggle(campo, toggle) {
        if (toggle) {
            this.mostraCampo(campo);
        }
        else {
            this.nascondiCampo(campo);
        }
    }
    mostraCampo(campo) {
        campo.show();
    }
    nascondiCampo(campo) {
        campo.find('input').val('');
        campo.hide();
    }
    onCheckModificato() {
        let checked = this._chkPresente.is(':checked');
        this._boxEstremiAtto.toggle(checked);
    }
    callTitoliService(idTipoTitolo) {
        var parameters = {
            token: this._token,
            idTipoTitolo: idTipoTitolo
        };
        var deferred = jQuery.Deferred();
        $.ajax({
            url: this._webServiceUrl,
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(parameters)
        }).then((data) => {
            deferred.resolve(data.d);
        }, (jqXHR, textStatus, errorThrown) => {
            console.log("Request failed: " + textStatus);
            deferred.reject(errorThrown);
        });
        return deferred.promise();
    }
}
$.fn.gestioneEndoPresenti = function (options) {
    return this.each(function () {
        if (!$.data(this, "plugin_gestioneEndoPresenti")) {
            $.data(this, "plugin_gestioneEndoPresenti", new GestioneEndoPresenti(options.webServiceUrl, options.token, $(this)));
        }
    });
};
//# sourceMappingURL=gestione-endo-presenti.js.map