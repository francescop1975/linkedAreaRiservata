
export interface LocalizzazionePunto 
{
    indirizzo: string,
    location: google.maps.LatLngLiteral
}


export interface PercorsoSalvato {
    waypoints: {
        inizio: LocalizzazionePunto,
        arrivo: LocalizzazionePunto,
        puntiIntermedi: LocalizzazionePunto[]
    },
    percorso: google.maps.DirectionsResult
}