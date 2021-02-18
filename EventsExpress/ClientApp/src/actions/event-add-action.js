import { SubmissionError } from 'redux-form';
import { EventService } from '../services';
import get_event from './event-item-view';
import { buildValidationState } from '../components/helpers/action-helpers';

export const SET_EVENT_SUCCESS = "SET_EVENT_SUCCESS";
export const SET_EVENT_PENDING = "SET_EVENT_PENDING";
export const EVENT_WAS_CREATED = "EVENT_WAS_CREATED";

const api_serv = new EventService();

export default function add_event(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.setEvent(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        const text = await response.text();
        dispatch(eventWasCreated(text));
        return Promise.resolve();
    }
}

export function edit_event(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.editEvent(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        dispatch(get_event(data.id));
        return Promise.resolve();
    }
}

function eventWasCreated(eventId) {
    return {
        type: EVENT_WAS_CREATED,
        payload: eventId
    };
}

export function setEventSuccess(data) {
    return {
        type: SET_EVENT_SUCCESS,
        payload: data
    };
}

export function setEventPending(data) {
    return {
        type: SET_EVENT_PENDING,
        payload: data
    };
}


