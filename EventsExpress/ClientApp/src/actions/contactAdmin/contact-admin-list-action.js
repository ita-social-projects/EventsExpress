import { ContactAdminService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CONTACT_ADMIN_DATA = "GET_CONTACT_ADMIN_DATA";
export const RESET_CONTACT_ADMIN = "RESET_CONTACT_ADMIN";

const api_serv = new ContactAdminService();

export default function getIssues(filters) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getAllIssues(filters);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getListOfIssues(jsonRes));
        return Promise.resolve();

    }
}

export function getListOfIssues(data) {
    return {
        type: GET_CONTACT_ADMIN_DATA,
        payload: data
    }
}

export function resetFilters() {
    return {
        type: RESET_CONTACT_ADMIN
    }
}


