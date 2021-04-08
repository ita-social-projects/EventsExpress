import initialState from '../store/initialState';
import {
    GET_LINKED_AUTHS_SUCCESS,
} from '../actions/editProfile/linked-auths-action';

export const reducer = (state = initialState.account, action) => {
    switch (action.type) {
        case GET_LINKED_AUTHS_SUCCESS:
            return {
                ...state,
                linkedAuths: action.payload
            };
        default:
            break;
    }
    return state;
}
