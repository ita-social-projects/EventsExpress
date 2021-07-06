
import UnitOfMeasuringService from '../../services/unitOfMeasuringService';
import { setErrorAllertFromResponse } from './../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_UNITS_OF_MEASURING_DATA = "GET_UNITS_OF_MEASURING_SUCCESS";

const api_serv = new UnitOfMeasuringService();

export default function get_unitsOfMeasuring() {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.getUnitsOfMeasuring();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getUnitsOfMeasuring(jsonRes));
        dispatch(getRequestDec());
        return Promise.resolve();
    }
}

function getUnitsOfMeasuring(data) {
    return {
        type: GET_UNITS_OF_MEASURING_DATA,
        payload: data
    }
}
