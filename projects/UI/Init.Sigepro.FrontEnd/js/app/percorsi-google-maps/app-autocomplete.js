import { AppEventHandler } from './app-event-handler.js';
export var EventoAutocompleteEnum;
(function (EventoAutocompleteEnum) {
    EventoAutocompleteEnum[EventoAutocompleteEnum["LuogoModificato"] = 0] = "LuogoModificato";
})(EventoAutocompleteEnum || (EventoAutocompleteEnum = {}));
export class AppAutocomplete extends AppEventHandler {
    constructor(input, bounds) {
        super();
        this.input = input;
        this.bounds = bounds;
        this.location = null;
        this.autocomplete = null;
        this.marker = null;
        this.countryRestrict = { 'country': 'it' };
        this.autocomplete = new google.maps.places.Autocomplete(this.input, {
            types: ['address'],
            componentRestrictions: this.countryRestrict,
            strictBounds: true,
            fields: ['address_component', 'geometry'],
        });
        if (this.bounds) {
            this.autocomplete.setBounds(this.bounds);
        }
        this.autocomplete.addListener('place_changed', () => {
            const place = this.autocomplete.getPlace();
            this.marker.setVisible(false);
            this.location = null;
            if (place.geometry) {
                this.location = place.geometry.location;
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
    onMarkerDragEnd() {
        const geocoder = new google.maps.Geocoder();
        const geocoderOptions = {
            location: this.marker.getPosition(),
        };
        geocoder.geocode(geocoderOptions, (results, status) => {
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
    aggiungiAllaMappa(map) {
        map.aggiungiMarker(this.marker);
    }
    luogoTrovato() {
        return this.location !== null;
    }
    getCoordinate() {
        return this.location;
    }
    destroy() {
        this.eliminaHandlers();
        this.marker.setMap(null);
    }
    getIndirizzo() {
        return {
            indirizzo: this.input.value,
            location: this.location.toJSON()
        };
    }
    triggerSearch() {
        setTimeout(() => {
            google.maps.event.trigger(this.input, 'focus', {});
            setTimeout(() => google.maps.event.trigger(this.input, 'keydown', {
                keyCode: 40,
                stopPropagation: () => { },
                preventDefault: () => { },
            }), 300);
            setTimeout(() => google.maps.event.trigger(this.input, 'keydown', { keyCode: 13 }), 1000);
        }, 400);
    }
    inizializza(posizione) {
        this.input.value = posizione.indirizzo;
        this.location = new google.maps.LatLng(posizione.location);
        this.marker.setVisible(false);
        this.marker.setPosition(this.location);
        this.marker.setVisible(true);
    }
}
//# sourceMappingURL=app-autocomplete.js.map