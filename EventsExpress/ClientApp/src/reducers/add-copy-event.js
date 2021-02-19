import initialState from '../store/initialState';

import {
    SET_COPY_EVENT_PENDING, SET_COPY_EVENT_SUCCESS
} from '../actions/event-copy-without-edit-action';

export const reducer = (state = initialState.add_copy_event, action) => {

    switch (action.type) {
        case SET_COPY_EVENT_PENDING:
            return {
                ...state,
                isCopyEventPending: action.payload
            };
        case SET_COPY_EVENT_SUCCESS:
            return {
                ...state,
                isCopyEventPending: false,
                isCopyEventSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};