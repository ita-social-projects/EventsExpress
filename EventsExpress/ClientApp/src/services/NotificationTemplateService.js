import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class NotificationTemplateService {

    getAll = () => baseService.getResource(`NotificationTemplate/All`);

    getByIdAsync = id => baseService.getResource(`NotificationTemplate/${id}/Get`);

    getProperties = template_id => baseService.getResource(`NotificationTemplate/${template_id}/GetTemplateProperties`);

    updateAsync = data => baseService.setResource(`NotificationTemplate/Edit`, data);
}
