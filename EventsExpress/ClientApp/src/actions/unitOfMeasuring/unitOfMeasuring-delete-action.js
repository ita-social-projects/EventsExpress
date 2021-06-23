import UnitOfMeasuringService from '../../services/unitOfMeasuringService';
import get_unitsOfMeasuring from './unitsOfMeasuring-list-action';
import { setErrorAllertFromResponse } from './../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

const api_serv = new UnitOfMeasuringService();

export function delete_unitOfMeasuring(data) {

    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.setUnitOfMeasuringDelete(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(getRequestDec());
        dispatch(get_unitsOfMeasuring());
        return Promise.resolve();
    }
}

