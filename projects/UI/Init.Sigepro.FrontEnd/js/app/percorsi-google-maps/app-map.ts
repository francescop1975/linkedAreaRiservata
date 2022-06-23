import { WaypointsPercorso } from './waypoints-percorso.js';
import { AppEventHandler } from './app-event-handler.js';

export enum AppMapEventsEnum {
    PercorsoModificato,
    InizioRicalcoloPercorso
}

export class AppMap extends AppEventHandler<AppMapEventsEnum>{

    map: google.maps.Map;
    directionsService = new google.maps.DirectionsService();
    directionsRenderer: google.maps.DirectionsRenderer;

    constructor(private mapElement: HTMLElement, private startingBounds: google.maps.LatLngBounds) {

        super();

        this.map = new google.maps.Map(this.mapElement, {
            center: { lat: -33.8688, lng: 151.2195 },
            zoom: 13,
        });

        this.directionsRenderer = new google.maps.DirectionsRenderer({
            map: this.map,
            // draggable: true,
            suppressMarkers: true
        });

        this.map.addListener('bounds_changed', () => {
            var bounds = JSON.stringify(
                {
                    ne: this.map.getBounds().getNorthEast().toJSON(),
                    sw: this.map.getBounds().getSouthWest().toJSON()
                });

            //do whatever you want with those bounds
            console.log('bounds_changed', bounds);
        });

        this.map.fitBounds(this.startingBounds);
    }

    /*
    * Aggiunge un marker alla mappa
    */
    aggiungiMarker(marker: google.maps.Marker): void {
        marker.setMap(this.map);
    }


    /*
    * Ricalcola il percorso di navigazione attraverso gli wayponts passati
    */
    ricalcolaPercorso(waypoints: WaypointsPercorso) {

        this.trigger(AppMapEventsEnum.InizioRicalcoloPercorso);

        if (waypoints === null) {
            this.directionsRenderer.setMap(null);
            this.directionsRenderer.setMap(this.map);

            return;
        }

        const options = {
            origin: waypoints.inizio,
            destination: waypoints.fine,
            waypoints: waypoints.puntiIntermedi.map(wp => {
                return {
                    location: wp
                }
            }),
            travelMode: google.maps.TravelMode.DRIVING
        };


        this.directionsService.route(options, (response, status) => {
            if (status === google.maps.DirectionsStatus.OK) {
                this.disegnaPercorso(response);
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
    }

    disegnaPercorso(percorso: google.maps.DirectionsResult){
        this.directionsRenderer.setDirections(percorso);
        console.log('directionsService.route', percorso);

        this.trigger(AppMapEventsEnum.PercorsoModificato);
    }

    /*
    * Forza la visualizzazione della mappa sui limiti passati
    */
    fitBounds(bounds: google.maps.LatLngBounds) {
        this.map.fitBounds(bounds);

        console.log('map bounds', {
            lat: this.map.getCenter().lat(),
            long: this.map.getCenter().lng()
        });
    }

    /*
    * Restituisce il percorso attualmente disegnato sulla mappa
    */
    getPercorsoAttuale(): google.maps.DirectionsResult {
        return this.directionsRenderer.getDirections();
    }
}