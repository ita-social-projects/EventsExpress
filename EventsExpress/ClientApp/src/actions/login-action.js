import { AuthenticationService } from '../services';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../components/helpers/action-helpers';
import { getUserInfo } from './authentication-action';

export const SET_LOGIN_PENDING = "SET_LOGIN_PENDING";
export const SET_LOGIN_SUCCESS = "SET_LOGIN_SUCCESS";

const api_serv = new AuthenticationService();

export default function login(email, password) {
    const call = () => api_serv.setLogin({
        Email: email,
        Password: password
    });
    return loginResponseHandler(call);
}

export function loginGoogle(tokenId, email, name, imageUrl) {
    const call = () => api_serv.setGoogleLogin({
        TokenId: tokenId,
        Email: email,
        Name: name,
        PhotoUrl: imageUrl
    });
    return loginResponseHandler(call);
}

export function loginFacebook(email, name, picture) {
    const call = () => api_serv.setFacebookLogin({
        Email: email,
        Name: name,
        PhotoUrl: picture
    });
    return loginResponseHandler(call);
}

export function loginTwitter(data) {
    const res = () => api_serv.setTwitterLogin({
        TokenId: data.oauth_token,
        TokenSecret: data.oauth_token_secret,
        UserId: data.user_id,
        Email: data.email,
        Name: typeof data.name !== 'undefined' ? data.name : data.screen_name,
        PhotoUrl: data.image_url,
    });
    return loginResponseHandler(res);
}

const loginResponseHandler = call => {
    return async dispatch => {
        dispatch(setLoginPending(true));
        let response = await call();
        if (!response.ok) {
            localStorage.clear();
            throw new SubmissionError(await buildValidationState(response));
        }
        let jsonRes = await response.json(); 
        localStorage.setItem('token', jsonRes.token);
        dispatch (getUserInfo());
        dispatch(setLoginSuccess(true));
        return Promise.resolve()
    }
}

export function setLoginPending(isLoginPending) {
    return {
        type: SET_LOGIN_PENDING,
        isLoginPending
    };
}

export function setLoginSuccess(isLoginSuccess) {
    return {
        type: SET_LOGIN_SUCCESS,
        isLoginSuccess
    };
}
