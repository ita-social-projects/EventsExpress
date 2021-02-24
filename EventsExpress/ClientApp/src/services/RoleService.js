import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class RoleService {

    getRoles = () => baseService.getResource('roles');
}
