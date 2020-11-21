import initialState from '../store/initialState';

import {
    SET_CANCEL_ALL_EVENT_ERROR, SET_CANCEL_ALL_EVENT_PENDING, SET_CANCEL_ALL_EVENT_SUCCESS
}from '../actions/cancel-all-eventSchedules';

export const reducer = (state = initialState.cancel_eventSchedules, action) => {

    switch(action.type){
        
        case SET_CANCEL_ALL_EVENT_ERROR:
            return {
                ...state,
                isCancelEventSchedulesPending: false,
                cancelEventSchedulesError: action.payload
            };
        case SET_CANCEL_ALL_EVENT_PENDING:
            return {
                ...state,
                isCancelEventSchedulesPending: action.payload
            };
        case SET_CANCEL_ALL_EVENT_SUCCESS:
            return {
                ...state,
                isCancelEventSchedulesPending: false,
                isCancelEventScheduleSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};