import { ContactUsService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_CONTACTUS_PENDING = "SET_CONTACTUS_PENDING";
export const GET_CONTACTUS_SUCCESS = "GET_CONTACTUS_SUCCESS";

const api_serv = new ContactUsService();

export default function get_message_by_id(id) {
    return async dispatch => {
        dispatch(setContactUsPending(true));

        let response = await api_serv.getMessage(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getMessageById(jsonRes));
        return Promise.resolve();
    }
}


function setContactUsPending(data) {
    return {
        type: SET_CONTACTUS_PENDING,
        payload: data
    };
}

function getMessageById(data) {
    return {
        type: GET_CONTACTUS_SUCCESS,
        payload: data
    }
}
