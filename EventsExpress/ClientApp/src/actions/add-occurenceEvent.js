import EventsExpressService from '../services/EventsExpressService';
import getOccurenceEvent from './occurenceEvent-item-view';

export const SET_OCCURENCE_EVENT_SUCCESS = "SET_OCCURENCE_EVENT_SUCCESS";
export const SET_OCCURENCE_EVENT_PENDING = "SET_OCCURENCE_EVENT_PENDING";
export const SET_OCCURENCE_EVENT_ERROR = "SET_OCCURENCE_EVENT_ERROR";
export const EVENT_OCCURENCE_WAS_CREATED = "EVENT_OCCURENCE_WAS_CREATED";

const api_serv = new EventsExpressService();

export default function add_occurenceEvent(data) {

    return dispatch => {
        dispatch(setOccurenceEventPending(true));

        const res = api_serv.setOccurenceEvent(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setOccurenceEventSuccess(true));
                response.text().then(x => { dispatch(occurenceEventWasCreated(x)); });
            } else {
                dispatch(setOccurenceEventError(response.error));
            }
        });
    }
}


export function edit_occurenceEvent(data) {
    return dispatch => {
        dispatch(setOccurenceEventPending(true));

        const res = api_serv.setOccurenceEvent(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setOccurenceEventSuccess(true));
                dispatch(getOccurenceEvent(data.id));
            } else {
                dispatch(setOccurenceEventError(response.error));
            }
        });
    }
}

function occurenceEventWasCreated(eventId) {
    return {
        type: EVENT_OCCURENCE_WAS_CREATED,
        payload: eventId
    }
}

export function setOccurenceEventSuccess(data) {
    return {
        type: SET_OCCURENCE_EVENT_SUCCESS,
        payload: data
    };
}

export function setOccurenceEventPending(data) {
    return {
        type: SET_OCCURENCE_EVENT_PENDING,
        payload: data
    };
}

export function setOccurenceEventError(data) {
    return {
        type: SET_OCCURENCE_EVENT_ERROR,
        payload: data
    };
}

