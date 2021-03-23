import NotificationTypeService from '../../services/NotificationTypeService';
import { setErrorAllertFromResponse } from './../alert-action';

export const SET_NOTIFICATION_TYPES_PENDING = "SET_NOTIFICATION_TYPES_PENDING";
export const GET_NOTIFICATION_TYPES_SUCCESS = "GET_NOTIFICATION_TYPES_SUCCESS";

const api_serv = new NotificationTypeService();

export default function get_notificationTypes() {

    return async dispatch => {
        dispatch(setNotificationTypesPending(true));

        let response = await api_serv.getAllNotificationTypes();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getNotificationTypes(jsonRes));
        return Promise.resolve();
    }
}

function setNotificationTypesPending(data) {
    return {
        type: SET_NOTIFICATION_TYPES_PENDING,
        payload: data
    }
}

function getNotificationTypes(data) {
    return {
        type: GET_NOTIFICATION_TYPES_SUCCESS,
        payload: data
    }
}
