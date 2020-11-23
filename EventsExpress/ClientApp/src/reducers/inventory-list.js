import initialState from '../store/initialState';
import { SET_INVENTORY_PENDING, GET_INVENTORY_SUCCESS, SET_INVENTORY_ERROR} from '../actions/inventory-list';

export const reducer = (state = initialState.inventories, action) => {
    switch (action.type) {
        case SET_INVENTORY_ERROR:
            return {
                ...state,
                isError: action.payload
            };
        case SET_INVENTORY_PENDING:
            return {
                ...state,
                isPending: true
            };
        case GET_INVENTORY_SUCCESS:
            return {
                ...state,
                isPending: false,
                items: action.payload
            };
        default:
            return state;
    }
}