import initialState from '../store/initialState';

import { GET_UNITS_OF_MEASURING } from '../actions/unitsOfMeasuring';

export const reducer = (state = initialState.unitsOfMeasuring, action) => {
    if (action.type === GET_UNITS_OF_MEASURING) {
        return {
            ...state,
            units: action.payload
        };
    }
    return state;
}