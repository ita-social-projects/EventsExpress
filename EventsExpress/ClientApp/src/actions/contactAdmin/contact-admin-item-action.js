import { ContactAdminService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CONTACT_ADMIN_DATA = "GET_CONTACT_ADMIN_DATA";

const api_serv = new ContactAdminService();

export default function get_message_by_id(id) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.getMessage(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getMessageById(jsonRes));
        return Promise.resolve();
    }
}

function getMessageById(data) {
    return {
        type: GET_CONTACT_ADMIN_DATA,
        payload: data
    }
}
