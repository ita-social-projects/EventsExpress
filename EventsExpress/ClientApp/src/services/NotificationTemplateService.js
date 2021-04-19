import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class NotificationTemplateService {
    
    getAll = () => baseService.getResourceNew(`NotificationTemplate/All`);
    
    getByIdAsync = id => baseService.getResourceNew(`NotificationTemplate/${id}/Get`);

    updateAsync = data => baseService.setResource(`NotificationTemplate/Edit`, data);
}
