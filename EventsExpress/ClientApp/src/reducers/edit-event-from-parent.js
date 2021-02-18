import initialState from '../store/initialState';

import {
    SET_EVENT_FROM_PARENT_PENDING, SET_EVENT_FROM_PARENT_SUCCESS
} from '../actions/event-copy-with-edit-action';

export const reducer = (state = initialState.edit_event_from_parent, action) => {

    switch (action.type) {
        case SET_EVENT_FROM_PARENT_PENDING:
            return {
                ...state,
                isEventFromParentPending: action.payload
            };
        case SET_EVENT_FROM_PARENT_SUCCESS:
            return {
                ...state,
                isEventFromParentPending: false,
                isEventFromParentSuccess: action.payload
            };
        default:
            break;
    }
    return state;
};