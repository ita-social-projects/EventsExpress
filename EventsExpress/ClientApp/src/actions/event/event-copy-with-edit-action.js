import { EventService } from '../../services';
import { setErrorAllertFromResponse, setSuccessAllert } from '../alert-action';
import { createBrowserHistory } from 'history';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const SET_EVENT_FROM_PARENT_SUCCESS = "SET_EVENT_FROM_PARENT_SUCCESS";
export const SET_EVENT_FROM_PARENT_PENDING = "SET_EVENT_FROM_PARENT_PENDING";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function edit_event_from_parent(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setEventFromParent(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(getRequestDec());
        let jsonRes  = await response.json();
        dispatch(setSuccessAllert('Your event has been successfully created!'));
        dispatch(history.push(`/event/${jsonRes.id}/1`));
        return Promise.resolve();
    }
}

export function setEventFromParentSuccess(data) {
    return {
        type: SET_EVENT_FROM_PARENT_SUCCESS,
        payload: data
    };
}

export function setEventFromParentPending(data) {
    return {
        type: SET_EVENT_FROM_PARENT_PENDING,
        payload: data
    };
}
