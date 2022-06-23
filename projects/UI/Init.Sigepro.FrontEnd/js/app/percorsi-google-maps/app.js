import { AppAutocomplete, EventoAutocompleteEnum } from './app-autocomplete.js';
import { AppMap, AppMapEventsEnum } from './app-map.js';
import { AppPercorso } from './app-percorso.js';
import { AppDescrizionePercorsoRenderer } from './app-descrizione-percorso-renderer.js';
import { AppEventHandler } from './app-event-handler.js';
export class MapIntegration extends AppEventHandler {
    constructor(options) {
        super();
        this.options = options;
        if (this.options.mapBounds) {
            this.bounds = new google.maps.LatLngBounds(this.options.mapBounds.sw, this.options.mapBounds.ne);
        }
    }
    inizializza(percorsoSalvato = null) {
        const inizio = new AppAutocomplete(this.options.startPoint, this.bounds);
        const fine = new AppAutocomplete(this.options.endPoint, this.bounds);
        this.percorso = new AppPercorso(inizio, fine);
        this.map = new AppMap(this.options.mapElement, this.bounds);
        this.percorso.aggiungiAllaMappa(this.map);
        this.percorso.aggiungiHandler(EventoAutocompleteEnum.LuogoModificato, () => this.aggiornaZoomMappa());
        this.percorso.aggiungiHandler(EventoAutocompleteEnum.LuogoModificato, () => this.ricalcolaPercorso());
        this.map.aggiungiHandler(AppMapEventsEnum.PercorsoModificato, () => {
            const percorso = this.map.getPercorsoAttuale();
            this.rendererPercorso.disegna(percorso);
            this.salvaPercorsoCalcolato(percorso);
        });
        this.map.aggiungiHandler(AppMapEventsEnum.InizioRicalcoloPercorso, () => {
            this.salvaPercorsoCalcolato(null);
        });
        this.rendererPercorso = new AppDescrizionePercorsoRenderer(this.options.contenitoreIndicazioni, this.options.templateElementoIndicazioni);
        if (percorsoSalvato != null) {
            inizio.inizializza(percorsoSalvato.waypoints.inizio);
            fine.inizializza(percorsoSalvato.waypoints.arrivo);
            percorsoSalvato.waypoints.puntiIntermedi.forEach(indirizzo => {
                this.aggiungiWaypoint(indirizzo);
            });
            this.map.disegnaPercorso(percorsoSalvato.percorso);
        }
    }
    salvaPercorsoCalcolato(percorsoCalcolato) {
        if (percorsoCalcolato == null) {
            this.options.contenitorePercorsoCalcolato.value = '';
            return;
        }
        const datiPercorso = JSON.stringify({
            waypoints: this.percorso.salvaStato(),
            percorso: percorsoCalcolato
        });
        this.options.contenitorePercorsoCalcolato.value = btoa(datiPercorso);
        this.trigger(AppMapEventsEnum.PercorsoModificato);
    }
    ricalcolaPercorso() {
        this.map.ricalcolaPercorso(this.percorso.getWaypoints());
    }
    aggiornaZoomMappa() {
        const bounds = new google.maps.LatLngBounds();
        this.percorso.getListaPuntiGeolocalizzati().forEach((coord) => bounds.extend(coord));
        this.map.fitBounds(bounds);
    }
    aggiungiWaypoint(indirizzo = null) {
        const cloned = document.createElement('div');
        cloned.innerHTML = this.options.templateWaypoint.innerHTML;
        const input = $(cloned).find('input[type=text]')[0];
        const bottoneRimuovi = $(cloned).find('button');
        const autocomplete = new AppAutocomplete(input, this.bounds);
        $(cloned).data("_autocomplete", autocomplete);
        bottoneRimuovi.on('click', (el) => this.rimuoviWaypoint(cloned));
        this.percorso.aggiungiWaypoint(autocomplete);
        this.options.contenitoreWaypoints.appendChild(cloned);
        if (indirizzo != null) {
            autocomplete.inizializza(indirizzo);
        }
    }
    rimuoviWaypoint(cloned) {
        const cloned$ = $(cloned);
        const autocomplete = cloned$.data("_autocomplete");
        this.percorso.rimuoviWaypoint(autocomplete);
        cloned$.remove();
    }
}
//# sourceMappingURL=app.js.map