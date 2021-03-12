import UnitOfMeasuringService from '../../services/unitOfMeasuringService';
import get_unitsOfMeasuring from './unitsOfMeasuring-list-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const SET_UNIT_OF_MEASURING_PENDING = "SET_UNIT_OF_MEASURING_PENDING";
export const SET_UNIT_OF_MEASURING_SUCCESS = "SET_UNIT_OF_MEASURING_SUCCESS";
export const SET_UNIT_OF_MEASURING_EDITED = "SET_UNIT_OF_MEASURING_EDITED";

const api_serv = new UnitOfMeasuringService();

export function add_unitOfMeasuring(data) {
    return async dispatch => {
        dispatch(setUnitOfMeasuringPending(true));
        let response;
        if (!!(data.id)) {
            response = await api_serv.editUnitOfMeasuring(data)
        } else {
            response = await api_serv.setUnitOfMeasuring(data)
        }
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setUnitOfMeasuringSuccess(true));
        dispatch(get_unitsOfMeasuring());
        return Promise.resolve();
    }
}

export function set_edited_unitOfMeasuring(id) {
    return dispatch => {
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


