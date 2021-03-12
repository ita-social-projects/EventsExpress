import { EventService } from '../../services';
import { setEventPending, getEvents } from '../event/event-list-action';
import { setErrorAllertFromResponse } from '../alert-action';

const api_serv = new EventService();

export function eventsForNotification(eventIds, page = 1) {
    return async dispatch => {
        dispatch(setEventPending(true));
        let response = await api_serv.getEvents(eventIds, page);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getEvents(jsonRes));
        return Promise.resolve();
    }
}
