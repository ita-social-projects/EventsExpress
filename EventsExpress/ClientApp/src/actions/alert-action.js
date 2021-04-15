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
        const alert = buildAllertWithError(errorText);
        dispatch(setAlert(alert));
    }
}

export function setSuccessAllert(msg) {
    return dispatch => {
        const alert = buildAllertWithSuccess(msg);
        dispatch(setAlert(alert));
    }
}

export function setErrorAlert(msg) {
    return dispatch => {
        const alert = buildAllertWithError(msg);
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

const buildAllertWithError = (msg) => ({
    variant: 'error',
    message: msg,
})

const buildAllertWithSuccess = (msg) => ({
    variant: 'success',
    message: msg,
})