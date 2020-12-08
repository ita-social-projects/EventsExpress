import { InventoryService } from '../services';

export const SET_INVENTORY_PENDING = "SET_INVENTORY_PENDING";
export const GET_INVENTORY_SUCCESS = "GET_INVENTORY_SUCCESS";
export const SET_INVENTORY_ERROR = "SET_INVENTORY_ERROR";

const api_serv = new InventoryService();

export function get_inventories_by_event_id(eventId) {
    return dispatch => {
        dispatch(setInventoryPending(true));
        const res = api_serv.getInventoriesByEventId(eventId);
        res.then(response => {
            if (response.error == null) {
                dispatch(getInventorySuccess(response));
            } else {
                dispatch(setInventoryError(response.error));
            }
        });
    }
}

export function update_inventories(inventoryList) {
    return dispatch => {
        dispatch(getInventorySuccess(inventoryList));
    }
}

export function setInventoryPending(data) {
    return {
        type: SET_INVENTORY_PENDING,
        payload: data
    }
}

export function getInventorySuccess(data) {
    return {
        type: GET_INVENTORY_SUCCESS,
        payload: data
    }
}

export function setInventoryError(data) {
    return {
        type: SET_INVENTORY_ERROR,
        payload: data
    }
}