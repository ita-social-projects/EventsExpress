
import initialState from '../store/initialState';
import {
    SET_CATEGORIES_ERROR, SET_CATEGORIES_PENDING, GET_CATEGORIES_SUCCESS
} from '../actions/category-list';
import { SET_CATEGORY_EDITED } from '../actions/delete-category'

export const reducer = (state = initialState.categories, action) => {
    switch (action.type) {
        case SET_CATEGORIES_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
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