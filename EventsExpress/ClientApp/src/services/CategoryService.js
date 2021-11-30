import EventsExpressService from "./EventsExpressService";

const baseService = new EventsExpressService();

export default class CategoryService {
  getAllCategories = () => baseService.getResource("category/all");

  getCategoriesByGroup = (id) => baseService.getResource(`category/all/${id}`);

  getUserCategories = () => baseService.getResource("Users/GetCategories");

  setCategory = (data) =>
    baseService.setResource("category/create", {
      name: data.name,
      categoryGroup: {
        id: data.categoryGroup.id,
        title: data.categoryGroup.title,
      },
    });

  editCategory = (data) =>
    baseService.setResource("category/edit", {
      id: data.id,
      name: data.name,
      categoryGroup: {
        id: data.categoryGroup.id,
        title: data.categoryGroup.title,
      },
    });

  setCategoryDelete = (data) =>
    baseService.setResource(`category/delete/${data}`);
}
