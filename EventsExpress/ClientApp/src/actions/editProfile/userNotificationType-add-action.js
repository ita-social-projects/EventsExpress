import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/helpers.js';

export const addUserNotificationType = {
    PENDING: "SET_ADD_USER_NOTIFICATION_TYPE_PENDING",
    SUCCESS: "SET_ADD_USER_NOTIFICATION_TYPE_SUCCESS",
    UPDATE: "UPDATE_NOTIFICATION_TYPES",
}

const api_serv = new UserService();

export default function setUserNotificationTypes(data) {
    return async dispatch => {
        dispatch(setAddUserNotificationTypePending(true));
        let response = await api_serv.setUserNotificationType(data);
        if (!response.ok) {
            throw new SubmissionError(buildValidationState(response));
        }
        dispatch(setAddUserNotificationTypeSuccess(true));
        dispatch(updateNotificationTypes(data));
        const textMessage = `Favorite notification type${data.notificationTypes.length > 1 ? "s have" : " has"} been updated`;
        dispatch(setSuccessAllert(textMessage));
        return Promise.resolve();
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


