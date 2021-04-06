import { AuthenticationService } from '../services';
import { getErrorMessege } from '../components/helpers/action-helpers';

export const authenticate = {
    PENDING: "SET_AUTHENTICATE_PENDING",
    SUCCESS: "SET_AUTHENTICATE_SUCCESS",
    SET_AUTHENTICATE: "SET_AUTHENTICATE",
}

const api_serv = new AuthenticationService();

export default function _authenticate(data) {
    return async dispatch => {
        dispatch(setAuthenticatePending(true));
        let response = await api_serv.auth(data);
        if (!response.ok) {
            dispatch(getErrorMessege(responce));
            return Promise.reject();
        }
        dispatch(setAuthenticate(responce));
        return Promise.resolve();
    }
}

export function setAuthenticate(data) {
    return {
        type: authenticate.SET_AUTHENTICATE,
        payload: data
    }
}

export function setAuthenticatePending(isAuthenticatePending) {
    return {
        type: authenticate.PENDING,
        isAuthenticatePending
    }
}

export function setAuthenticateSuccess(isAuthenticateSuccess) {
    return {
        type: authenticate.SUCCESS,
        isAuthenticateSuccess
    }
}
