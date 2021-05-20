import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class ContactUsService {

    setContactUs = data => baseService.setResource('contactUs/ContactAdmins', data);

    getAllIssues = filters => baseService.getResource(`contactUs/All${filters}`);

    updateIssueStatus = data => baseService.setResource(`contactUs/${data.MessageId}/UpdateStatus`, data);

    getMessage = id => baseService.getResource(`contactUs/${id}`, id);
}
