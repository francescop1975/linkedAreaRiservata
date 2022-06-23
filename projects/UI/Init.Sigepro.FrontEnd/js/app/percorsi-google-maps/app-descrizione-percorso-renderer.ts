import { AppElementoIndicazioni } from './app-elemento-indicazioni.js';

/*
* Oggetto che si occupa di disegnare su un elemento della pagina un percorso risultante da una ricerca sulle
* indicazioni stradali di google
*/
export class AppDescrizionePercorsoRenderer {
    constructor(private contenitore: HTMLElement, private templateElemento: HTMLElement) {
    }

    disegna(percorso: google.maps.DirectionsResult) {
        this.contenitore.innerHTML = '';

        //const delay = 800;
        //var idx = 0;

        percorso.routes[0].legs.forEach((leg) => {


            leg.steps.forEach((step) => {

                //idx++;

                const el = $(this.templateElemento.innerHTML)[0];
                const elemento = new AppElementoIndicazioni(el);

                //setTimeout(() => {
                    //this.geocodificaStep(step.start_location, elemento);
                
                    //elemento.setDistanzaPercorsa(step.distance.value);                
                //}, delay * idx);
                elemento.setDistanzaPercorsaStr(step.distance.text);

                el.getElementsByClassName('indicazioni')[0].innerHTML = step.instructions;

                this.contenitore.appendChild(el);
            });
        });
    }
    /*
    geocodificaStep(location: google.maps.LatLng, element: AppElementoIndicazioni) {
        const geocoder = new google.maps.Geocoder();
        const geocoderOptions = {
            location: location,
        };


        geocoder.geocode(
            geocoderOptions,
            (results, status) => {
                if (status == google.maps.GeocoderStatus.OK) {
                    console.log('geocode results', results);

                    const via = this.estraiValoreDaRisultato(results[0].address_components, 'route');
                    const citta = this.estraiValoreDaRisultato(results[0].address_components, 'locality');
                    const comune = this.estraiValoreDaRisultato(results[0].address_components, 'administrative_area_level_3');
                    const provincia = this.estraiValoreDaRisultato(results[0].address_components, 'administrative_area_level_2');

                    element.setVia(via);
                    element.setCitta(citta);
                    element.setComune(comune);
                    element.setProvincia(provincia);
                    element.setVia(via);

                    return;
                }

                if (status == google.maps.GeocoderStatus.OVER_QUERY_LIMIT) {
                    setTimeout(() => {
                        this.geocodificaStep(location, element);
                    }, 2000);
                }
                else {
                    console.error('geocodificaStep: Impossibile determinare la posizione del marcatore.' + status);
                }
            });

    }
    estraiValoreDaRisultato(address_components: google.maps.GeocoderAddressComponent[], nomeCampo: string): string {
        const component = address_components.find(x => x.types.indexOf(nomeCampo) != -1);

        if (component) {
            return component.short_name;
        }

        return '';
    }
    */

}