import { AuthenticationService } from '../../services';
import { reset } from 'redux-form';
import { setAlert } from '../alert';

export const changePassword = {
    PENDING: "SET_CHANGEPASSWORD_PENDING",
    SUCCESS: "SET_CHANGEPASSWORD_SUCCESS",
    ERROR: "SET_CHANGEPASSWORD_ERROR",
    UPDATE: "UPDATE_PASSWORD"
}

const api_serv = new AuthenticationService();

export default function change_Password(data) {
    return dispatch => {
        dispatch(setChangePasswordPending(true));

        const res = api_serv.setChangePassword(data);

        res.then(response => {
            if (response.error == null) {
                dispatch(setChangePasswordSuccess(true));
                dispatch(setAlert({
                    variant: 'success',
                    message: 'Password was succesfully changed'
                }));
                dispatch(reset('ChangePassword'));
            } else {
                dispatch(setChangePasswordError(response.error));
                dispatch(setAlert({ variant: 'error', message: 'Failed' }));
            }
        });
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

export function setChangePasswordError(data) {
    return {
        type: changePassword.ERROR,
        payload: data
    };
}
