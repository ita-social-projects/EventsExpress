import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CategoryGroupService {

    getAllCategoryGroups = () => baseService.getResource('categoryGroup/all');

    getGroupById = (id) => baseService.getResource(`categoryGroup/${id}`);

}