import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CategoryService {

    getAllCategories = async () => {
        const res = await baseService.getResource('category/all');
        return res;
    }

    setCategory = async (data) => {
        const res = await baseService.setResource('category/edit', {
            Id: data.Id,
            Name: data.Name
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setCategoryDelete = async (data) => {
        const res = await baseService.setResource(`category/delete/${data}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}