import UnitOfMeasuringService from '../../services/UnitOfMeasuringService';
import get_unitsOfMeasuring from './unitsOfMeasuring-list';

export const SET_UNIT_OF_MEASURING_DELETE_PENDING = "SET_UNIT_OF_MEASURING_DELETE_PENDING";
export const SET_UNIT_OF_MEASURING_DELETE_SUCCESS = "SET_UNIT_OF_MEASURING_DELETE_SUCCESS";
export const SET_UNIT_OF_MEASURING_DELETE_ERROR = "SET_UNIT_OF_MEASURING_DELETE_ERROR";

const api_serv = new UnitOfMeasuringService();

export function delete_unitOfMeasuring(data) {

    return dispatch => {
        dispatch(setUnitOfMeasuringDeletePending(true));

        const res = api_serv.setUnitOfMeasuringDelete(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setUnitOfMeasuringDeleteSuccess(true));
                dispatch(get_unitsOfMeasuring());
            } else {
                dispatch(setUnitOfMeasuringDeleteError(response.error));
            }
        });
    }
}


function setUnitOfMeasuringDeleteSuccess(data) {
    return {
        type: SET_UNIT_OF_MEASURING_DELETE_SUCCESS,
        payload: data
    };
}

function setUnitOfMeasuringDeletePending(data) {
    return {
        type: SET_UNIT_OF_MEASURING_DELETE_PENDING,
        payload: data
    };
}

export function setUnitOfMeasuringDeleteError(data) {
    return {
        type: SET_UNIT_OF_MEASURING_DELETE_ERROR,
        payload: data
    };
}

