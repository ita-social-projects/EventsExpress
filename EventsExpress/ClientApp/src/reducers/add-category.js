import initialState from '../store/initialState';

import {
    SET_CATEGORY_ERROR, SET_CATEGORY_PENDING, SET_CATEGORY_SUCCESS
} from '../actions/add-category';

export const reducer = (state = initialState.add_category, action) => {

    switch (action.type) {

        case SET_CATEGORY_ERROR:
            return {
                ...state,
                categoryError: action.payload
            };
        case SET_CATEGORY_PENDING:
            return {
                ...state,
                isCategoryPending: action.payload
            };
        case SET_CATEGORY_SUCCESS:
            return {
                ...state,
                isCategorySuccess: action.payload
            };
    }
    return state;
};