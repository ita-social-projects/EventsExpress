
import initialState from '../store/initialState';
import {
     GET_PROFILE_DATA, RESET_USER
} from '../actions/user/user-item-view-action';

export const reducer = (
    state = initialState.profile,
    action
) => {
    switch (action.type) {
        case GET_PROFILE_DATA:
            return {
                ...state,
                isError: false,
                data: action.payload
            }
        case RESET_USER:
            return initialState.profile;
        default:
            return state;
    }
}  