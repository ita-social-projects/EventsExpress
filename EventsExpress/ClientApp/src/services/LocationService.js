import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class LocationService {

    getCountries = async () => {
        const res = await baseService.getResource('locations/countries');
        return res;
    }

    getCities = async (country) => {
        const res = await baseService.getResource(`locations/country:${country}/cities`);
        return res;
    }
}