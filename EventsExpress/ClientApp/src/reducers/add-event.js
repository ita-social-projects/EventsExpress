import initialState from '../store/initialState';

import {
    SET_EVENT_PENDING, SET_EVENT_SUCCESS
} from '../actions/event/event-add-action';

export const reducer = (state = initialState.add_event, action) => {

    switch(action.type){
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
        default:
            break;
    }
    return state;
};