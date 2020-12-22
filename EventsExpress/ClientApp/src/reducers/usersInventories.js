import initialState from '../store/initialState';
import { SET_USERSINVENTORIES_ERROR, SET_USERSINVENTORIES_PENDING, GET_USERSINVENTORIES_SUCCESS } from '../actions/usersInventories';


export const reducer = (state = initialState.usersInventories, action) => {
    switch (action.type) {
        case SET_USERSINVENTORIES_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            };
        case SET_USERSINVENTORIES_PENDING:
            return {
                ...state,
                isPending: true,
                isError: null
            };
        case GET_USERSINVENTORIES_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            };
        default:
            return state;
    }
}