import { EventService } from '../services';

export const GET_EVENT_PENDING = "GET_EVENT_PENDING";
export const GET_EVENT_SUCCESS = "GET_EVENT_SUCCESS";
export const GET_EVENT_ERROR = "GET_EVENT_ERROR";
export const RESET_EVENT = "RESET_EVENT";


export const event = {
    CHANGE_STATUS: 'UPDATE_EVENT',
    ERROR: 'ERROR_EVENT',
}

const api_serv = new EventService();

export default function get_event(id) {

    return dispatch => {
        dispatch(getEventPending(true));

        const res = api_serv.getEvent(id);
        res.then(response => {
            if (response.error == null) {
                dispatch(getEvent(response));

            } else {
                dispatch(getEventError(response.error));
            }
        });
    }
}

export function leave(userId, eventId) {
    return dispatch => {
        api_serv.setUserFromEvent({ userId: userId, eventId: eventId }).then(response => {
            if (response.error == null) {
                api_serv.getEvent(eventId).then(response => {
                    if (response.error == null) {
                        dispatch(getEvent(response));

                    } else {
                        dispatch(getEventError(response.error));
                    }
                });
            }
        });
    }
}

export function join(userId, eventId) {
    return dispatch => {
        api_serv.setUserToEvent({ userId: userId, eventId: eventId }).then(response => {
            if (response.error == null) {
                api_serv.getEvent(eventId).then(response => {
                    if (response.error == null) {
                        dispatch(getEvent(response));
                    } else {
                        dispatch(getEventError(response.error));
                    }
                });
            }
        });
    }
}

// ACTION APPROVER FOR USER
export function approveUser(userId, eventId, buttonAction) {
    return dispatch => {
        const res = api_serv.setApprovedUser({ userId: userId, eventId: eventId, buttonAction: buttonAction });
        res.then(response => {
            if (response.error == null) {

                const res1 = api_serv.getEvent(eventId);
                res1.then(response => {
                    if (response.error == null) {
                        dispatch(getEvent(response));

                    } else {
                        dispatch(getEventError(response.error));
                    }
                });
            }
        });
    }
}

export function deleteFromOwners(userId, eventId) {
    return dispatch => {
        api_serv.onDeleteFromOwners({ userId: userId, eventId: eventId }).then(response => {
            if (response.error == null) {
                api_serv.getEvent(eventId).then(response => {
                    if (response.error == null) {
                        dispatch(getEvent(response));
                    } else {
                        dispatch(getEventError(response.error));
                    }
                })
            }
        })
    }
}

export function promoteToOwner(userId, eventId) {
    return dispatch => {
        api_serv.onPromoteToOwner({ userId: userId, eventId: eventId }).then(response => {
            if (response.error == null) {
                api_serv.getEvent(eventId).then(response => {
                    if (response.error == null) {
                        dispatch(getEvent(response));
                    } else {
                        dispatch(getEventError(response.error));
                    }
                })
            }
        })
    }
}

// ACTION CREATOR FOR CHANGE EVENT STATUS:
export function change_event_status(eventId, reason, eventStatus) {
    return dispatch => {
        const res = api_serv.setEventStatus({ EventId: eventId, Reason: reason, EventStatus: eventStatus });

        res.then(response => {
            if (response.error == null) {
                dispatch(changeEventStatus(eventId, eventStatus));
            } else {
                dispatch(eventError(response.error));
            }
        });
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

function getEvent(data) {
    return {
        type: GET_EVENT_SUCCESS,
        payload: data
    }
}

export function getEventError(data) {
    return {
        type: GET_EVENT_ERROR,
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

function eventError(data) {
    return {
        type: event.ERROR,
        payload: data
    }
}
