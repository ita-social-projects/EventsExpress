import { SubmissionError } from 'redux-form';
import { EventService } from '../../services';
import { getEvent } from './event-item-view-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { createBrowserHistory } from 'history';
import { getRequestInc, getRequestDec } from "../request-count-action";


export const EVENT_WAS_CREATED = "EVENT_WAS_CREATED";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function add_event(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setEvent(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        const event = await response.json();
        dispatch(history.push(`/editEvent/${event.id}`));
        return Promise.resolve();
    }
}

export function edit_event(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.editEvent(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getEvent(data));
        dispatch(getRequestDec());
        return Promise.resolve();
    }
}

export function publish_event(eventId) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.publishEvent(eventId);
        if (response.ok) {
            dispatch(getEvent(eventId));
            dispatch(getRequestDec());
            dispatch(history.push(`/event/${eventId}/1`));
            dispatch(eventWasCreated(eventId));
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



