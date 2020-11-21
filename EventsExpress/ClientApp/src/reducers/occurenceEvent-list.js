import initialState from '../store/initialState';
import {
    SET_OCCURENCE_EVENTS_ERROR,
    SET_OCCURENCE_EVENTS_PENDING,
    GET_OCCURENCE_EVENTS_SUCCESS,
    RESET_OCCURENCE_EVENTS,
} from '../actions/occurenceEvent-list';

export const reducer = (state = initialState.occurenceEvents, action) => {
    switch (action.type) {
        case SET_OCCURENCE_EVENTS_ERROR:
            return {
                ...state,
                isError: action.payload
            };
        case SET_OCCURENCE_EVENTS_PENDING:
            return {
                ...state,
                isPending: true
            };
        case GET_OCCURENCE_EVENTS_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            };
        case RESET_OCCURENCE_EVENTS:
            return initialState.occurenceEvents;
        default:
            return state;
    }
}
