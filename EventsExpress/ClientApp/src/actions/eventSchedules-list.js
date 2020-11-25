import EventsExpressService from '../services/EventsExpressService';

export const SET_EVENTS_SCHEDULE_PENDING = "SET_EVENTS_SCHEDULE_PENDING";
export const GET_EVENTS_SCHEDULE_SUCCESS = "GET_EVENTS_SCHEDULE_SUCCESS";
export const SET_EVENTS_SCHEDULE_ERROR = "SET_EVENTS_SCHEDULE_ERROR";
export const RESET_EVENTS_SCHEDULE = "RESET_EVENTS_SCHEDULE";

const api_serv = new EventsExpressService();

export function getEventSchedules() {
    return dispatch => {
        dispatch(setEventSchedulesPending(true));
        dispatch(setEventSchedulesError(false));
        const res = api_serv.getAllEventSchedules();
        res.then(response => {
            if (response.error == null) {
                dispatch(get_eventSchedules(response));
            } else {
                dispatch(setEventSchedulesError(response.error));
            }
        });
    }
}

export function setEventSchedulesPending(data) {
    return {
        type: SET_EVENTS_SCHEDULE_PENDING,
        payload: data
    }
}

export function get_eventSchedules(data) {
    return {
        type: GET_EVENTS_SCHEDULE_SUCCESS,
        payload: data
    }
}

export function setEventSchedulesError(data) {
    return {
        type: SET_EVENTS_SCHEDULE_ERROR,
        payload: data
    }
}

export function reset_EventsSchedule() {
    return {
        type: RESET_EVENTS_SCHEDULE
    }
}

