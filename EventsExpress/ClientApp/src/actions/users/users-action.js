import { UserService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import * as SignalR from '@aspnet/signalr';
import { jwtStorageKey } from "../../constants/constants";

export const GET_USERS_PENDING = "GET_USERS_PENDING";
export const GET_USERS_SUCCESS = "GET_USERS_SUCCESS";
export const RESET_USERS = "RESET_USERS";
export const CHANGE_USERS_FILTER = "CHANGE_USERS_FILTER";
export const GET_USERS_COUNT = "GET_USERS_COUNT";

const hubConnection = new SignalR.HubConnectionBuilder().withUrl(`${window.location.origin}/usersHub`,
    { accessTokenFactory: () => (localStorage.getItem(jwtStorageKey)) }).build();
const api_serv = new UserService();

export function initialConnection() {
    return async dispatch => {
        await hubConnection.start();
        
        try {
            hubConnection.on("CountUsers", (numberOfUsers) => {
                dispatch(getCount(numberOfUsers));
                return Promise.resolve();
            });
        } catch(err) {
            console.error(err.toString());
            return Promise.reject();
        }
    }
}

export async function closeConnection() {
    await hubConnection.stop();
}

export function get_count() {
    return async dispatch => {
        const response = await api_serv.getCount();
        if(!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        const jsonRes = await response.json();
        dispatch(getCount(jsonRes));
        return Promise.resolve();
    }
}

export function get_users(filters) {
    return async dispatch => {
        dispatch(getUsersPending(true));
        let response = await api_serv.getUsers(filters);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getUsers(jsonRes));
        return Promise.resolve();
    }
}

export function change_Filter(filters) {
    return dispatch => {
        dispatch(changeFilters(filters));
    }
}

export function get_SearchUsers(filters) {
    return async dispatch => {
        dispatch(getUsersPending(true));
        let response = await api_serv.getSearchUsers(filters);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getUsers(jsonRes));
        return Promise.resolve();
    }
}

function getUsersPending(data) {
    return {
        type: GET_USERS_PENDING,
        payload: data
    }
}

function getCount(data) {
    return {
        type: GET_USERS_COUNT,
        payload: data
    }
}

function getUsers(data) {
    return {
        type: GET_USERS_SUCCESS,
        payload: data
    }
}

function changeFilters(data) {
    return {
        type: CHANGE_USERS_FILTER,
        payload: data
    }
}

export function reset_users() {
    return {
        type: RESET_USERS
    }
}
