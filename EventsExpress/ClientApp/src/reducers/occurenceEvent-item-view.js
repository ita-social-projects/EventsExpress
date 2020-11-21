
import initialState from '../store/initialState';
import {
    GET_OCCURENCE_EVENT_ERROR, GET_OCCURENCE_EVENT_PENDING, GET_OCCURENCE_EVENT_SUCCESS, RESET_OCCURENCE_EVENT
} from '../actions/occurenceEvent-item-view';


export const reducer = (
    state = initialState.occurenceEvent,
    action
) => {
    switch (action.type) {
        case GET_OCCURENCE_EVENT_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
        case GET_OCCURENCE_EVENT_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_OCCURENCE_EVENT_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        case RESET_OCCURENCE_EVENT:
            return {
                ...initialState.occurenceEvent
            }
        default:
            return state;
    }
}  