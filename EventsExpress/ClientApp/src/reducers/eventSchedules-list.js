import initialState from '../store/initialState';
import {
    GET_EVENTS_SCHEDULE_DATA,
    RESET_EVENTS_SCHEDULE,
} from '../actions/eventSchedule/eventSchedule-list-action';

export const reducer = (state = initialState.eventSchedules, action) => {
    switch (action.type) {
        case GET_EVENTS_SCHEDULE_DATA:
            return {
                ...state,
                data: action.payload
            };
        case RESET_EVENTS_SCHEDULE:
            return initialState.eventSchedules;
        default:
            return state;
    }
}
