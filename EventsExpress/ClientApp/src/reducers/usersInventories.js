import initialState from '../store/initialState';
import { GET_USERSINVENTORIES_DATA } from '../actions/users/users-inventories-action';


export const reducer = (state = initialState.usersInventories, action) => {
    switch (action.type) {
        case GET_USERSINVENTORIES_DATA:
            return {
                ...state,
                data: action.payload
            };
        default:
            return state;
    }
}