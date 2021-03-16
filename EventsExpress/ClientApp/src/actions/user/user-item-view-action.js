import { UserService } from '../../services';
import { get_future_events } from '../events/events-for-profile-action';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_PROFILE_PENDING = "GET_PROFILE_PENDING";
export const GET_PROFILE_SUCCESS = "GET_PROFILE_SUCCESS";
export const RESET_USER = "RESET_USER";

const api_serv = new UserService();

export default function get_user(id) {
    return async dispatch => {
        dispatch(getProfilePending(true));

        let response = await api_serv.getUserById(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(getProfile(response));
        dispatch(get_future_events(id));
        return Promise.resolve();
    }
}

export function setAttitude(data) {
    return async dispatch => {
        let response = await api_serv.setAttitude(data);
        if (response.ok) {
            let res = await api_serv.getUserById(data.userToId);
            if (!res.ok) {
                dispatch(setErrorAllertFromResponse(response));
                return Promise.reject();
            }
            let jsonRes = await res.json();
            dispatch(getProfile(jsonRes))
            return Promise.reject();
        }
        //res.then(response => {
        //    if (response.error == null) {
        //        const res1 = api_serv.getUserById(data.userToId);
        //        res1.then(response => {
        //            if (response.error == null) {
        //                dispatch(getProfile(response));
        //            } else {
        //                dispatch(getProfileError(response.error));
        //            }
        //        });
        //    }
    }
}

function getProfilePending(data) {
    return {
        type: GET_PROFILE_PENDING,
        payload: data
    }
}

function getProfile(data) {
    return {
        type: GET_PROFILE_SUCCESS,
        payload: data
    }
}

export function reset_user() {
    return {
        type: RESET_USER
    }
}
