import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CategoryOfMeasuringService {
    getCategoriesOfMeasuring = async (data) => baseService.getResource('categoryOfMeasuring/getAll');
}

