import { UserService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_CONTACTUS_PENDING = "SET_CONTACTUS_PENDING";
export const GET_CONTACTUS_SUCCESS = "GET_CONTACTUS_SUCCESS";

const api_serv = new UserService();

export default function getIssues() {
    return async dispatch => {
        dispatch(setContactUsPending(true));
        let response = await api_serv.getAllIssues();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getListOfIssues(jsonRes));
        return Promise.resolve();
    }
}

function setContactUsPending(data) {
    return {
        type: SET_CONTACTUS_PENDING,
        payload: data
    };
}

function getListOfIssues(data) {
    return {
        type: GET_CONTACTUS_SUCCESS,
        payload: data
    }
}

