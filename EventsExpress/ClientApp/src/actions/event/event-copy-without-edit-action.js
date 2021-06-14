import { EventService } from '../../services';
import { setErrorAllertFromResponse, setSuccessAllert } from '../alert-action';
import { createBrowserHistory } from 'history';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const SET_COPY_EVENT_SUCCESS = "SET_COPY_EVENT_SUCCESS";
export const SET_COPY_EVENT_PENDING = "SET_COPY_EVENT_PENDING";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function add_copy_event(eventId) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setCopyEvent(eventId);
        dispatch(getRequestDec())
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(setSuccessAllert('Your event was created!'));
        dispatch(history.push(`/event/${jsonRes.id}/1`));
        return Promise.resolve();
    }
}

export function setCopyEventSuccess(eventId) {
    return {
        type: SET_COPY_EVENT_SUCCESS,
        payload: eventId
    };
}

export function setCopyEventPending(eventId) {
    return {
        type: SET_COPY_EVENT_PENDING,
        payload: eventId
    };
}
