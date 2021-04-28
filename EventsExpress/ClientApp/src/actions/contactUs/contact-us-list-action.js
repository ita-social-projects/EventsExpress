import { UserService } from "../../services";
import { setErrorAllertFromResponse } from '../alert-action';

export const contactUs = {
    PENDING: "SET_CONTACTUS_PENDING",
}

const api_serv = new UserService();

export default function getIssues(data, page = 1) {
    return async dispatch => {
        dispatch(setContactUsPending(true));
        let response = await api_serv.getAllIssues(data, page);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getListOfIssues(jsonRes));
        return Promise.resolve();
    }
}

function setContactUsPending(data) {
    return {
        type: contactUs.PENDING,
        payload: data
    };
}


function getListOfIssues(data) {
    return {
        type: GET_COMMENTS_SUCCESS,
        payload: data
    }
}

