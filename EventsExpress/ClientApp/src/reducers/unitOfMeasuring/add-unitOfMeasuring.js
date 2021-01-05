

// import initialState from '../../store/initialState';

// import {
//     SET_UNIT_OF_MEASURING_ERROR, SET_UNIT_OF_MEASURING_PENDING, SET_UNIT_OF_MEASURING_SUCCESS
// } from '../../actions/unitOfMeasuring/add-unitOfMeasuring';

// export const reducer = (state = initialState.unitsOfMeasuring, action) => {

//     switch (action.type) {
        

//         case SET_UNIT_OF_MEASURING_ERROR:
//             return {
//                 ...state,
//                 isAdded:false,
//                 isError:action.payload
//             };
//         case SET_UNIT_OF_MEASURING_PENDING:
//             return {
//                 ...state,
//                 isAdded:false,
//                 isPending:action.payload
//             };
//         case SET_UNIT_OF_MEASURING_SUCCESS:
//             return {
//                 ...state,
//                 isPending: false,
//                 isAdded:true,
//                 units: [...state.units, action.payload]
//             };
//         default:
//             break;
//     }
//     return state;
// };

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

  