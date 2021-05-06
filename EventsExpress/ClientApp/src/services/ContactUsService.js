import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class ContactUsService {

    setContactUs = data => baseService.setResource('contactUs/ContactAdmins', data);

    getAllIssues = (page) => baseService.getResourceNew(`contactUs/All?page=${page}`, page);

    setEventStatus = data => baseService.setResource(`contactUs/${data.MessageId}/SetStatus`, data);
}