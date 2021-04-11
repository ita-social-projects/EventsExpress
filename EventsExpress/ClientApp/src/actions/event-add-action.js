import { SubmissionError } from 'redux-form';
import { EventService } from '../services';
import get_event from './event-item-view';
import { buildValidationState } from '../components/helpers/action-helpers';
import { createBrowserHistory } from 'history';

export const SET_EVENT_SUCCESS = "SET_EVENT_SUCCESS";
export const SET_EVENT_PENDING = "SET_EVENT_PENDING";
export const EVENT_WAS_CREATED = "EVENT_WAS_CREATED";
export const PUBLISH_EVENT = "PUBLISH_EVENT";

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

export function add_wizard(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.setEvent(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        const event = await response.json();
        dispatch(history.push(`/editWizard/${event.id}`));
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

export function edit_event_part1(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.part1(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        dispatch(get_event(data.id));
        return Promise.resolve();
    }
}

export function edit_event_part2(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.part2(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        dispatch(get_event(data.id));
        return Promise.resolve();
    }
}

export function edit_event_part3(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.part3(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        dispatch(get_event(data.id));
        return Promise.resolve();
    }
}

export function edit_event_part5(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.part5(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setEventSuccess(true));
        dispatch(get_event(data.id));
        return Promise.resolve();
    }
}

export function publish_event(data) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.publishEvent(data);
        if (response.ok) {
            dispatch(setEventSuccess(true));
            dispatch(get_event(data));
            dispatch(eventWasCreated(data));
            return Promise.resolve();
        }
        else {
            dispatch(publishEventErrors( await buildValidationState(response)));
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

export function publishEventErrors(data) {
    return {
        type: PUBLISH_EVENT,
        payload: data
    };
}

