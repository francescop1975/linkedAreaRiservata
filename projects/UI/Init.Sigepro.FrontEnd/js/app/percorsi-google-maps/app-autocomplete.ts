import { AppMap } from './app-map.js';
import { AppEventHandler } from './app-event-handler.js';
import { LocalizzazionePunto } from './app-percorso-salvato.js';

export enum EventoAutocompleteEnum {
    LuogoModificato
}

export class AppAutocomplete extends AppEventHandler<EventoAutocompleteEnum> {


    private location: google.maps.LatLng = null;
    private autocomplete: google.maps.places.Autocomplete = null;
    private marker: google.maps.Marker = null;

    private countryRestrict = { 'country': 'it' };


    constructor(private input: HTMLInputElement, private bounds: google.maps.LatLngBounds) {
        super();

        this.autocomplete = new google.maps.places.Autocomplete(
            this.input, {
            types: ['address'],
            componentRestrictions: this.countryRestrict,
            strictBounds: true,
            fields: ['address_component', 'geometry'],
        });

        if (this.bounds) {
            this.autocomplete.setBounds(this.bounds);
        }

        // Aggiungo un handler per l'evento di modifica
        this.autocomplete.addListener('place_changed', () => {
            const place = this.autocomplete.getPlace();

            this.marker.setVisible(false);

            this.location = null;

            if (place.geometry) {
                this.location = place.geometry.location
                this.marker.setPosition(this.location);
            }

            this.marker.setVisible(true);

            this.trigger(EventoAutocompleteEnum.LuogoModificato);
        });

        this.marker = new google.maps.Marker({
            anchorPoint: new google.maps.Point(0, 0),
            draggable: true,
            animation: google.maps.Animation.DROP,
            visible: false
        });


        this.marker.addListener('dragend', () => this.onMarkerDragEnd());
    }

    private onMarkerDragEnd(): void {
        const geocoder = new google.maps.Geocoder();
        const geocoderOptions = {
            location: this.marker.getPosition(),
        };

        geocoder.geocode(
            geocoderOptions,
            (results, status) => {
                if (status == google.maps.GeocoderStatus.OK) {

                    console.log('Geocode results: ', results);

                    this.input.value = results[0].formatted_address;
                    this.location = results[0].geometry.location;

                    this.trigger(EventoAutocompleteEnum.LuogoModificato);
                }
                else {
                    this.marker.setPosition(this.location);

                    console.error('Impossibile determinare la posizione del marcatore.' + status);
                }
            });
    }

    aggiungiAllaMappa(map: AppMap): void {
        map.aggiungiMarker(this.marker);
    }

    luogoTrovato(): boolean {
        return this.location !== null;
    }

    getCoordinate(): google.maps.LatLng {
        return this.location;
    }

    destroy(): void {
        this.eliminaHandlers();
        this.marker.setMap(null);
    }

    getIndirizzo(): LocalizzazionePunto {
        return {
            indirizzo: this.input.value,
            location: this.location.toJSON()
        };
    }

    triggerSearch(): void {


        setTimeout(() => {



            google.maps.event.trigger(this.input, 'focus', {});
            setTimeout(() => google.maps.event.trigger(this.input, 'keydown', {
                keyCode: 40,
                stopPropagation: () => { }, // because these get called
                preventDefault: () => { },
            }), 300);
            setTimeout(() => google.maps.event.trigger(this.input, 'keydown', { keyCode: 13 }), 1000);
        }, 400);
        //google.maps.event.trigger(this.input, 'keydown', { keyCode: 13 });
        //google.maps.event.trigger(this, 'focus', {});
    }

    inizializza(posizione: LocalizzazionePunto ) {
        this.input.value = posizione.indirizzo;
        this.location = new google.maps.LatLng(posizione.location);
        this.marker.setVisible(false);
        this.marker.setPosition(this.location);
        this.marker.setVisible(true);
    }
}

