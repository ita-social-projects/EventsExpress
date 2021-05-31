import { EventService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_EVENTS_DATA = "GET_EVENTS_DATA";
export const RESET_EVENTS = "RESET_EVENTS";
export const UPDATE_EVENTS_FILTERS = "UPDATE_EVENTS_FILTERS";

const api_serv = new EventService();

export function get_events(filters) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getAllEvents(filters);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getEvents(jsonRes));
        return Promise.resolve();
    }
}

export function get_drafts(page = 1) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getAllDrafts(page);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getEvents(jsonRes));
        return Promise.resolve();
    }
}


export function getEvents(data) {
    return {
        type: GET_EVENTS_DATA,
        payload: data
    }
}

export function reset_events() {
    return {
        type: RESET_EVENTS
    }
}

export function updateEventsFilters(data) {
    return {
        type: UPDATE_EVENTS_FILTERS,
        payload: data
    }
}
