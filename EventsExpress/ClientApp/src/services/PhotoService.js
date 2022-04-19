import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class PhotoService {
    getPreviewEventPhoto = id => baseService.getPhoto(`EventPhoto/GetPreviewEventPhoto/${id}`);

    getFullEventPhoto = id => baseService.getPhoto(`EventPhoto/GetFullEventPhoto/${id}`);

    getUserPhoto = id => baseService.getPhoto(`UserPhoto/GetUserPhoto/${id}`);

    setEventTempPhoto = async (id, data) => {
        let photo = new FormData();
        photo.append(`Photo`, data);
        return baseService.setResourceWithData(`EventPhoto/SetEventTempPhoto/${id}`, photo)
    };

    deleteUserPhoto = id => baseService.setResource(`UserPhoto/DeleteUserPhoto/${id}`)
}