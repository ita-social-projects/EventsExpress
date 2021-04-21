import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class NotificationTemplateService {
    
    getAll = () => baseService.getResource(`NotificationTemplate/All`);
    
    getByIdAsync = id => baseService.getResource(`NotificationTemplate/${id}/Get`);

    updateAsync = data => baseService.setResource(`NotificationTemplate/Edit`, data);
}
