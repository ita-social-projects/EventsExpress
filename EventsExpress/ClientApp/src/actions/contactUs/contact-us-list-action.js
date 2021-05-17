import { ContactUsService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_CONTACTUS_PENDING = "SET_CONTACTUS_PENDING";
export const GET_CONTACTUS_SUCCESS = "GET_CONTACTUS_SUCCESS";
export const RESET_CONTACTUS = "RESET_CONTACTUS";

const api_serv = new ContactUsService();

export default function getIssues(filters) {
    return async dispatch => {
        dispatch(setContactUsPending(true));
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

export function setContactUsPending(data) {
    return {
        type: SET_CONTACTUS_PENDING,
        payload: data
    };
}

export function getListOfIssues(data) {
    return {
        type: GET_CONTACTUS_SUCCESS,
        payload: data
    }
}

export function resetFilters() {
    return {
        type: RESET_CONTACTUS
    }
}


