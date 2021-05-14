import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class TrackService {

    getAll = async (filter) => {
        return baseService.setResource(`tracks/all`, filter);
    }

    getEntityNames = async () => {
        return baseService.getResource(`tracks/getEntityNames`);
    }
}