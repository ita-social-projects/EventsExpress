import initialState from '../store/initialState';
import { SET_INVENTORY_PENDING, GET_INVENTORY_SUCCESS} from '../actions/inventory-list-action';

export const reducer = (state = initialState.inventories, action) => {
    switch (action.type) {
        case SET_INVENTORY_PENDING:
            return {
                ...state,
                isPending: true,
                listInventoriesErrorMessage: null
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