import initialState from '../store/initialState';

import {
    SET_INVENTAR_SUCCESS, SET_INVENTAR_PENDING, SET_INVENTAR_ERROR
} from '../actions/add-inventar';

export const reducer = (state = initialState.add_inventar, action) => {

    switch(action.type){
        
        case SET_INVENTAR_SUCCESS:
            return {
                ...state,
                isInventarPending: false,
                inventarError: action.payload
            };
        case SET_INVENTAR_PENDING:
            return {
                ...state,
                isInventarPending: action.payload
            };
        case SET_INVENTAR_ERROR:
            return {
                ...state,
                isInventarPending: false,
                isInventarSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};