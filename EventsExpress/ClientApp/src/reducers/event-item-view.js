
import initialState from '../store/initialState';
import {
    GET_EVENT_DATA, RESET_EVENT, event
} from '../actions/event/event-item-view-action';
import { getRate, getAverageRate } from '../actions/rating-action'


export const reducer = (
    state = initialState.event,
    action
) => {
    switch (action.type) {
        case GET_EVENT_DATA:
            return {
                ...state,
                data: action.payload
            }
        case event.CHANGE_STATUS:
            let stateChangeEvent = { ...state };
            stateChangeEvent.data.eventStatus = action.payload.eventStatus;
            return stateChangeEvent;
        case RESET_EVENT:
            return {
                ...initialState.event
            }
        case getRate.SUCCESS:
            return {
                ...state,
                myRate: action.payload
            }

        case getAverageRate.SUCCESS:
            return {
                ...state,
                averageRate: action.payload
            }
        default:
            return state;
    }
}  