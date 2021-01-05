
import initialState from '../store/initialState';
import {
    SET_EVENTS_PROFILE_ERROR, SET_EVENTS_PROFILE_PENDING, GET_EVENTS_PROFILE_SUCCESS
} from '../actions/events-for-profile';

export const reducer = (
    state = initialState.events_for_profile,
    action
) => {
    switch (action.type) {
        case SET_EVENTS_PROFILE_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
        case SET_EVENTS_PROFILE_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_EVENTS_PROFILE_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        default:
            return state;
    }
}  