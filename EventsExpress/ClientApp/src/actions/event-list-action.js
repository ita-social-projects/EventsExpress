import { EventService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';

export const SET_EVENTS_PENDING = "SET_EVENTS_PENDING";
export const GET_EVENTS_SUCCESS = "GET_EVENTS_SUCCESS";
export const RESET_EVENTS = "RESET_EVENTS";
export const UPDATE_EVENTS_FILTERS = "UPDATE_EVENTS_FILTERS";

const api_serv = new EventService();

export function get_events(filters) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.getAllEvents(filters);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getEvents(jsonRes));
        return Promise.resolve();
    }
}
export function get_drafts() {
    return dispatch => {
        dispatch(setEventPending(true));
        const res = api_serv.getAllDrafts();
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvents(response));
            } else {
                dispatch(setEventError(response.error));
            }
        });
    }
}



export function setEventPending(data) {
    return {
        type: SET_EVENTS_PENDING,
        payload: data
    }
}

export function getEvents(data) {
    return {
        type: GET_EVENTS_SUCCESS,
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
