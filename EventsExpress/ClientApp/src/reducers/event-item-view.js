
import initialState from '../store/initialState';
import {
    GET_EVENT_ERROR, GET_EVENT_PENDING, GET_EVENT_SUCCESS, RESET_EVENT,  event 
} from '../actions/event/event-item-view';
import { getRate, getAverageRate } from '../actions/rating'


export const reducer = (
    state = initialState.event,
    action
) => {
    switch (action.type) {
        case GET_EVENT_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
        case GET_EVENT_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_EVENT_SUCCESS:
            return {
                ...state,
                isPending: false,
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