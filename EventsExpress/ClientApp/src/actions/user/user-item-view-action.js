import { UserService } from '../../services';
import { getRequestInc, getRequestDec } from "../request-count-action";
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_PROFILE_DATA = "GET_PROFILE_DATA";
export const RESET_USER = "RESET_USER";

const api_serv = new UserService();

export default function get_user(id) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.getUserById(id);
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getProfile(jsonRes));
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
    }
}

function getProfile(data) {
    return {
        type: GET_PROFILE_DATA,
        payload: data
    }
}

export function reset_user() {
    return {
        type: RESET_USER
    }
}