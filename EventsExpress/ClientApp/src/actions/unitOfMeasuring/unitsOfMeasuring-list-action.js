
import UnitOfMeasuringService from '../../services/unitOfMeasuringService';
import { setErrorAllertFromResponse } from './../alert-action';

export const SET_UNITS_OF_MEASURING_PENDING = "SET_UNITS_OF_MEASURING_PENDING";
export const GET_UNITS_OF_MEASURING_SUCCESS = "GET_UNITS_OF_MEASURING_SUCCESS";

const api_serv = new UnitOfMeasuringService();

export default function get_unitsOfMeasuring() {
    return async dispatch => {
        dispatch(setUnitOfMeasuringPending(true));

        let response = await api_serv.getUnitsOfMeasuring();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getUnitsOfMeasuring(jsonRes));
        return Promise.resolve();
    }
}

function setUnitOfMeasuringPending(data) {
    return {
        type: SET_UNITS_OF_MEASURING_PENDING,
        payload: data
    }
}

function getUnitsOfMeasuring(data) {
    return {
        type: GET_UNITS_OF_MEASURING_SUCCESS,
        payload: data
    }
}
