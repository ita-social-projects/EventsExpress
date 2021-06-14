import { ContactAdminService } from "../../services";
import { setErrorAllertFromResponse, setSuccessAllert } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";


export const CHANGE_STATUS = 'UPDATE_STATUS';
export const SET_CONTACT_ADMIN_PENDING = "SET_CONTACT_ADMIN_PENDING";

const api_serv = new ContactAdminService();

export default function change_issue_status(messageId, resolutionDetails, issueStatus) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.updateIssueStatus({ MessageId: messageId, ResolutionDetails: resolutionDetails, Status: issueStatus });
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(getRequestDec());
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