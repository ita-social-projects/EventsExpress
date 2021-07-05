import { EventScheduleService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_EVENTS_SCHEDULE_DATA = "GET_EVENTS_SCHEDULE_DATA";
export const RESET_EVENTS_SCHEDULE = "RESET_EVENTS_SCHEDULE";

const api_serv = new EventScheduleService();

export function getEventSchedules() {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getAllEventSchedules();
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(get_eventSchedules(jsonRes));
        return Promise.resolve();
    }
}

export function get_eventSchedules(data) {
    return {
        type: GET_EVENTS_SCHEDULE_DATA,
        payload: data
    }
}

export function reset_EventsSchedule() {
    return {
        type: RESET_EVENTS_SCHEDULE,
    }
}
