import initialState from '../../store/initialState';

import {
    SET_CATEGORY_PENDING, SET_CATEGORY_SUCCESS
} from '../../actions/category/category-add-action';

export const reducer = (state = initialState.add_category, action) => {
    switch (action.type) {   

        case SET_CATEGORY_PENDING:
            return {
                ...state,
                categoryError: null,
                isCategoryPending: action.payload
            };
        case SET_CATEGORY_SUCCESS:
            return {
                ...state,
                categoryError: null
            };
        default:
            break;
    }
    return state;
};