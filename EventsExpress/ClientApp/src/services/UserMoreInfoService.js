import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class UserMoreInfoService {
    create = (data) => baseService.setResource("userMoreInfo/create", data);
}
