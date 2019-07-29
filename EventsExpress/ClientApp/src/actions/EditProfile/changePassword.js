import EventsExpressService from '../../services/EventsExpressService';

export const changePassword = {
    PENDING: "SET_CHANGEPASSWORD_PENDING",
    SUCCESS: "SET_CHANGEPASSWORD_SUCCESS",
    ERROR: "SET_CHANGEPASSWORD_ERROR",
    UPDATE: "UPDATE_PASSWORD"
}

const api_serv = new EventsExpressService();

export default function change_Password(data) {
    return dispatch => {
        dispatch(setChangePasswordPending(true));
        const res = api_serv.setChangePassword(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setChangePasswordSuccess(true));
                dispatch(changePasswordUpdate(data));
            } else {
                dispatch(setChangePasswordError(response.error));
            }
        });

    }
}


function changePasswordUpdate(data) {
    return {
        type: changePassword.UPDATE,
        payload: data
    };
}

function setChangePasswordPending(data) {
    return {
        type: changePassword.PENDING,
        payload: data
    };
}

function setChangePasswordSuccess(data) {
    return {
        type: changePassword.SUCCESS,
        payload: data
    };
}

function setChangePasswordError(data) {
    return {
        type: changePassword.ERROR,
        payload: data
    };
}

