import { getErrorMessege } from '../components/helpers/action-helpers';

export const _alert = {
    SET: "SET_ALERT",
    SETOPEN: "ALERT_SET_OPEN"
}

export function setAlert(data) {
    return dispatch => {
        dispatch(setAlertInternal(data));
        dispatch(setAlertOpen(true));
    }
}

export function setErrorAllertFromResponse(responsePromise) {
    return async dispatch => {
        const errorText = await getErrorMessege(responsePromise);
        const alert = buildAlertWithError(errorText);
        dispatch(setAlert(alert));
    }
}

export function setSuccessAllert(msg) {
    return dispatch => {
        const alert = buildAlertWithSuccess(msg);
        dispatch(setAlert(alert));
    }
}

export function setErrorAlert(msg) {
    return dispatch => {
        const alert = buildAlertWithError(msg);
        dispatch(setAlert(alert));
    }
}

export function setAlertOpen(data) {
    return {
        type: _alert.SETOPEN,
        payload: data
    }
}

function setAlertInternal(data) {
    return {
        type: _alert.SET,
        payload: data
    }
}

const buildAlertWithError = (msg) => ({
    variant: 'error',
    message: msg,
})

const buildAlertWithSuccess = (msg) => ({
    variant: 'success',
    message: msg,
})