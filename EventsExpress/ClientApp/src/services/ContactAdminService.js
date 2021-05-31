import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class ContactAdminService {

    setContactAdmin = data => baseService.setResource('contactAdmin/ContactAdmins', data);

    getAllIssues = filters => baseService.getResource(`contactAdmin/All${filters}`);

    updateIssueStatus = data => baseService.setResource(`contactAdmin/${data.MessageId}/UpdateStatus`, data);

    getMessage = id => baseService.getResource(`contactAdmin/${id}`, id);
}
