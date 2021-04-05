import { AuthenticationService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const changePassword = {
    PENDING: "SET_CHANGEPASSWORD_PENDING",
    SUCCESS: "SET_CHANGEPASSWORD_SUCCESS",
    UPDATE: "UPDATE_PASSWORD"
}

const api_serv = new AuthenticationService();

export default function change_Password(data) {
    return async dispatch => {
        dispatch(setChangePasswordPending(true));

        let response = await api_serv.setChangePassword(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setChangePasswordSuccess(true));
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

export function setChangePasswordPending(data) {
    return {
        type: changePassword.PENDING,
        payload: data
    };
}

export function setChangePasswordSuccess(data) {
    return {
        type: changePassword.SUCCESS,
        payload: data
    };
}
