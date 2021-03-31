import initialState from '../store/initialState';
import {
    SET_EVENTS_PROFILE_PENDING, GET_EVENTS_PROFILE_SUCCESS
} from '../actions/events/events-for-profile-action';

export const reducer = (state = initialState.events_for_profile, action) => {
    switch (action.type) {
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
