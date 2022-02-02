import { EventService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_EVENT_DATA = "GET_EVENT_DATA";
export const RESET_EVENT = "RESET_EVENT";

export const event = {
    CHANGE_STATUS: 'UPDATE_EVENT',
}

const api_serv = new EventService();

export default function get_event(id) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.getEvent(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getEvent(jsonRes));
        dispatch(getRequestDec());
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

// ACTION CREATOR FOR DELETE FROM ORGANIZERS:
export function deleteFromOrganizers(userId, eventId) {
    return async dispatch => {
        let response = await api_serv.onDeleteFromOrganizers({ userId: userId, eventId: eventId });
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

// ACTION CREATOR FOR PROMOTE TO ORGANIZER:
export function promoteToOrganizer(userId, eventId) {
    return async dispatch => {
        let response = await api_serv.onPromoteToOrganizer({ userId: userId, eventId: eventId });
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

export function getEvent(data) {
    return {
        type: GET_EVENT_DATA,
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
