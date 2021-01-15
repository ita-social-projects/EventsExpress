import UnitOfMeasuringService from '../../services/UnitOfMeasuringService';
import get_unitsOfMeasuring from './unitsOfMeasuring-list';


export const SET_UNIT_OF_MEASURING_PENDING = "SET_UNIT_OF_MEASURING_PENDING";
export const SET_UNIT_OF_MEASURING_SUCCESS = "SET_UNIT_OF_MEASURING_SUCCESS";
export const SET_UNIT_OF_MEASURING_ERROR = "SET_UNIT_OF_MEASURING_ERROR";
export const SET_UNIT_OF_MEASURING_EDITED = "SET_UNIT_OF_MEASURING_EDITED";

const api_serv = new UnitOfMeasuringService();


export function add_unitOfMeasuring(data) {
    return dispatch => {
        dispatch(setUnitOfMeasuringPending(true));

        let res;
        if  (data.id) {
            res = api_serv.editUnitOfMeasuring(data);
        } else {
            res = api_serv.setUnitOfMeasuring(data);
        }        
        res.then(response => {
            if (response.error == null) {
                dispatch(setUnitOfMeasuringSuccess(true));
                dispatch(get_unitsOfMeasuring());
            } else {
                dispatch(setUnitOfMeasuringError(response.error));
            }
        });
    }
}


export function set_edited_unitOfMeasuring(id) {
   
    return dispatch => {
        dispatch(setUnitOfMeasuringError(null));
        dispatch(setUnitOfMeasuringEdited(id));     
    }
}

function setUnitOfMeasuringEdited(data) {
    return {
        type: SET_UNIT_OF_MEASURING_EDITED,
        payload: data
    };
}

export function setUnitOfMeasuringPending(data) {
    return {
        type: SET_UNIT_OF_MEASURING_PENDING,
        payload: data
    };
}

export function setUnitOfMeasuringSuccess(data) {
    return {
        type: SET_UNIT_OF_MEASURING_SUCCESS,
        payload: data
    };
}

export function setUnitOfMeasuringError(data) {
    return {
        type: SET_UNIT_OF_MEASURING_ERROR,
        payload: data
    };
}

