import initialState from '../store/initialState';
import { GET_INVENTORY_DATA } from '../actions/inventory/inventory-list-action';

export const reducer = (state = initialState.inventories, action) => {
    switch (action.type) {
        case GET_INVENTORY_DATA:
            return {
                ...state,
                items: action.payload
            };
        default:
            return state;
    }
}