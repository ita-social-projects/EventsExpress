import { data } from 'jquery';
import EventsExpressService from '../services/EventsExpressService';
import { generateQuerySearch } from '../components/helpers/helpers';

export const SET_EVENTS_PENDING = "SET_EVENTS_PENDING";
export const GET_EVENTS_SUCCESS = "GET_EVENTS_SUCCESS";
export const SET_EVENTS_ERROR = "SET_EVENTS_ERROR";
export const RESET_EVENTS = "RESET_EVENTS";
export const UPDATE_EVENTS_FILTERS = "UPDATE_EVENTS_FILTERS";

const api_serv = new EventsExpressService();

// TODO: Remove this func signsture.
// export function get_events(filters = "?page=1") {
export function get_events(filters) {
    return dispatch => {
        dispatch(updateEventsFilters(filters))
        dispatch(setEventPending(true));
        dispatch(setEventError(false));
        const res = api_serv.getAllEvents(generateQuerySearch(filters));
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvents(response));
            } else {
                dispatch(setEventError(response.error));
            }
        });
    }
}

// TODO: Remove this func signsture.
// export function get_eventsForAdmin(filters = "?page=1") {
export function get_eventsForAdmin(filters) {
    return dispatch => {
        dispatch(updateEventsFilters(filters))
        dispatch(setEventPending(true));
        dispatch(setEventError(false));
        const res = api_serv.getAllEventsForAdmin(generateQuerySearch(filters));
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
