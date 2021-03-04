import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class TrackService {

    getAll = async (filter) => {
        console.log("filter", filter)
        const res = await baseService.setResource(`tracks/all`, filter);
        return !res.ok
            ? { error: await res.text() }
            : res.json();
    }
}