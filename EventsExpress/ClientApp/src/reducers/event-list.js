import { blockEvent, unBlockEvent } from '../actions/event-item-view'
import initialState from '../store/initialState';
import {
    SET_EVENTS_ERROR,
    SET_EVENTS_PENDING,
    GET_EVENTS_SUCCESS,
    RESET_EVENTS,
    UPDATE_EVENTS_FILTERS
} from '../actions/event-list';

export const reducer = (state = initialState.events, action) => {
    switch (action.type) {
        case SET_EVENTS_ERROR:
            return {
                ...state,
                isError: action.payload
            };
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
            }
        case blockEvent.UPDATE: {
            let newState = { ...state };
            newState.data.items = state.data.items.map((item) => {
                if (item.id === action.payload) {
                    let updatedItem = item;
                    updatedItem.isBlocked = true;
                    return updatedItem;
                }
                return item;
            });
            return newState;
        }
        case unBlockEvent.UPDATE: {
            let newState = { ...state };
            newState.data.items = state.data.items.map((item) => {
                if (item.id === action.payload) {
                    let updatedItem = item;
                    updatedItem.isBlocked = false;
                    return updatedItem;
                }
                return item;
            });
            return newState;
        }
        case RESET_EVENTS:
            return initialState.events;
        case UPDATE_EVENTS_FILTERS:
            return {
                ...state,
                searchParams: action.payload,
            };
        default:
            return state;
    }
}
