import EventsExpressService from '../services/EventsExpressService';
import getEventSchedule from './eventSchedule-item-view';

export const SET_EVENT_SCHEDULE_SUCCESS = "SET_EVENT_SCHEDULE_SUCCESS";
export const SET_EVENT_SCHEDULE_PENDING = "SET_EVENT_SCHEDULE_PENDING";
export const SET_EVENT_SCHEDULE_ERROR = "SET_EVENT_SCHEDULE_ERROR";
export const EVENT_SCHEDULE_WAS_CREATED = "EVENT_SCHEDULE_WAS_CREATED";

const api_serv = new EventsExpressService();

export default function add_eventSchedule(data) {

    return dispatch => {
        dispatch(setEventSchedulePending(true));

        const res = api_serv.setEventSchedule(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setEventScheduleSuccess(true));
                response.text().then(x => { dispatch(eventScheduleWasCreated(x)); });
            } else {
                dispatch(setEventSchedulevError(response.error));
            }
        });
    }
}


export function edit_eventSchedule(data) {
    return dispatch => {
        dispatch(setEventSchedulePending(true));

        const res = api_serv.setEventSchedule(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setEventScheduleSuccess(true));
                dispatch(getEventSchedule(data.id));
            } else {
                dispatch(setEventSchedulevError(response.error));
            }
        });
    }
}

function eventScheduleWasCreated(eventId) {
    return {
        type: EVENT_SCHEDULE_WAS_CREATED,
        payload: eventId
    }
}

export function setEventScheduleSuccess(data) {
    return {
        type: SET_EVENT_SCHEDULE_SUCCESS,
        payload: data
    };
}

export function setEventSchedulePending(data) {
    return {
        type: SET_EVENT_SCHEDULE_PENDING,
        payload: data
    };
}

export function setEventSchedulevError(data) {
    return {
        type: SET_EVENT_SCHEDULE_ERROR,
        payload: data
    };
}

