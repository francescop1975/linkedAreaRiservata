export class AppElementoIndicazioni {
    constructor(htmlElement) {
        this.htmlElement = htmlElement;
        this.citta = htmlElement.getElementsByClassName("nome-citta")[0];
        this.comune = htmlElement.getElementsByClassName("nome-comune")[0];
        this.provincia = htmlElement.getElementsByClassName("nome-provincia")[0];
        this.via = htmlElement.getElementsByClassName("nome-via")[0];
        this.distanzaPercorsa = htmlElement.getElementsByClassName("distanza-percorsa")[0];
    }
    setCitta(value) {
        if (!this.citta) {
            return;
        }
        this.citta.innerHTML = value;
    }
    setComune(value) {
        if (!this.comune) {
            return;
        }
        this.comune.innerHTML = value;
    }
    setProvincia(value) {
        if (!this.provincia) {
            return;
        }
        this.provincia.innerHTML = value;
    }
    setVia(value) {
        this.via.innerHTML = value;
    }
    setDistanzaPercorsa(value) {
        this.distanzaPercorsa.innerHTML = value.toString();
    }
    setDistanzaPercorsaStr(value) {
        this.distanzaPercorsa.innerHTML = value.toString();
    }
}
//# sourceMappingURL=app-elemento-indicazioni.js.map