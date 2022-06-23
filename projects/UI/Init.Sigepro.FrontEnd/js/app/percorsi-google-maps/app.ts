import { AppAutocomplete, EventoAutocompleteEnum } from './app-autocomplete.js';
import { AppOptions } from './app-options.js';
import { AppMap, AppMapEventsEnum } from './app-map.js';
import { AppPercorso } from './app-percorso.js';
import { AppDescrizionePercorsoRenderer } from './app-descrizione-percorso-renderer.js';
import { PercorsoSalvato, LocalizzazionePunto } from './app-percorso-salvato.js';
import { AppEventHandler } from './app-event-handler.js';

//import * as $ from 'jquery';
//import 'jqueryui';
//import 'googlemaps';




export class MapIntegration extends AppEventHandler<AppMapEventsEnum> {

    percorso: AppPercorso;
    map: AppMap;
    bounds: google.maps.LatLngBounds;
    rendererPercorso: AppDescrizionePercorsoRenderer;

    constructor(private options: AppOptions) {
        super();

        if (this.options.mapBounds) {
            this.bounds = new google.maps.LatLngBounds(
                this.options.mapBounds.sw,
                this.options.mapBounds.ne
            );
        }
        
    }

    inizializza(percorsoSalvato: PercorsoSalvato = null): void {

        const inizio = new AppAutocomplete(this.options.startPoint, this.bounds);
        const fine = new AppAutocomplete(this.options.endPoint, this.bounds);

        this.percorso = new AppPercorso(inizio, fine);

        this.map =  new AppMap(this.options.mapElement, this.bounds);

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
            inizio.inizializza(percorsoSalvato.waypoints.inizio, );
            fine.inizializza(percorsoSalvato.waypoints.arrivo);

            percorsoSalvato.waypoints.puntiIntermedi.forEach(indirizzo => {
                this.aggiungiWaypoint(indirizzo);
            });

            
            this.map.disegnaPercorso(percorsoSalvato.percorso);
           
        }
    }

    salvaPercorsoCalcolato(percorsoCalcolato: google.maps.DirectionsResult) {

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

    aggiungiWaypoint(indirizzo: LocalizzazionePunto = null) {
        const cloned = document.createElement('div');
        cloned.innerHTML = this.options.templateWaypoint.innerHTML;
        
        // TODO: rimuovere jquery
        const input = <HTMLInputElement>$(cloned).find('input[type=text]')[0];
        const bottoneRimuovi = $(cloned).find('button');
        const autocomplete = new AppAutocomplete(input, this.bounds);
        // TODO: rimuovere jquery
        $(cloned).data("_autocomplete", autocomplete);

        bottoneRimuovi.on('click', (el) => this.rimuoviWaypoint(cloned));

        this.percorso.aggiungiWaypoint(autocomplete);

        this.options.contenitoreWaypoints.appendChild(cloned);

        if (indirizzo != null) {
            autocomplete.inizializza(indirizzo);
        }
    }

    // TODO: rimuovere jquery
    rimuoviWaypoint(cloned: HTMLDivElement): void {
        const cloned$ = $(cloned);
        const autocomplete = <AppAutocomplete>cloned$.data("_autocomplete");
        
        this.percorso.rimuoviWaypoint(autocomplete);
        cloned$.remove();   
    }



}
