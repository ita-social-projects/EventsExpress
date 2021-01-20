import initialState from '../../store/initialState';
import {
    SET_UNIT_OF_MEASURING_ERROR, SET_UNIT_OF_MEASURING_PENDING, SET_UNIT_OF_MEASURING_SUCCESS
} from '../../actions/unitOfMeasuring/add-unitOfMeasuring';

export const reducer = (state = initialState.add_unitOfMeasuring, action) => {

    switch (action.type) {        

        case SET_UNIT_OF_MEASURING_ERROR:
            return {
                ...state,
                unitOfMeasuringError:action.payload
            };
        case SET_UNIT_OF_MEASURING_PENDING:
            return {
                ...state,
                unitOfMeasuringError:null,
                isUnitOfMeasuringPending:action.payload
            };
        case SET_UNIT_OF_MEASURING_SUCCESS:
            return {
                ...state,
                unitOfMeasuringError:null,
                isUnitOfMeasuringSuccess:action.payload
            };
        default:
            break;
    }
    return state;
};

  