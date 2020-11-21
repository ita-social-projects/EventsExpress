import initialState from '../store/initialState';
import {
    SET_EVENTS_SCHEDULE_ERROR,
    SET_EVENTS_SCHEDULE_PENDING,
    GET_EVENTS_SCHEDULE_SUCCESS,
    RESET_EVENTS_SCHEDULE,
} from '../actions/eventSchedules-list';

export const reducer = (state = initialState.eventSchedules, action) => {
    switch (action.type) {
        case SET_EVENTS_SCHEDULE_ERROR:
            return  {
                ...state,
                isError: action.payload
            };
        case SET_EVENTS_SCHEDULE_PENDING:
            return {
                ...state,
                isPending: true
            };
        case GET_EVENTS_SCHEDULE_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            };
        case RESET_EVENTS_SCHEDULE:
            return initialState.eventSchedules;
        default:
            return state;
    }
}
