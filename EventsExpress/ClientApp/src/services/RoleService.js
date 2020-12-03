import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class RoleService {

    getRoles = async () => {
        const res = await baseService.getResource('roles');
        return res;
    }
}