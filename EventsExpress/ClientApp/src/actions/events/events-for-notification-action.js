import { EventService } from '../../services';
import { getEvents } from '../event/event-list-action';
import { getRequestInc, getRequestDec } from "../request-count-action";
import { setErrorAllertFromResponse } from '../alert-action';

const api_serv = new EventService();

export function eventsForNotification(eventIds, page = 1) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getEvents(eventIds, page);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getEvents(jsonRes));
        return Promise.resolve();
    }
}
