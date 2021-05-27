import { ContactAdminService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_CONTACT_ADMIN_PENDING = "SET_CONTACT_ADMIN_PENDING";
export const GET_CONTACT_ADMIN_SUCCESS = "GET_CONTACT_ADMIN_SUCCESS";

const api_serv = new ContactAdminService();

export default function get_message_by_id(id) {
    return async dispatch => {
        dispatch(setContactAdminPending(true));

        let response = await api_serv.getMessage(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getMessageById(jsonRes));
        dispatch(setContactAdminPending(true));
        return Promise.resolve();
    }
}


function setContactAdminPending(data) {
    return {
        type: SET_CONTACT_ADMIN_PENDING,
        payload: data
    };
}

function getMessageById(data) {
    return {
        type: GET_CONTACT_ADMIN_SUCCESS,
        payload: data
    }
}
