import { UserService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import * as SignalR from '@aspnet/signalr';
import { jwtStorageKey } from "../../constants/constants";

export const GET_USERS_PENDING = "GET_USERS_PENDING";
export const GET_USERS_SUCCESS = "GET_USERS_SUCCESS";
export const RESET_USERS = "RESET_USERS";
export const CHANGE_USERS_FILTER = "CHANGE_USERS_FILTER";
export const GET_USERS_COUNT = "GET_USERS_COUNT";
export const GET_BLOCKED_USERS_COUNT = "GET_BLOCKED_USERS_COUNT";
export const GET_UNBLOCKED_USERS_COUNT = "GET_UNBLOCKED_USERS_COUNT";
export const CHANGE_STATUS = "CHANGE_STATUS";
export const accountStatus = {
    All: 0,
    Activated: 1,
    Blocked: 2
}

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
            hubConnection.on("CountBlockedUsers", (numberOfUsers) => {
                dispatch(getBlockedCount(numberOfUsers));
                return Promise.resolve();
            });
            hubConnection.on("CountUnblockedUsers", (numberOfUsers) => {
                dispatch(getUnblockedCount(numberOfUsers));
                return Promise.resolve();
            });
        } catch(err) {
            console.error(err.toString());
            return Promise.reject();
        }
    }
}

export function closeConnection() {
    return async () => {
        await hubConnection.stop();
    }
}

export function get_count(accountStatus) {
    return async dispatch => {
        const response = await api_serv.getCount(accountStatus);

        if(!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        const jsonRes = await response.json();

        switch (accountStatus) {
            case this.accountStatus.Blocked:
                dispatch(getBlockedCount(jsonRes));
                break;
            case this.accountStatus.Activated:
                dispatch(getUnblockedCount(jsonRes));
                break;
            default:
                dispatch(getCount(jsonRes));
        }

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

export function change_status(status) {
    return dispatch => {
        dispatch(changeStatus(status));
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

function getBlockedCount(data) {
    return {
        type: GET_BLOCKED_USERS_COUNT,
        payload: data
    }
}

function getUnblockedCount(data) {
    return {
        type: GET_UNBLOCKED_USERS_COUNT,
        payload: data
    }
}

function getUsers(data) {
    return {
        type: GET_USERS_SUCCESS,
        payload: data
    }
}

function changeStatus(data) {
    return {
        type: CHANGE_STATUS,
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
