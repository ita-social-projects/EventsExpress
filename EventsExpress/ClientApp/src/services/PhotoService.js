import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class PhotoService {
    getPreviewEventPhoto = async id => await baseService.getPhoto(`photo/GetPreviewEventPhoto?id=${id}`);

    getFullEventPhoto = async id => await baseService.getPhoto(`photo/GetFullEventPhoto?id=${id}`);
}