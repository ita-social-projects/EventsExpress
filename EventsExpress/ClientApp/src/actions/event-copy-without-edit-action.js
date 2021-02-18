import { EventService } from '../services';
import { setErrorAllertFromResponse, setSuccessAllert } from './alert-action';
import { createBrowserHistory } from 'history';

export const SET_COPY_EVENT_SUCCESS = "SET_COPY_EVENT_SUCCESS";
export const SET_COPY_EVENT_PENDING = "SET_COPY_EVENT_PENDING";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function add_copy_event(eventId) {
    return async dispatch => {
        dispatch(setCopyEventPending(true));
        let response = await api_serv.setCopyEvent(eventId);
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
