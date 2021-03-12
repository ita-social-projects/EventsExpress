import { event } from '../actions/event/event-item-view';
import initialState from '../store/initialState';
import {
    SET_EVENTS_PENDING,
    GET_EVENTS_SUCCESS,
    RESET_EVENTS,
    UPDATE_EVENTS_FILTERS
} from '../actions/event/event-list-action';

export const reducer = (state = initialState.events, action) => {
    switch (action.type) {
        case SET_EVENTS_PENDING:
            return {
                ...state,
                isPending: true
            };
        case GET_EVENTS_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            };
        case event.CHANGE_STATUS:
            let stateChangeEvent = { ...state };
            stateChangeEvent.data.items = state.data.items.map((item) => {
                if (item.id === action.payload.eventId) {
                    let updatedItem = item;
                    updatedItem.eventStatus = action.payload.eventStatus;
                    return updatedItem;
                }
                return item;
            });
            return stateChangeEvent;
        case RESET_EVENTS:
            return initialState.events;
        case UPDATE_EVENTS_FILTERS:
            return {
                ...state,
                filter: action.payload,
            };
        default:
            return state;
    }
}
