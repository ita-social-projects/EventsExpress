import { InventoryService } from '../services';
import { get_inventories_by_event_id } from './inventory-list';
import { get_users_inventories_by_event_id } from './usersInventories';

export const SET_INVENTAR_ERROR = "SET_INVENTAR_ERROR";

const api_serv = new InventoryService();

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
            dispatch(get_users_inventories_by_event_id(eventId));
            if (response.error) {
                dispatch(setInvertarError(response));
            } else {
                dispatch(setInvertarError(null));
            }
        });
    }
}

export function want_to_take(data) {
    return dispatch => {
        api_serv.setWantToTake(data).then(respose => {
            dispatch(get_users_inventories_by_event_id(data.eventId));
            dispatch(get_inventories_by_event_id(data.eventId));
        });
    }
}

export function setInvertarError(data) {
    return {
        type: SET_INVENTAR_ERROR,
        payload: data
    }
}