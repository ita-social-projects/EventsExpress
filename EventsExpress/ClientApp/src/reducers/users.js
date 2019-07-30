import initialState from '../store/initialState';
import { GET_USERS_ERROR, GET_USERS_PENDING, GET_USERS_SUCCESS } from '../actions/users';
import { blockUser, unBlockUser } from '../actions/user';

export const reducer = (state, action) => {
    state = state || initialState.users;
    switch(action.type) {
        case GET_USERS_SUCCESS:
            return{
                ...state,
                isPending: false,
                data: action.payload
            }
        case GET_USERS_PENDING:
            return{
                ...state,
                isPending: action.payload
            }

        case GET_USERS_ERROR:
            return {
                ...state,
                isError: action.payload,
                isPending: false
            }
        case blockUser.UPDATE:
            let newState = { ...state };
            newState.data = state.data.map((item) => {
                if (item.id === action.payload) {
                    let updatedItem = item;
                    updatedItem.isBlocked = true;
                    return updatedItem;
                }
                return item;
            });
            return newState;

        case unBlockUser.UPDATE:

            let newstate = { ...state };

            newstate.data = state.data.map((item) => {
                if (item.id === action.payload) {
                    let updItem = item;
                    updItem.isBlocked = false;
                    return updItem;
                }
                return item;
            });

            return newstate;
    }
    return state;
}