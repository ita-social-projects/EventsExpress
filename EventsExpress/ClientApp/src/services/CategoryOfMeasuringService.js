import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CategoryOfMeasuringService {
    getCategoriesOfMeasuring = async () => baseService.getResource('categoryOfMeasuring/getAll');
}

