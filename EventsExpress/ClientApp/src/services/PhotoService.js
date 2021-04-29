import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class PhotoService {
    getPreviewEventPhoto = id => baseService.getPhoto(`photo/GetPreviewEventPhoto?id=${id}`, "eventPreviewPhotoImg" + id);

    getFullEventPhoto = id => baseService.getPhoto(`photo/GetFullEventPhoto?id=${id}`, "eventFullPhotoImg" + id);
}