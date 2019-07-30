import initialState from '../store/initialState';
import {
    SET_CATEGORY_DELETE_PENDING,
    SET_CATEGORY_DELETE_SUCCESS,
    SET_CATEGORY_DELETE_ERROR
} from "../actions/delete-category";

export const reducer = (state = initialState.add_category, action) => {

    switch (action.type) {
        case SET_CATEGORY_DELETE_PENDING:
            return Object.assign({}, state, {
                isCategoryDeletePending: action.isCategoryDeletePending
            });

        case SET_CATEGORY_DELETE_SUCCESS:
            return Object.assign({}, state, {
                isCategoryDeleteSuccess: action.isCategoryDeleteSuccess
            });

        case SET_CATEGORY_DELETE_ERROR:
            return Object.assign({}, state, {
                categoryDeleteError: action.categoryDeleteError
            });

        default:
            return state;
    }
}