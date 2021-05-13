import { ContactUsService } from "../../services";
import { setErrorAllertFromResponse, setSuccessAllert } from '../alert-action';

export const CHANGE_STATUS = 'UPDATE_STATUS';
export const SET_CONTACTUS_PENDING = "SET_CONTACTUS_PENDING";

const api_serv = new ContactUsService();

export default function change_issue_status(messageId, issueStatus) {
    return async dispatch => {
        dispatch(setContactUsPending(true));
        let response = await api_serv.setIssueStatus({ MessageId: messageId, Status: issueStatus});
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(changeIssueStatus(messageId, issueStatus));
        dispatch(setSuccessAllert('Issue status was changed'));
        return Promise.resolve();
    }
}

function changeIssueStatus(id, issueStatus) {
    return {
        type: CHANGE_STATUS,
        payload: { messageId: id, issueStatus: issueStatus}
    }
}

function setContactUsPending(data) {
    return {
        type: SET_CONTACTUS_PENDING,
        payload: data
    };
}
