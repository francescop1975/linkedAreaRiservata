import { AppElementoIndicazioni } from './app-elemento-indicazioni.js';
export class AppDescrizionePercorsoRenderer {
    constructor(contenitore, templateElemento) {
        this.contenitore = contenitore;
        this.templateElemento = templateElemento;
    }
    disegna(percorso) {
        this.contenitore.innerHTML = '';
        percorso.routes[0].legs.forEach((leg) => {
            leg.steps.forEach((step) => {
                const el = $(this.templateElemento.innerHTML)[0];
                const elemento = new AppElementoIndicazioni(el);
                elemento.setDistanzaPercorsaStr(step.distance.text);
                el.getElementsByClassName('indicazioni')[0].innerHTML = step.instructions;
                this.contenitore.appendChild(el);
            });
        });
    }
}
//# sourceMappingURL=app-descrizione-percorso-renderer.js.map