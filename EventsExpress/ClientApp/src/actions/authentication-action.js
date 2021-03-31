import { AuthenticationService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';
import { createBrowserHistory } from 'history';
import eventHelper from '../components/helpers/eventHelper';
import { updateEventsFilters } from './event-list-action';
import { initialConnection } from './chat';
import { getUnreadMessages } from './chats';

export const authenticate = {
    PENDING: "SET_AUTHENTICATE_PENDING",
    SUCCESS: "SET_AUTHENTICATE_SUCCESS",
    SET_AUTHENTICATE: "SET_AUTHENTICATE",
    
}
export const SET_USER = "SET_USER";


const history = createBrowserHistory({ forceRefresh: true });
const api_serv = new AuthenticationService();

export default function _authenticate(data) {
    return async dispatch => {
        dispatch(setAuthenticatePending(true));
        let response = await api_serv.auth(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setAuthenticate(response));
        return Promise.resolve();
    }
}

export function getUserInfo(){
    return async dispatch => {
        let response = await api_serv.getUserInfo();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        if (response.status == 204 && history.location.pathname != '/registerComplete'){
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

export function setUser(data) {
    return {
        type: SET_USER,
        payload: data
    };
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
