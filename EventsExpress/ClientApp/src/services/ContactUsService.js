import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class ContactUsService {

    setContactUs = data => baseService.setResource('contactUs/ContactAdmins', data);

    getAllIssues = filters => baseService.getResourceNew(`contactUs/All${filters}`);

    updateIssueStatus = data => baseService.setResource(`contactUs/${data.MessageId}/UpdateStatus`, data);

    getMessage = id => baseService.getResourceNew(`contactUs/${id}`, id);
}
