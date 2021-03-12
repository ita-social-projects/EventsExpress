import { EventService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_EVENTS_PROFILE_PENDING = "SET_EVENTS_PROFILE_PENDING";
export const GET_EVENTS_PROFILE_SUCCESS = "GET_EVENTS_PROFILE_SUCCESS";

const api_serv = new EventService();

export function get_future_events(id, page = 1) {
    let call = api_serv.getFutureEvents;
    return master(call, id, page);
}

export function get_past_events(id, page = 1) {
    let call = api_serv.getPastEvents;
    return master(call, id, page);
}

export function get_visited_events(id, page = 1) {
    let call = api_serv.getVisitedEvents;
    return master(call, id, page);
}

export function get_events_togo(id, page = 1) {
    let call = api_serv.getEventsToGo;
    return master(call, id, page);
}

function master(call, id, page) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await call(id, page);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json()
        dispatch(getEvents(jsonRes));
        return Promise.resolve();
    }
}

function setEventPending(data) {
    return {
        type: SET_EVENTS_PROFILE_PENDING,
        payload: data
    }
}

function getEvents(data) {
    return {
        type: GET_EVENTS_PROFILE_SUCCESS,
        payload: data
    }
}
