import { EventService } from '../../services';
import { setErrorAllertFromResponse, setSuccessAllert } from '../alert-action';
import { createBrowserHistory } from 'history';

export const SET_EVENT_FROM_PARENT_SUCCESS = "SET_EVENT_FROM_PARENT_SUCCESS";
export const SET_EVENT_FROM_PARENT_PENDING = "SET_EVENT_FROM_PARENT_PENDING";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function edit_event_from_parent(data) {
    return async dispatch => {
        dispatch(setEventFromParentPending(true));
        let response = await api_serv.setEventFromParent(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setEventFromParentSuccess(true));
        let jsonRes  = await response.json();
        dispatch(setSuccessAllert('Your event was created!'));
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
