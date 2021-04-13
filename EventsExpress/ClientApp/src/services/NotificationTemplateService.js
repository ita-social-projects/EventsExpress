import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class NotificationTemplateService {

    addAsync = async (data) => {
        const res = await baseService.setResource('NotificationTemplate/Add', data)
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getAll = (pageNumber = 1, pageSize = 10) => baseService.getResourceNew(`NotificationTemplate/All?page=${pageNumber}&pageSize=${pageSize}`);
    
    getByIdAsync = id => baseService.getResourceNew(`NotificationTemplate/${id}/Get`);

    updateAsync = async data => {
        const res = await baseService.setResource(`NotificationTemplate/${data.id}/Edit`, data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}