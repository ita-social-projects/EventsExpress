import {
    SELECT_CATEGORIES_SECCESS,
    SELECT_CATEGORIES_ERROR
} from "../actions/SelectCategories";
import { reducer } from "./login";
export const reducer = (
    state = {
        IsSelectCategoriesSeccess: false,
        IsSelectCategoriesError: 
    }
)