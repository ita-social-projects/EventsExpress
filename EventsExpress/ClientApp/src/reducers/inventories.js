import initialState from '../store/initialState';

import { ADD_ITEM_TO_INVENTAR } from '../actions/inventories';

export const reducer = (state = initialState.inventories, action) => {
    if (action.type == ADD_ITEM_TO_INVENTAR) {
        return {
            ...state,
            count: action.payload.length,
            items: action.payload
        };
    }
    return state;
}