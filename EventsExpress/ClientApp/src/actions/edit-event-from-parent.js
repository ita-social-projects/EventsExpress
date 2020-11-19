import EventsExpressService from '../services/EventsExpressService';
import { SetAlert } from './alert';
import {createBrowserHistory} from 'history';

export const SET_EVENT_FROM_PARENT_SUCCESS = "SET_EVENT_FROM_PARENT_SUCCESS";
export const SET_EVENT_FROM_PARENT_PENDING = "SET_EVENT_FROM_PARENT_PENDING";
export const SET_EVENT_FROM_PARENT_ERROR = "SET_EVENT_FROM_PARENT_ERROR";
export const EVENT_FROM_PARENT_WAS_CREATED = "EVENT_FROM_PARENT_WAS_CREATED";

const api_serv = new EventsExpressService();
const history = createBrowserHistory({forceRefresh:true});

export default function edit_event_from_parent(data) {

    return dispatch => {
        dispatch(setEventFromParentPending(true));

        const res = api_serv.setEventFromParent(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setEventFromParentSuccess(true));
                response.json().then(x => { 
                    dispatch(eventFromParentWasCreated(x));
                    dispatch(SetAlert({ variant: 'success', message: 'Your event was created!' }));
                    dispatch(history.push(`/event/${x.id}/1`)); } );
            } else {
                dispatch(setEventFromParentError(response.error));
            }
        });
    }
}

function eventFromParentWasCreated(eventId) {
    return {
        type: EVENT_FROM_PARENT_WAS_CREATED,
        payload: eventId
    }
}

export function setEventFromParentSuccess(data) {
    return {
        type: SET_EVENT_FROM_PARENT_SUCCESS,
        payload: data
    };
}

export function setEventFromParentPending(data) {
    return {
        type: SET_EVENT_FROM_PARENT_PENDING,
        payload: data
    };
}

export function setEventFromParentError(data) {
    return {
        type: SET_EVENT_FROM_PARENT_ERROR,
        payload: data
    };
}

