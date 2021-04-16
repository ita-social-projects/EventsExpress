import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CategoryService {

    getAllCategories = () => baseService.getResource('category/all');

    setCategory = data => baseService.setResource('category/create', {
        name: data.name
    });

    editCategory = data => baseService.setResource('category/edit', {
        id: data.id,
        name: data.name
    });

    setCategoryDelete = data => baseService.setResource(`category/delete/${data}`);
}
