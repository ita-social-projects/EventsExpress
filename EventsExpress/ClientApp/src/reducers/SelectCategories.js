import {
    SELECT_CATEGORIES_SECCESS,
    SELECT_CATEGORIES_ERROR
} from "../actions/SelectCategories";

import initialState from '../store/initialState';

export const reducer = (
    state = initialState.SelectCategories,
    action
) => {
    switch (action.type) {
        case SELECT_CATEGORIES_SECCESS:
            return Object.assign({}, state, {
                IsSelectCategoriesSeccess: action.IsSelectCategoriesSeccess,
                IsSelectCategoriesError: null

            });

        case SELECT_CATEGORIES_ERROR:
            return Object.assign({}, state, {
                IsSelectCategoriesError: action.IsSelectCategoriesError,
                IsSelectCategoriesSeccess: false
            });
        default:
            return state;
    }
}