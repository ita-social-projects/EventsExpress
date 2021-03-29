import { EventScheduleService } from '../../services';
import { setSuccessAllert, setErrorAllertFromResponse } from '../alert-action';
import { createBrowserHistory } from 'history';

export const SET_CANCEL_ALL_EVENT_SUCCESS = "SET_CANCEL_ALL_EVENT_SUCCESS";
export const SET_CANCEL_ALL_EVENT_PENDING = "SET_CANCEL_ALL_EVENT_PENDING";

const api_serv = new EventScheduleService();
const history = createBrowserHistory({ forceRefresh: true });

export default function cancel_all_eventSchedules(eventId) {
    return async dispatch => {
        dispatch(setCancelAllEventSchedulesPending(true));
        let response = await api_serv.setEventSchedulesCancel(eventId);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setCancelAllEventSchedulesSuccess(true));
        dispatch(setSuccessAllert('Your events was canceled!'));
        dispatch(history.push(`/eventSchedules`));
        return Promise.resolve();
    }
}

export function setCancelAllEventSchedulesSuccess(eventId) {
    return {
        type: SET_CANCEL_ALL_EVENT_SUCCESS,
        payload: eventId
    };
}

export function setCancelAllEventSchedulesPending(eventId) {
    return {
        type: SET_CANCEL_ALL_EVENT_PENDING,
        payload: eventId
    };
}
