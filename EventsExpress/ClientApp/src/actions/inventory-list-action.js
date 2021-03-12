import { InventoryService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';

export const SET_INVENTORY_PENDING = "SET_INVENTORY_PENDING";
export const GET_INVENTORY_SUCCESS = "GET_INVENTORY_SUCCESS";

const api_serv = new InventoryService();

export function get_inventories_by_event_id(eventId) {
    return async dispatch => {
        dispatch(setInventoryPending(true));
        let response = await api_serv.getInventoriesByEventId(eventId);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getInventorySuccess(jsonRes));
        return Promise.resolve();
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
