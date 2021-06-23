import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class ConfigService {
    getConfigFromBack = () => baseService.getResource('FrontConfigs/GetConfigs');
}
