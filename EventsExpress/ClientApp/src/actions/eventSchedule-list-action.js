import { EventScheduleService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';

export const SET_EVENTS_SCHEDULE_PENDING = "SET_EVENTS_SCHEDULE_PENDING";
export const GET_EVENTS_SCHEDULE_SUCCESS = "GET_EVENTS_SCHEDULE_SUCCESS";
export const RESET_EVENTS_SCHEDULE = "RESET_EVENTS_SCHEDULE";

const api_serv = new EventScheduleService();

export function getEventSchedules() {
    return async dispatch => {
        dispatch(setEventSchedulesPending(true));
        let response = await api_serv.getAllEventSchedules();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(get_eventSchedules(jsonRes));
        return Promise.resolve();
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

export function reset_EventsSchedule() {
    return {
        type: RESET_EVENTS_SCHEDULE,
    }
}
