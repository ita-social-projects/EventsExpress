import initialState from '../../store/initialState';
import {
    SET_UNITS_OF_MEASURING_PENDING, GET_UNITS_OF_MEASURING_SUCCESS
} from '../../actions/unitOfMeasuring/unitsOfMeasuring-list-action';
import { SET_UNIT_OF_MEASURING_EDITED } from '../../actions/unitOfMeasuring/unitOfMeasuring-add-action'

export const reducer = (state = initialState.unitsOfMeasuring, action) => {
    switch (action.type) {
        case SET_UNITS_OF_MEASURING_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_UNITS_OF_MEASURING_SUCCESS:
            return {
                ...state,
                isPending: false,
                units: action.payload
            }
        case SET_UNIT_OF_MEASURING_EDITED:
            return {
                ...state,
                editedUnitOfMeasuring: action.payload
            }
        default:
            return state;
    }
}




