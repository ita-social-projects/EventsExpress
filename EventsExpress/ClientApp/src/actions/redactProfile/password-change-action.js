import { AuthenticationService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";


export const changePassword = {
    UPDATE: "UPDATE_PASSWORD"
}

const api_serv = new AuthenticationService();

export default function change_Password(data) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.setChangePassword(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(setSuccessAllert('Password was succesfully changed'));
        dispatch(reset('ChangePassword'));
        return Promise.resolve();
    }
}

export function changePasswordUpdate(data) {
    return {
        type: changePassword.UPDATE,
        payload: data
    };
}