export class AppElementoIndicazioni{

    private citta: Element;
    private comune: Element;
    private provincia: Element;
    private via: Element;
    private distanzaPercorsa: Element;

    /**
     *
     */
    constructor(private htmlElement:HTMLElement) {
            this.citta = htmlElement.getElementsByClassName("nome-citta")[0];
            this.comune = htmlElement.getElementsByClassName("nome-comune")[0];
            this.provincia = htmlElement.getElementsByClassName("nome-provincia")[0];
            this.via = htmlElement.getElementsByClassName("nome-via")[0];
            this.distanzaPercorsa = htmlElement.getElementsByClassName("distanza-percorsa")[0];
    }

    setCitta(value: string): void {
        if (!this.citta) {
            return;
        }
        this.citta.innerHTML = value;

    }

    setComune(value: string): void {
        if (!this.comune) {
            return;
        }
        this.comune.innerHTML = value;
    }

    setProvincia(value: string): void {
        if (!this.provincia) {
            return;
        }
        this.provincia.innerHTML = value;
    }

    setVia(value: string): void {
        this.via.innerHTML = value;
    }

    setDistanzaPercorsa(value: number): void {
        this.distanzaPercorsa.innerHTML = value.toString();
    }
    setDistanzaPercorsaStr(value: string): void {
        this.distanzaPercorsa.innerHTML = value.toString();
    }

}