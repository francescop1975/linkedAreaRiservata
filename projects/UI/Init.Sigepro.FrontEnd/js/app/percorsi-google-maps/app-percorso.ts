import { AppAutocomplete, EventoAutocompleteEnum } from './app-autocomplete.js';
import { AppMap } from './app-map.js';
import { WaypointsPercorso } from './waypoints-percorso.js';
import { AppEventHandler } from './app-event-handler.js';



export class AppPercorso extends AppEventHandler<EventoAutocompleteEnum>
{
    waypoints = new Array<AppAutocomplete>();
    map: AppMap;

    constructor(private inizio: AppAutocomplete, private fine: AppAutocomplete) {
        super();

        this.inizio.aggiungiHandler(EventoAutocompleteEnum.LuogoModificato, (evento) => this.trigger(evento));
        this.fine.aggiungiHandler(EventoAutocompleteEnum.LuogoModificato, (evento) => this.trigger(evento));
    }

    aggiungiAllaMappa(map: AppMap) {

        this.map = map;

        this.inizio.aggiungiAllaMappa(map);
        this.fine.aggiungiAllaMappa(map);
    }


    getListaPuntiGeolocalizzati(): Array<google.maps.LatLng> {
        var rVal = new Array<google.maps.LatLng>();

        if (this.inizio.luogoTrovato())
        {
            rVal.push(this.inizio.getCoordinate());
        }

        if (this.fine.luogoTrovato())
        {
            rVal.push(this.fine.getCoordinate());
        }

        this.waypoints.filter( x => x.luogoTrovato()).forEach( x => rVal.push(x.getCoordinate()));

        return rVal;
    }

    getWaypoints(): WaypointsPercorso {
        
        if (!this.inizio.luogoTrovato() || !this.fine.luogoTrovato())
        {
            return null;
        }

        return {
            inizio: this.inizio.getCoordinate(),
            fine: this.fine.getCoordinate(),
            puntiIntermedi: this.waypoints.filter( x => x.luogoTrovato()).map( x => x.getCoordinate())
        };
    }

    aggiungiWaypoint(wp: AppAutocomplete) {
        wp.aggiungiHandler(EventoAutocompleteEnum.LuogoModificato, (evento) => this.trigger(evento));
        wp.aggiungiAllaMappa(this.map);

        this.waypoints.push(wp);
    }

    rimuoviWaypoint(autocomplete: AppAutocomplete) {
        
        const idx = this.waypoints.indexOf(autocomplete);

        if (idx != -1) {
            this.waypoints.splice(idx, 1);

            autocomplete.destroy();

            this.trigger(EventoAutocompleteEnum.LuogoModificato);
        }
    }


    salvaStato() {
        return {
            inizio: this.inizio.getIndirizzo(),
            arrivo: this.fine.getIndirizzo(),
            puntiIntermedi: this.waypoints.map( x => x.getIndirizzo())
        };
    }


}