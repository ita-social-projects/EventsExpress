import initialState from '../store/initialState';

import {
    SET_EVENT_ERROR, SET_EVENT_PENDING, SET_EVENT_SUCCESS, EVENT_WAS_CREATED
}from '../actions/add-event';

export const reducer = (state = initialState.add_event, action) => {

    switch(action.type){
        
        case SET_EVENT_ERROR:
            return {
                ...state,
                isEventPending: false,
                eventError: action.payload
            };
        case SET_EVENT_PENDING:
            return {
                ...state,
                isEventPending: action.payload
            };
        case SET_EVENT_SUCCESS:
            return {
                ...state,
                isEventPending: false,
                isEventSuccess: action.payload
            };
        case EVENT_WAS_CREATED:
            return {
                ...state,
                lastEventId: action.payload
            };
        default:
            break;
    }
    return state;
};