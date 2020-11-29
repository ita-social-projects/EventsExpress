
import EventsExpressService from '../services/EventsExpressService';


export const GET_EVENT_SCHEDULE_PENDING = "GET_EVENT_SCHEDULE_PENDING";
export const GET_EVENT_SCHEDULE_SUCCESS = "GET_EVENT_SCHEDULE_SUCCESS";
export const GET_EVENT_SCHEDULE_ERROR = "GET_EVENT_SCHEDULE_ERROR";
export const RESET_EVENT_SCHEDULE = "RESET_EVENT_SCHEDULE";

const api_serv = new EventsExpressService();

export default function getEventSchedule(id) {

    return dispatch => {
        dispatch(getEventSchedulePending(true));

        const res = api_serv.getEventSchedule(id);
        res.then(response => {
            if (response.error == null) {
                dispatch(get_eventSchedule(response));
            } else {
                dispatch(getEventScheduleError(response.error));
            }
        });
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

export function getEventScheduleError(data) {
    return {
        type: GET_EVENT_SCHEDULE_ERROR,
        payload: data
    }
}
