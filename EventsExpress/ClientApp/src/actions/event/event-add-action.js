import { SubmissionError } from 'redux-form';
import { EventService } from '../../services';
import get_event, { getEvent } from './event-item-view-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { createBrowserHistory } from 'history';


export const SET_EVENT_SUCCESS = "SET_EVENT_SUCCESS";
export const SET_EVENT_PENDING = "SET_EVENT_PENDING";
export const EVENT_WAS_CREATED = "EVENT_WAS_CREATED";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function add_event(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.setEvent(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        const event = await response.json();
        dispatch(history.push(`/editEvent/${event.id}`));
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
        dispatch(getEvent(data));
        return Promise.resolve();
    }
}

export function publish_event(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.publishEvent(data);
        if (response.ok) {
            dispatch(setEventSuccess(true));
            dispatch(getEvent(data));
            dispatch(eventWasCreated(data.id));
            return Promise.resolve();
        }
        else {
            throw new SubmissionError(await buildValidationState(response));
        }
    }
}

function eventWasCreated(eventId) {
    return {
        type: EVENT_WAS_CREATED,
        payload: eventId
    }
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


