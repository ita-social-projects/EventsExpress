
import EventsExpressService from '../services/EventsExpressService';


export const SET_EVENTS_PROFILE_PENDING = "SET_EVENTS_PROFILE_PENDING";
export const GET_EVENTS_PROFILE_SUCCESS = "GET_EVENTS_PROFILE_SUCCESS";
export const SET_EVENTS_PROFILE_ERROR = "SET_EVENTS_PROFILE_ERROR";


const api_serv = new EventsExpressService();

export default function get_future_events(id) {

    return dispatch => {
        dispatch(setEventPending(true));

        const res = api_serv.getFutureEvents(id);
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvents(response));

            } else {
                dispatch(setEventError(response.error));
            }
        });
    }
}

export function get_past_events(id) {

    return dispatch => {
        dispatch(setEventPending(true));

        const res = api_serv.getPastEvents(id);
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvents(response));

            } else {
                dispatch(setEventError(response.error));
            }
        });
    }
}

export function get_visited_events(id) {

    return dispatch => {
        dispatch(setEventPending(true));

        const res = api_serv.getVisitedEvents(id);
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvents(response));

            } else {
                dispatch(setEventError(response.error));
            }
        });
    }
}

export function get_events_togo(id) {

    return dispatch => {
        dispatch(setEventPending(true));

        const res = api_serv.getEventsToGo(id);
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvents(response));

            } else {
                dispatch(setEventError(response.error));
            }
        });
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

function setEventError(data) {
    return {
        type: SET_EVENTS_PROFILE_ERROR,
        payload: data
    }
}