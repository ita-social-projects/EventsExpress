import { event } from '../actions/event/event-item-view-action';
import initialState from '../store/initialState';
import {
    GET_EVENTS_DATA,
    RESET_EVENTS,
    UPDATE_EVENTS_FILTERS
} from '../actions/event/event-list-action';
import { SET_EVENTS_LAYOUT } from '../actions/events-layout';

export const reducer = (state = initialState.events, action) => {
    switch (action.type) {
        case GET_EVENTS_DATA:      
            return {
                ...state,
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
        case SET_EVENTS_LAYOUT:
            return {
                ...state,
                layout: action.payload,
            };
        default:
            return state;
    }
}
