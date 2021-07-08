import NotificationTypeService from '../../services/NotificationTypeService';
import { setErrorAllertFromResponse } from './../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_NOTIFICATION_TYPES_DATA = "GET_NOTIFICATION_TYPES_DATA";
export const GET_USER_NOTIFICATION_TYPES_DATA = "GET_USER_NOTIFICATION_TYPES_DATA";

const api_serv = new NotificationTypeService();

export default function get_notificationTypes() {

    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.getAllNotificationTypes();
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getNotificationTypes(jsonRes));
        return Promise.resolve();
    }
}

function getNotificationTypes(data) {
    return {
        type: GET_NOTIFICATION_TYPES_DATA,
        payload: data
    }
}
