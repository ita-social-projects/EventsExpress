import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class NotificationTemplateService {
    
    getAll = (pageNumber = 1, pageSize = 10) => baseService.getResourceNew(`NotificationTemplate/All?page=${pageNumber}&pageSize=${pageSize}`);
    
    getByIdAsync = id => baseService.getResourceNew(`NotificationTemplate/${id}/Get`);

    updateAsync = data => baseService.setResource(`NotificationTemplate/Edit`, data);
}
