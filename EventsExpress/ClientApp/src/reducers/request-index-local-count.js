import initialState from '../store/initialState';
import { REQUEST_LOCAL_INC, REQUEST_LOCAL_DEC } from "../actions/request-local-count-action"

export const reducer = (state = initialState.requestLocalCount, action) => {
    switch (action.type) {
        case REQUEST_LOCAL_INC:
            return {
                ...state,
                localCounter: state.localCounter + 1
            };
        case REQUEST_LOCAL_DEC:
            return {
                ...state,
                localCounter: state.localCounter - 1
            };
        default:
            return state;
    }
}