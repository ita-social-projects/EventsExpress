import UnitOfMeasuringService from '../../services/unitOfMeasuringService';
import get_unitsOfMeasuring from './unitsOfMeasuring-list-action';
import { setErrorAllertFromResponse } from './../alert-action';

export const SET_UNIT_OF_MEASURING_DELETE_PENDING = "SET_UNIT_OF_MEASURING_DELETE_PENDING";
export const SET_UNIT_OF_MEASURING_DELETE_SUCCESS = "SET_UNIT_OF_MEASURING_DELETE_SUCCESS";

const api_serv = new UnitOfMeasuringService();

export function delete_unitOfMeasuring(data) {

    return async dispatch => {
        dispatch(setUnitOfMeasuringDeletePending(true));

        let response = await api_serv.setUnitOfMeasuringDelete(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setUnitOfMeasuringDeleteSuccess(true));
        dispatch(get_unitsOfMeasuring());
        return Promise.resolve();
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


