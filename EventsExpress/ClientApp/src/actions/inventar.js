import EventsExpressService from '../services/EventsExpressService';
import {get_inventories_by_event_id} from './inventory-list';

export const SET_INVENTAR_PENDING = "SET_INVENTAR_PENDING";
export const GET_INVENTAR_SUCCESS = "GET_INVENTAR_SUCCESS";
export const SET_INVENTAR_ERROR = "SET_INVENTAR_ERROR";

const api_serv = new EventsExpressService();

export function add_item(item, eventId) {
    return dispatch => {
        dispatch(setInventarPending(true));
        dispatch(setInventarError(false));
        const res = api_serv.setItemToInventory(item, eventId);
        res.then(response => {
            console.log("added", eventId);
            dispatch(get_inventories_by_event_id(eventId));
            if (response.error == null) {
                dispatch(getInventar(response));
            } else {
                dispatch(setInventarError(response.error));
            }
        });
    }
}

export function delete_item(itemId, eventId) {
    return dispatch => {
        dispatch(setInventarPending(true));
        dispatch(setInventarError(false));
        const res = api_serv.setItemDelete(itemId);
        console.log('deleting...', eventId);
        res.then(response => {
            console.log('deleted', eventId);
            dispatch(get_inventories_by_event_id(eventId));
            if (response.error == null) {
                dispatch(getInventar(response));
            } else {
                dispatch(setInventarError(response.error));
            }
        });
    }
}

export function edit_item(item, eventId) {
    return dispatch => {
        dispatch(setInventarPending(true));
        dispatch(setInventarError(false));
        const res = api_serv.setItem(item);
        res.then(response => {
            console.log('edited', eventId);
            dispatch(get_inventories_by_event_id(eventId));
            if (response.error == null) {
                dispatch(getInventar(response));
            } else {
                dispatch(setInventarError(response.error));
            }
        });
    }
}

export function setInventarPending(data) {
    return {
        type: SET_INVENTAR_PENDING,
        payload: data
    }
}

export function getInventar(data) {
    return {
        type: GET_INVENTAR_SUCCESS,
        payload: data
    }
}

export function setInventarError(data) {
    return {
        type: SET_INVENTAR_ERROR,
        payload: data
    }
}