import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class TrackService {

    getAll = async (filter) => {
        return await baseService.setResource(`tracks/all`, filter);
    }
    
    getEntityNames = async () => {
        return await baseService.getResource(`tracks/getEntityNames`);
    }
}