import EventsExpressService from '../services/EventsExpressService';

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
            console.log('action', response);
            if (response.error == null) {
                dispatch(getInventar(response));
            } else {
                dispatch(setInventarError(response.error));
            }
        });
    }
}

export function delete_item(id) {
    return dispatch => {
        dispatch(setInventarPending(true));
        dispatch(setInventarError(false));
        const res = api_serv.setItemDelete(id);
        res.then(response => {
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