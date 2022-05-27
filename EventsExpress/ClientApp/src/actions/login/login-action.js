import { AuthenticationService } from '../../services';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { setErrorAllertFromResponse } from '../alert-action';
import { createBrowserHistory } from 'history';
import filterHelper from '../../components/helpers/filterHelper';
import { updateEventsFilters } from '../event/event-list-action';
import { initialConnection } from '../chat/chat-action';
import { getUnreadMessages } from '../chat/chats-action';
import { jwtStorageKey } from '../../constants/constants';
import { getRequestInc, getRequestDec } from '../request-count-action';
import {toggleLoginModalState} from "../login-modal"

export const SET_LOGIN_PENDING = 'SET_LOGIN_PENDING';
export const SET_LOGIN_SUCCESS = 'SET_LOGIN_SUCCESS';
export const SET_USER = 'SET_USER';

const history = createBrowserHistory({ forceRefresh: true });
const api_serv = new AuthenticationService();

export default function login(email, password) {
    const call = () => {
        return api_serv.setLogin({
            Email: email,
            Password: password,
        });
    };
    return loginResponseHandler(call, { email });
}

export function loginGoogle(tokenId, profile) {
    const call = () => {
        return api_serv.setGoogleLogin({
            TokenId: tokenId,
            Email: profile.email,
            Name: profile.name,
            PhotoUrl: profile.imageUrl,
        });
    };
    
    return loginResponseHandler(call, {
        email: profile.email,
        name: profile.name,
        firstName: profile.givenName,
        lastName: profile.familyName,
        type: 0,
    });
}

export function loginFacebook(profile) {
    const call = () => {
        api_serv.setFacebookLogin({
            Email: profile.email,
            Name: profile.name,
            PhotoUrl: profile.picture.data.url,
        });
    };
    
    return loginResponseHandler(call, {
        email: profile.email,
        name: profile.name,
        firstName: profile.firstName,
        lastName: profile.lastName,
        birthday: profile.birthday,
        gender: profile.gender,
        type: 1,
    });
}

export function loginTwitter(data) {
    const call = () => {
        return api_serv.setTwitterLogin({
            TokenId: data.oauth_token,
            TokenSecret: data.oauth_token_secret,
            UserId: data.user_id,
            Email: data.email,
            Name: data.name || data.screen_name,
            PhotoUrl: data.image_url,
        });
    };
    
    return loginResponseHandler(call, {
        email: data.email,
        name: data.name || data.screen_name,
        type: 2,
    });
}

export function loginAfterEmailConfirmation(data) {
    return async dispatch => {
        let response = await api_serv.auth(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        return setUserInfo(response, null, dispatch);
    };
}

export function getUserInfo(profile) {
    return async dispatch => {
        const response = await api_serv.getUserInfo();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        if (response.status == 204) {
            if (history.location.pathname != '/register/complete') {
                history.push('/register/complete', { profile });
            }
            return Promise.resolve();
        }

        const userInfo = await response.json();
        const eventFilter = {
            ...filterHelper.getDefaultEventFilter(),
            categories: userInfo.categories.map(item => item.id),
        };

        dispatch(setUser(userInfo));
        dispatch(updateEventsFilters(eventFilter));
        localStorage.setItem('id', userInfo.id);
        dispatch(initialConnection());
        dispatch(getUnreadMessages(userInfo.id));
        return Promise.resolve();
    };
}

export function setUser(data) {
    return {
        type: SET_USER,
        payload: data,
    };
}

function loginResponseHandler(call, profile) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await call();
        dispatch(getRequestDec());
        if (!response.ok) {
            localStorage.clear();
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(toggleLoginModalState(false));
        return setUserInfo(response, profile, dispatch);
    };
}

async function setUserInfo(response, profile, dispatch) {
    let jsonRes = await response.json();
    localStorage.setItem(jwtStorageKey, jsonRes.token);

    dispatch(getUserInfo(profile));
    return Promise.resolve();
}
