import initialState from '../store/initialState';

import {
    SET_OCCURENCE_EVENT_ERROR, SET_OCCURENCE_EVENT_PENDING, SET_OCCURENCE_EVENT_SUCCESS
} from '../actions/add-occurenceEvent';

export const reducer = (state = initialState.add_occurenceEvent, action) => {

    switch (action.type) {

        case SET_OCCURENCE_EVENT_ERROR:
            return {
                ...state,
                isOccurenceEventPending: false,
                occurenceEventError: action.payload
            };
        case SET_OCCURENCE_EVENT_PENDING:
            return {
                ...state,
                isOccurenceEventPending: action.payload
            };
        case SET_OCCURENCE_EVENT_SUCCESS:
            return {
                ...state,
                isOccurenceEventPending: false,
                isOccurenceEventSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};