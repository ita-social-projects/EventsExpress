import initialState from '../store/initialState';

import {
    SET_CANCEL_NEXT_EVENT_ERROR, SET_CANCEL_NEXT_EVENT_PENDING, SET_CANCEL_NEXT_EVENT_SUCCESS
}from '../actions/cancel-next-eventSchedule';

export const reducer = (state = initialState.cancel_next_eventSchedule, action) => {

    switch(action.type){
        
        case SET_CANCEL_NEXT_EVENT_ERROR:
            return {
                ...state,
                isCancelNextEventSchedulePending: false,
                cancelNextEventScheduleError: action.payload
            };
        case SET_CANCEL_NEXT_EVENT_PENDING:
            return {
                ...state,
                isCancelNextEventSchedulePending: action.payload
            };
        case SET_CANCEL_NEXT_EVENT_SUCCESS:
            return {
                ...state,
                isCancelNextEventSchedulePending: false,
                isCancelNextEventScheduleSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};