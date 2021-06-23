import initialState from '../store/initialState';
import { REQUEST_INC, REQUEST_DEC } from "../actions/request-count-action"

export const reducer = (state = initialState.requestCount, action) => {
    switch (action.type) {
        case REQUEST_INC:
            return {
                ...state,
                counter: state.counter + 1
            };
        case REQUEST_DEC:
            return {
                ...state,
                counter: state.counter - 1
            };
        default:
            return state;
    }
}