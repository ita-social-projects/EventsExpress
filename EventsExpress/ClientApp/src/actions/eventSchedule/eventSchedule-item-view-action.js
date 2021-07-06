import { EventScheduleService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_EVENT_SCHEDULE_DATA = "GET_EVENT_SCHEDULE_DATA";
export const RESET_EVENT_SCHEDULE = "RESET_EVENT_SCHEDULE";

const api_serv = new EventScheduleService();

export default function getEventSchedule(id) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getEventSchedule(id);
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(get_eventSchedule(jsonRes));
        return Promise.resolve();
    }
}

export function resetEventSchedule() {
    return {
        type: RESET_EVENT_SCHEDULE,
        payload: {}
    }
}

function get_eventSchedule(data) {
    return {
        type: GET_EVENT_SCHEDULE_DATA,
        payload: data
    }
}
