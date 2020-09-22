import EventsExpressService from '../../services/EventsExpressService';

export const recoverPassword = {
    PENDING: "SET_RECOVERPASSWORD_PENDING",
    SUCCESS: "SET_RECOVERPASSWORD_SUCCESS",
    ERROR: "SET_RECOVERPASSWORD_ERROR",

}

const api_serv = new EventsExpressService();

export default function recover_Password(data) {
    return dispatch => {
        dispatch(setRecoverPasswordPending(true));
        const res = api_serv.setRecoverPassword(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setRecoverPasswordSuccess(true));
            } else {
                dispatch(setRecoverPasswordError(response.error));
            }
        });
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

function setRecoverPasswordError(data) {
    return {
        type: recoverPassword.ERROR,
        payload: data
    };
}
