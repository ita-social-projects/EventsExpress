import EventsExpressService from '../services/EventsExpressService';

export const SET_OCCURENCE_EVENTS_PENDING = "SET_OCCURENCE_EVENTS_PENDING";
export const GET_OCCURENCE_EVENTS_SUCCESS = "GET_OCCURENCE_EVENTS_SUCCESS";
export const SET_OCCURENCE_EVENTS_ERROR = "SET_OCCURENCE_EVENTS_ERROR";
export const RESET_OCCURENCE_EVENTS = "RESET_OCCURENCE_EVENTS";

const api_serv = new EventsExpressService();

export function getOccurenceEvents() {
    return dispatch => {
        dispatch(setOccurenceEventsPending(true));
        dispatch(setOccurenceEventsError(false));
        const res = api_serv.getAllOccurenceEvents();
        res.then(response => {
            if (response.error == null) {
                dispatch(get_occurenceEvents(response));
            } else {
                dispatch(setOccurenceEventsError(response.error));
            }
        });
    }
}

export function setOccurenceEventsPending(data) {
    return {
        type: SET_OCCURENCE_EVENTS_PENDING,
        payload: data
    }
}

export function get_occurenceEvents(data) {
    return {
        type: GET_OCCURENCE_EVENTS_SUCCESS,
        payload: data
    }
}

export function setOccurenceEventsError(data) {
    return {
        type: SET_OCCURENCE_EVENTS_ERROR,
        payload: data
    }
}

export function reset_occurenceEvents() {
    return {
        type: RESET_OCCURENCE_EVENTS
    }
}

