import { AuthenticationService } from '../../services';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";


export const recoverPassword = {
    DATA: "SET_RECOVERPASSWORD_SUCCESS",
}

const api_serv = new AuthenticationService();

export default function recover_Password(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setRecoverPassword(data);
        dispatch(getRequestDec());
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setRecoverPasswordSucces());
        return Promise.resolve();
    }
}

const setRecoverPasswordSucces = () => {
    return {
        type: recoverPassword.DATA,
        payload: data
    }
}
