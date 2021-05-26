import { ContactAdminService } from "../../services";
import { setErrorAllertFromResponse, setSuccessAllert } from '../alert-action';

export const CHANGE_STATUS = 'UPDATE_STATUS';
export const SET_CONTACT_ADMIN_PENDING = "SET_CONTACT_ADMIN_PENDING";

const api_serv = new ContactAdminService();

export default function change_issue_status(messageId, resolutionDetails, issueStatus) {
    return async dispatch => {
        dispatch(setContactAdminPending(true));
        let response = await api_serv.updateIssueStatus({ MessageId: messageId, ResolutionDetails: resolutionDetails, Status: issueStatus });
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(changeIssueStatus(messageId, issueStatus));
        dispatch(setSuccessAllert('Issue status was changed'));
        return Promise.resolve();
    }
}

function changeIssueStatus(messageId, issueStatus) {
    return {
        type: CHANGE_STATUS,
        payload: { MessageId: messageId, issueStatus: issueStatus }
    }
}

function setContactAdminPending(data) {
    return {
        type: SET_CONTACT_ADMIN_PENDING,
        payload: data
    };
}
