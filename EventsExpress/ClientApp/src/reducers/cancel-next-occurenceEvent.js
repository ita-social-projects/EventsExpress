import initialState from '../store/initialState';

import {
    SET_CANCEL_NEXT_EVENT_ERROR, SET_CANCEL_NEXT_EVENT_PENDING, SET_CANCEL_NEXT_EVENT_SUCCESS
}from '../actions/cancel-next-occurenceEvent';

export const reducer = (state = initialState.cancel_next_occurenceEvent, action) => {

    switch(action.type){
        
        case SET_CANCEL_NEXT_EVENT_ERROR:
            return {
                ...state,
                isCancelNextOccurenceEventPending: false,
                cancelNextOccurenceEventError: action.payload
            };
        case SET_CANCEL_NEXT_EVENT_PENDING:
            return {
                ...state,
                isCancelNextOccurenceEventPending: action.payload
            };
        case SET_CANCEL_NEXT_EVENT_SUCCESS:
            return {
                ...state,
                isCancelNextOccurenceEventPending: false,
                isCancelNextOccurenceEventSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};