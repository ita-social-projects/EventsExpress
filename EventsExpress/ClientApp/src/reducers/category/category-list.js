
import initialState from '../../store/initialState';
import {
    SET_CATEGORIES_PENDING, GET_CATEGORIES_SUCCESS
} from '../../actions/category/category-list-action';
import { SET_CATEGORY_EDITED } from '../../actions/category/category-add-action'

export const reducer = (state = initialState.categories, action) => {
    switch (action.type) {
        case SET_CATEGORIES_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_CATEGORIES_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        case SET_CATEGORY_EDITED:
            return {
                ...state,
                editedCategory: action.payload
            }
        default:
            return state;
    }
}  