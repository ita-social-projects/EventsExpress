import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class NotificationTypeService {

    getAllNotificationTypes = () => {
        return baseService.getResource('notificationType/All');
    }
}