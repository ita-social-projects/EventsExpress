import { UserService } from '../services';

export const GET_USERS_PENDING = "GET_USERS_PENDING";
export const GET_USERS_SUCCESS = "GET_USERS_SUCCESS";
export const GET_USERS_ERROR = "GET_USERS_ERROR";
export const RESET_USERS = "RESET_USERS";
export const CHANGE_USERS_FILTER = "CHANGE_USERS_FILTER";

const api_serv = new UserService();

export function get_users(filters) {
    return dispatch => {
        dispatch(getUsersPending(true));
        dispatch(getUsersError(false));
        const res = api_serv.getUsers(filters);
        res.then(response => {
            if (response.error == null) {
                dispatch(getUsers(response));

            } else {
                dispatch(getUsersError(response.error));
            }
        });
    }
}

export function change_Filter(filters) {
    return dispatch => {
        dispatch(changeFilters(filters));
    }
}

export function get_SearchUsers(filters) {
    return dispatch => {
        dispatch(getUsersPending(true));
        dispatch(getUsersError(false));
        const res1 = api_serv.getSearchUsers(filters);
        res1.then(response => {
            if (response.error == null) {
                dispatch(getUsers(response));

            } else {
                dispatch(getUsersError(response.error));
            }
        });
    }
}

function getUsersPending(data) {
    return {
        type: GET_USERS_PENDING,
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

export function getUsersError(data) {
    return {
        type: GET_USERS_ERROR,
        payload: data
    }
}

export function reset_users() {
    return {
        type: RESET_USERS
    }
}
