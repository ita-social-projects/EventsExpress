import { EventScheduleService } from '../../services';
import { setSuccessAllert, setErrorAllertFromResponse } from '../alert-action';
import { createBrowserHistory } from 'history';

export const SET_CANCEL_NEXT_EVENT_SUCCESS = "SET_CANCEL_NEXT_EVENT_SUCCESS";
export const SET_CANCEL_NEXT_EVENT_PENDING = "SET_CANCEL_NEXT_EVENT_PENDING";

const api_serv = new EventScheduleService();
const history = createBrowserHistory({ forceRefresh: true });

export default function cancel_next_eventSchedule(eventId) {
    return async dispatch => {
        dispatch(setCancelNextEventSchedulePending(true));
        let response = await api_serv.setNextEventScheduleCancel(eventId);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setCancelNextEventScheduleSuccess(true));
        dispatch(setSuccessAllert('The next event was canceled!'));
        dispatch(history.push(`/eventSchedules`));
        return Promise.resolve();
    }
}

export function setCancelNextEventScheduleSuccess(eventId) {
    return {
        type: SET_CANCEL_NEXT_EVENT_SUCCESS,
        payload: eventId
    };
}

export function setCancelNextEventSchedulePending(eventId) {
    return {
        type: SET_CANCEL_NEXT_EVENT_PENDING,
        payload: eventId
    };
}
