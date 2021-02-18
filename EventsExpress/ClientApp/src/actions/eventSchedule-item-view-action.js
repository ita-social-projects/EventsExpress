import { EventScheduleService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';

export const GET_EVENT_SCHEDULE_PENDING = "GET_EVENT_SCHEDULE_PENDING";
export const GET_EVENT_SCHEDULE_SUCCESS = "GET_EVENT_SCHEDULE_SUCCESS";
export const RESET_EVENT_SCHEDULE = "RESET_EVENT_SCHEDULE";

const api_serv = new EventScheduleService();

export default function getEventSchedule(id) {
    return async dispatch => {
        dispatch(getEventSchedulePending(true));
        let response = await api_serv.getEventSchedule(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.resolve();
        }
        let jsonRes = await response.json();
        dispatch(get_eventSchedule(jsonRes));
        return Promise.reject();
    }
}

export function resetEventSchedule() {
    return {
        type: RESET_EVENT_SCHEDULE,
        payload: {}
    }
}

function getEventSchedulePending(data) {
    return {
        type: GET_EVENT_SCHEDULE_PENDING,
        payload: data
    }
}

function get_eventSchedule(data) {
    return {
        type: GET_EVENT_SCHEDULE_SUCCESS,
        payload: data
    }
}
