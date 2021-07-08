import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const addUserNotificationType = {
    UPDATE: "UPDATE_NOTIFICATION_TYPES",
}

const api_serv = new UserService();

export default function setUserNotificationTypes(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setUserNotificationType(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
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
