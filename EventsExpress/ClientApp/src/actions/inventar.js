import EventsExpressService from '../services/EventsExpressService';
import {get_inventories_by_event_id} from './inventory-list';

export const SET_INVENTAR_ERROR = "SET_INVENTAR_ERROR";

const api_serv = new EventsExpressService();

export function add_item(item, eventId) {
    return dispatch => {
        const res = api_serv.setItemToInventory(item, eventId);
        res.then(response => {
            dispatch(get_inventories_by_event_id(eventId));
            if (response.error) {
                dispatch(setInvertarError(response.error));
            } else {
                dispatch(setInvertarError(null));
            }
        });
    }
}

export function delete_item(itemId, eventId) {
    return dispatch => {
        const res = api_serv.setItemDelete(itemId, eventId);
        res.then(response => {
            dispatch(get_inventories_by_event_id(eventId));
            if (response.error) {
                dispatch(setInvertarError(response));
            } else {
                dispatch(setInvertarError(null));
            }
        });
    }
}

export function edit_item(item, eventId) {
    return dispatch => {
        const res = api_serv.setItem(item, eventId);
        res.then(response => {
            dispatch(get_inventories_by_event_id(eventId));
            if (response.error) {
                dispatch(setInvertarError(response));
            } else {
                dispatch(setInvertarError(null));
            }
        });
    }
}

export function setInvertarError(data) {
    return {
        type: SET_INVENTAR_ERROR,
        payload: data
    }
}