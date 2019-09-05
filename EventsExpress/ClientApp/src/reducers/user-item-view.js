
import initialState from '../store/initialState';
import {
    GET_PROFILE_ERROR, GET_PROFILE_PENDING, GET_PROFILE_SUCCESS, RESET_USER
} from '../actions/user-item-view';

export const reducer = (
    state = initialState.profile,
    action
) => {
    switch (action.type) {
        case GET_PROFILE_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
        case GET_PROFILE_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_PROFILE_SUCCESS:
            return {
                ...state,
                isPending: false,
                isError: false,
                data: action.payload
            }
        case RESET_USER:
            return initialState.profile;
        default:
            return state;
    }
}  