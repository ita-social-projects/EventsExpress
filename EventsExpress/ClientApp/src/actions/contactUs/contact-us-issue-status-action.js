import { ContactUsService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_CONTACTUS_PENDING = "SET_CONTACTUS_PENDING";
export const GET_CONTACTUS_SUCCESS = "GET_CONTACTUS_SUCCESS";

const api_serv = new ContactUsService();


export function change_issue_status(messageId, issueStatus) {
    return async dispatch => {
        dispatch(setContactUsPending(true));
        let response = await api_serv.setEventStatus({ MessageId: messageId, Status: issueStatus });
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(changeIssueStatus(eventId, eventStatus));
        return Promise.resolve();
    }
}
function setContactUsPending(data) {
    return {
        type: SET_CONTACTUS_PENDING,
        payload: data
    };
}

function changeIssueStatus(id, issueStatus) {
    return {
        type: GET_CONTACTUS_SUCCESS,
        payload: { messageId: id, issueStatus: issueStatus }
    }
}

