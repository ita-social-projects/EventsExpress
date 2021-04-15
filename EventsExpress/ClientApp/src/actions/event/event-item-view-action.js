import { EventService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_EVENT_PENDING = "GET_EVENT_PENDING";
export const GET_EVENT_SUCCESS = "GET_EVENT_SUCCESS";
export const RESET_EVENT = "RESET_EVENT";

export const event = {
    CHANGE_STATUS: 'UPDATE_EVENT',
}

const api_serv = new EventService();

export default function get_event(id) {
    return async dispatch => {
        dispatch(getEventPending(true));

        let response = await api_serv.getEvent(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getEvent(jsonRes));
        return Promise.resolve();
    }
}

export function leave(userId, eventId) {
    return async dispatch => {
        let response = await api_serv.setUserFromEvent({ userId: userId, eventId: eventId });
        if (response.ok) {
            let res = await api_serv.getEvent(eventId);
            if (!res.ok) {
                dispatch(setErrorAllertFromResponse(response));
                return Promise.reject();
            }
            let jsonRes = await res.json();
            dispatch(getEvent(jsonRes))
            return Promise.reject();
        }
    }
}

export function join(userId, eventId) {
    return async dispatch => {
        let response = await api_serv.setUserToEvent({ userId: userId, eventId: eventId });
        if (response.ok) {
            let res = await api_serv.getEvent(eventId);
            if (!res.ok) {
                dispatch(setErrorAllertFromResponse(response));
                return Promise.reject();
            }
            let jsonRes = await res.json();
            dispatch(getEvent(jsonRes))
            return Promise.reject();
        }
    }
}

// ACTION APPROVER FOR USER
export function approveUser(userId, eventId, buttonAction) {
    return async dispatch => {
        let response = await api_serv.setApprovedUser({ userId: userId, eventId: eventId, buttonAction: buttonAction });
        if (response.ok) {
            let res = await api_serv.getEvent(eventId);
            if (!res.ok) {
                dispatch(setErrorAllertFromResponse(response));
                return Promise.reject();
            }
            let jsonRes = await res.json();
            dispatch(getEvent(jsonRes))
            return Promise.reject();
        }
    }
}

// ACTION CREATOR FOR DELETE FROM OWNERS:
export function deleteFromOwners(userId, eventId) {
    return async dispatch => {
        let response = await api_serv.onDeleteFromOwners({ userId: userId, eventId: eventId });
        if (response.ok) {
            let res = await api_serv.getEvent(eventId);
            if (!res.ok) {
                dispatch(setErrorAllertFromResponse(response));
                return Promise.reject();
            }
            let jsonRes = await res.json();
            dispatch(getEvent(jsonRes))
            return Promise.reject();
        }
    }
}

// ACTION CREATOR FOR PROMOTE TO OWNER:
export function promoteToOwner(userId, eventId) {
    return async dispatch => {
        let response = await api_serv.onPromoteToOwner({ userId: userId, eventId: eventId });
        if (response.ok) {
            let res = await api_serv.getEvent(eventId);
            if (!res.ok) {
                dispatch(setErrorAllertFromResponse(response));
                return Promise.reject();
            }
            let jsonRes = await res.json();
            dispatch(getEvent(jsonRes))
            return Promise.reject();
        }
    }
}

// ACTION CREATOR FOR CHANGE EVENT STATUS:
export function change_event_status(eventId, reason, eventStatus) {
    return async dispatch => {
        let response = await api_serv.setEventStatus({ EventId: eventId, Reason: reason, EventStatus: eventStatus });
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(changeEventStatus(eventId, eventStatus));
        return Promise.resolve();
    }
}

export function resetEvent() {
    return {
        type: RESET_EVENT,
        payload: {}
    }
}

function getEventPending(data) {
    return {
        type: GET_EVENT_PENDING,
        payload: data
    }
}

export function getEvent(data) {
    return {
        type: GET_EVENT_SUCCESS,
        payload: data
    }
}

//CHANGE EVENT ACTIONS
function changeEventStatus(id, eventStatus) {
    return {
        type: event.CHANGE_STATUS,
        payload: { eventId: id, eventStatus: eventStatus }
    }
}
