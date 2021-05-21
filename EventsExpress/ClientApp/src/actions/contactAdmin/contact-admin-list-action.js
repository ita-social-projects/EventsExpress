import { ContactAdminService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_CONTACT_ADMIN_PENDING = "SET_CONTACT_ADMIN_PENDING";
export const GET_CONTACT_ADMIN_SUCCESS = "GET_CONTACT_ADMIN_SUCCESS";
export const RESET_CONTACT_ADMIN = "RESET_CONTACT_ADMIN";

const api_serv = new ContactAdminService();

export default function getIssues(filters) {
    return async dispatch => {
        dispatch(setContactAdminPending(true));
        let response = await api_serv.getAllIssues(filters);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getListOfIssues(jsonRes));
        return Promise.resolve();

    }
}

export function setContactAdminPending(data) {
    return {
        type: SET_CONTACT_ADMIN_PENDING,
        payload: data
    };
}

export function getListOfIssues(data) {
    return {
        type: GET_CONTACT_ADMIN_SUCCESS,
        payload: data
    }
}

export function resetFilters() {
    return {
        type: RESET_CONTACT_ADMIN
    }
}


