import UnitOfMeasuringService from '../../services/unitOfMeasuringService';
import get_unitsOfMeasuring from './unitsOfMeasuring-list-action';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const SET_UNIT_OF_MEASURING_EDITED = "SET_UNIT_OF_MEASURING_EDITED";

const api_serv = new UnitOfMeasuringService();

export function add_unitOfMeasuring(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response;
        if (!!(data.id)) {
            response = await api_serv.editUnitOfMeasuring(data)
        } else {
            response = await api_serv.setUnitOfMeasuring(data)
        }
        dispatch(getRequestDec());
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(reset('add-form'));
        dispatch(setUnitOfMeasuringEdited(null));
        dispatch(get_unitsOfMeasuring());
        return Promise.resolve();
    }
}

export function setUnitOfMeasuringEdited(id) {
    return {
        type: SET_UNIT_OF_MEASURING_EDITED,
        payload: id
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


