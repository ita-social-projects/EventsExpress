import { InventoryService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const SET_INVENTORY_PENDING = "SET_INVENTORY_PENDING";
export const GET_INVENTORY_DATA = "GET_INVENTORY_SUCCESS";

const api_serv = new InventoryService();

export function get_inventories_by_event_id(eventId) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getInventoriesByEventId(eventId);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
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
        type: GET_INVENTORY_DATA,
        payload: data
    }
}
