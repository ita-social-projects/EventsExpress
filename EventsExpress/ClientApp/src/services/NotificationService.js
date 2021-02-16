import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class NotificationTypeService {

    getAllNotificationTypes = async () => {
        const res = await baseService.getResource('notificationType/All');
        return res;
    }
}