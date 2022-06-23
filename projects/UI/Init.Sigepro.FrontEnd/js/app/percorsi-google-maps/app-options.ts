export interface AppOptions {
    mapElement: HTMLElement;
    startPoint: HTMLInputElement;
    endPoint: HTMLInputElement;
    tabellaIndicazioni: HTMLElement;
    templateWaypoint: HTMLElement;
    contenitoreWaypoints: HTMLElement;
    mapBounds: {
        ne: google.maps.LatLngLiteral,
        sw: google.maps.LatLngLiteral
    },
    contenitoreIndicazioni: HTMLElement, 
    templateElementoIndicazioni: HTMLElement,
    contenitorePercorsoCalcolato: HTMLInputElement
}
