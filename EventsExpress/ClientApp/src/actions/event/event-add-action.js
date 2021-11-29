import { SubmissionError } from 'redux-form';
import { EventService } from '../../services';
import get_event, { getEvent } from './event-item-view-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { createBrowserHistory } from 'history';
import { getRequestInc, getRequestDec } from "../request-count-action";
import { setSuccessAllert, setErrorAllertFromResponse } from '../alert-action';

export const EVENT_WAS_CREATED = "EVENT_WAS_CREATED";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function add_event() {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setEvent();
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        const event = await response.json();
        dispatch(history.push(`/editEvent/${event.id}`));
        return Promise.resolve();
    }
}

export function edit_event(data, onError, onSuccess) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.editEvent(data);
        dispatch(getRequestDec());
        if (!response.ok && onError) {
            await onError(response);
        } else if (response.ok && onSuccess) {
            onSuccess(response);
        }
        dispatch(getEvent(data));
        return Promise.resolve();
    }
}

export function publish_event(eventId) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.publishEvent(eventId);
        dispatch(getRequestDec());
        if (response.ok) {
            dispatch(get_event(eventId));
            dispatch(setSuccessAllert('Your event has been successfully published!'));
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



