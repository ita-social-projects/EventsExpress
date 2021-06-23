import initialState from '../store/initialState';
import { GET_EVENTS_PROFILE_DATA } from '../actions/events/events-for-profile-action';

export const reducer = (state = initialState.events_for_profile, action) => {
    switch (action.type) {
        case GET_EVENTS_PROFILE_DATA:
            return {
                ...state,
                data: action.payload
            }
        default:
            return state;
    }
}
