import * as Geocoding from 'esri-leaflet-geocoder';

export default function geocodeCoords(latlng, resFunc) {
    var geocodeService = Geocoding.geocodeService();
    return geocodeService.reverse()
    .latlng(latlng)
    .language("en")
    .run(resFunc);
}