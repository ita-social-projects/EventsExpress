import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class NotificationTypeService {

    getAllNotificationTypes = async () => {
        return await baseService.getResource('notificationType/All');
    }
}