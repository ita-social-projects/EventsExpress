import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class ConfigService {
    getConfigsFromBack = () => baseService.getResource('FrontConfigs/GetConfigs');
}
