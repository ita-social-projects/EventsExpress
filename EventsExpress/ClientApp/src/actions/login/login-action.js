import { AuthenticationService } from '../../services';
import eventHelper from '../../components/helpers/eventHelper';
import { initialConnection } from '../chat/chat';
import { getUnreadMessages } from '../chat/chats';
import { updateEventsFilters } from '../event/event-list-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const SET_LOGIN_PENDING = "SET_LOGIN_PENDING";
export const SET_LOGIN_SUCCESS = "SET_LOGIN_SUCCESS";
export const SET_USER = "SET_USER";

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
        const eventFilter = {
            ...eventHelper.getDefaultEventFilter(),
            categories: jsonRes.categories.map(item => item.id),
        };

        dispatch(setUser(jsonRes));
        dispatch(updateEventsFilters(eventFilter));
        dispatch(setLoginSuccess(true));

        localStorage.setItem('token', jsonRes.token);
        localStorage.setItem('id', jsonRes.id);

        dispatch(initialConnection());
        dispatch(getUnreadMessages(jsonRes.id));
        return Promise.resolve()
    }
}

export function setUser(data) {
    return {
        type: SET_USER,
        payload: data
    };
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
