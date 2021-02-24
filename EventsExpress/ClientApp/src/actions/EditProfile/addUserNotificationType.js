import { UserService } from '../../services';
import { setAlert } from '../alert-action';

export const addUserNotificationType = {
    PENDING: "SET_ADD_USER_NOTIFICATION_TYPE_PENDING",
    SUCCESS: "SET_ADD_USER_NOTIFICATION_TYPE_SUCCESS",
    ERROR: "SET_ADD_USER_NOTIFICATION_TYPE_ERROR",
    UPDATE: "UPDATE_NOTIFICATION_TYPES",
}

const api_serv = new UserService();

export default function setUserNotificationTypes(data) {
    return dispatch => {
        dispatch(setAddUserNotificationTypePending(true));
        const res = api_serv.setUserNotificationType(data);
        res.then(response => {
            if (!response.error) {
                dispatch(setAddUserNotificationTypeSuccess(true));
                dispatch(updateNotificationTypes(data));
                const textMessage=`Favorite notification type${data.notificationTypes.length>1?"s have":" has"} been updated`;
                dispatch(setAlert({ variant: 'success', message: textMessage }));
            } else {
                dispatch(setAddUserNotificationTypeError(response.error));
                dispatch(setAlert({ variant: 'error', message: 'Failed' }));
            }
        });
    }
}

function updateNotificationTypes(data) {
    return {
        type: addUserNotificationType.UPDATE,
        payload: data.notificationTypes,
    };
}

function setAddUserNotificationTypePending(data) {
    return {
        type: addUserNotificationType.PENDING,
        payload: data
    };
}

function setAddUserNotificationTypeSuccess(data) {
    return {
        type: addUserNotificationType.SUCCESS,
        payload: data
    };
}

export function setAddUserNotificationTypeError(data) {
    return {
        type: addUserNotificationType.ERROR,
        payload: data
    };
}

