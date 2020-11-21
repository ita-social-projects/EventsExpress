import initialState from '../store/initialState';

import {
    SET_EVENT_SCHEDULE_ERROR, SET_EVENT_SCHEDULE_PENDING, SET_EVENT_SCHEDULE_SUCCESS
} from '../actions/add-eventSchedule';

export const reducer = (state = initialState.add_eventSchedule, action) => {

    switch (action.type) {

        case SET_EVENT_SCHEDULE_ERROR:
            return {
                ...state,
                isEventSchedulePending: false,
                eventScheduleError: action.payload
            };
        case SET_EVENT_SCHEDULE_PENDING:
            return {
                ...state,
                isEventSchedulePending: action.payload
            };
        case SET_EVENT_SCHEDULE_SUCCESS:
            return {
                ...state,
                isEventSchedulePending: false,
                isEventScheduleSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};