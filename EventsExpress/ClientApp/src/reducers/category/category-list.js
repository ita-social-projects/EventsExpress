﻿
import initialState from '../../store/initialState';
import { GET_CATEGORIES_DATA } from '../../actions/category/category-list-action';
import { GET_CATEGORIES_BY_GROUP_ID } from '../../actions/category/category-list-by-groupid-action';
import { SET_CATEGORY_EDITED } from '../../actions/category/category-add-action'

export const reducer = (state = initialState.categories, action) => {
    switch (action.type) {
        case GET_CATEGORIES_BY_GROUP_ID:
        case GET_CATEGORIES_DATA:
            return {
                ...state,
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