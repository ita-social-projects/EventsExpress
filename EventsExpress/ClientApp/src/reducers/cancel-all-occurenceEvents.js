import initialState from '../store/initialState';

import {
    SET_CANCEL_ALL_EVENT_ERROR, SET_CANCEL_ALL_EVENT_PENDING, SET_CANCEL_ALL_EVENT_SUCCESS
}from '../actions/cancel-all-occurenceEvents';

export const reducer = (state = initialState.cancel_occurenceEvents, action) => {

    switch(action.type){
        
        case SET_CANCEL_ALL_EVENT_ERROR:
            return {
                ...state,
                isCancelOccurenceEventsPending: false,
                cancelOccurenceEventsError: action.payload
            };
        case SET_CANCEL_ALL_EVENT_PENDING:
            return {
                ...state,
                isCancelOccurenceEventsPending: action.payload
            };
        case SET_CANCEL_ALL_EVENT_SUCCESS:
            return {
                ...state,
                isCancelOccurenceEventsPending: false,
                isCancelOccurenceEventsSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};