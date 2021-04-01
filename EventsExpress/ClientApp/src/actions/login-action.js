import { AuthenticationService } from '../services';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../components/helpers/action-helpers';
import { setErrorAllertFromResponse } from './alert-action';
import { createBrowserHistory } from 'history';
import eventHelper from '../components/helpers/eventHelper';
import { updateEventsFilters } from './event-list-action';
import { initialConnection } from './chat';
import { getUnreadMessages } from './chats';

export const SET_LOGIN_PENDING = "SET_LOGIN_PENDING";
export const SET_LOGIN_SUCCESS = "SET_LOGIN_SUCCESS";
export const SET_USER = "SET_USER";

const history = createBrowserHistory({ forceRefresh: true });
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

export function loginAfterEmailConfirmation(data) {
    return async dispatch => {
        let response = await api_serv.auth(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        return await setUserInfo(response, dispatch);
    }
}

export function getUserInfo() {
    return async dispatch => {
        let response = await api_serv.getUserInfo();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        if (response.status == 204 && history.location.pathname != '/registerComplete') {
            history.push('/registerComplete')
            return Promise.resolve();
        }

        let userInfo = await response.json();
        const eventFilter = {
            ...eventHelper.getDefaultEventFilter(),
            categories: userInfo.categories.map(item => item.id),
        };
        dispatch(setUser(userInfo));
        dispatch(updateEventsFilters(eventFilter));
        localStorage.setItem('id', userInfo.id);
        dispatch(initialConnection());
        dispatch(getUnreadMessages(userInfo.id));

        return Promise.resolve();
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

export function setUser(data) {
    return {
        type: SET_USER,
        payload: data
    };
}

function loginResponseHandler(call) {
    return async dispatch => {
        dispatch(setLoginPending(true));
        let response = await call();
        if (!response.ok) {
            localStorage.clear();
            throw new SubmissionError(await buildValidationState(response));
        }
        return await setUserInfo(response, dispatch);
    }
}

async function setUserInfo(response, dispatch) {
    let jsonRes = await response.json();
    localStorage.setItem('token', jsonRes.token);
    dispatch(getUserInfo());
    dispatch(setLoginSuccess(true));
    return Promise.resolve();
}
