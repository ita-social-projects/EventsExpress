import EventsExpressService from '../services/EventsExpressService';

export const SET_EVENTS_PENDING = "SET_EVENTS_PENDING";
export const GET_EVENTS_SUCCESS = "GET_EVENTS_SUCCESS";
export const SET_EVENTS_ERROR = "SET_EVENTS_ERROR";
export const RESET_EVENTS = "RESET_EVENTS";
export const UPDATE_EVENTS_FILTERS = "UPDATE_EVENTS_FILTERS";

const api_serv = new EventsExpressService();

export function get_events(filters) {
    return dispatch => {
        dispatch(setEventPending(true));
        dispatch(setEventError(false));
        const res = api_serv.getAllEvents(filters);
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

export function setEventError(data) {
    return {
        type: SET_EVENTS_ERROR,
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
