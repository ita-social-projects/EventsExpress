import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class NotificationTemplateService {
    addAsync = async (data) => {
        const res = await baseService.setResource('NotificationTemplate/Add', {
            id: data.id,
            title: data.title,
            subject: data.subject,
            messageText: data.messageText
        })
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    deleteByIdAsync = async id => {
        const res = await baseService.setResource(`NotificationTemplate/${id}/Delete`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getAllAsync = async () => await baseService.getResource(`NotificationTemplate/All`);

    getByIdAsync = async id => await baseService.getResource(`NotificationTemplate/${id}/Get`);
    
    updateAsync = async data => {
        const res = await baseService.setResource(`NotificationTemplate/${data.id}/Edit`, {
            id: data.id,
            title: data.title,
            subject: data.subject,
            messageText: data.messageText
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}