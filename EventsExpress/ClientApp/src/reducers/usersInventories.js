import initialState from '../store/initialState';
import { SET_USERSINVENTORIES_PENDING, GET_USERSINVENTORIES_SUCCESS } from '../actions/users/users-inventories-action';


export const reducer = (state = initialState.usersInventories, action) => {
    switch (action.type) {
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