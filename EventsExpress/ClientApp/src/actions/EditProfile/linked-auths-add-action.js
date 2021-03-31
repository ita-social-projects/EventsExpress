import { AccountService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import getLinkedAuths from './linked-auths-action';

const api_serv = new AccountService();

export function localLoginAdd(email, password) {
    const call = () => api_serv.setLocalLoginAdd({
        Email: email,
        Password: password
    });
    return loginResponseHandler(call);
}

export function googleLoginAdd(tokenId, email) {
    const call = () => api_serv.setGoogleLoginAdd({
        TokenId: tokenId,
        Email: email
    });
    return loginResponseHandler(call);
}

export function facebookLoginAdd(email) {
    const call = () => api_serv.setFacebookLoginAdd({
        Email: email
    });
    return loginResponseHandler(call);
}

export function twitterLoginAdd(email) {
    const res = () => api_serv.setTwitterLoginAdd({
        Email: email
    });
    return loginResponseHandler(res);
}

const loginResponseHandler = call => {
    return async dispatch => {
        let response = await call();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(getLinkedAuths());
        return Promise.resolve();
    }
}
