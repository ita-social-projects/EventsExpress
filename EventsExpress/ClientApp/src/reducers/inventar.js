import initialState from '../store/initialState';
import { SET_INVENTAR_PENDING, GET_INVENTAR_SUCCESS, SET_INVENTAR_ERROR} from '../actions/inventar';

export const reducer = (state = initialState.inventar, action) => {
    switch (action.type) {
        case SET_INVENTAR_ERROR:
            return {
                ...state,
                isError: action.payload
            };
        case SET_INVENTAR_PENDING:
            return {
                ...state,
                isPending: true
            };
        case GET_INVENTAR_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            };
        default:
            return state;
    }
}