import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class PhotoService{
    getPreviewEventPhoto = id => baseService.getResource(`/photo/GetPreviewEventPhoto?id=${id}`);

    getFullEventPhoto = id => baseService.getResource(`/photo/GetFullEventPhoto?id=${id}`);

    getUserPhoto = id => baseService.getResource(`/photo/GetUserPhoto?id=${id}`);
}