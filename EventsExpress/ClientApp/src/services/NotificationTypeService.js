import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class NotificationTypeService {

    getAllNotificationTypes = () => baseService.getResourceNew('notificationType/All');
}