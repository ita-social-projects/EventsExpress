import { AuthenticationService } from '../../services';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/helpers.js'

export const recoverPassword = {
    PENDING: "SET_RECOVERPASSWORD_PENDING",
    SUCCESS: "SET_RECOVERPASSWORD_SUCCESS",
}

const api_serv = new AuthenticationService();

export default function recover_Password(data) {
    return async dispatch => {
        dispatch(setRecoverPasswordPending(true));
        let response = await api_serv.setRecoverPassword(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setRecoverPasswordSuccess(true));
        return Promise.resolve();
    }
}

function setRecoverPasswordPending(data) {
    return {
        type: recoverPassword.PENDING,
        payload: data
    };
}

function setRecoverPasswordSuccess(data) {
    return {
        type: recoverPassword.SUCCESS,
        payload: data
    };
}

