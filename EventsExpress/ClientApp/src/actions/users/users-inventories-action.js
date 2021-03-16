import InventoryService from '../../services/InventoryService';
import { get_inventories_by_event_id } from '../inventory/inventory-list-action';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_USERSINVENTORIES_SUCCESS = 'GET_USERSINVENTORIES_SUCCESS';
export const SET_USERSINVENTORIES_PENDING = 'SET_USERSINVENTORIES_PENDING';

const api_serv = new InventoryService();

export function get_users_inventories_by_event_id(eventId) {
    return async dispatch => {
        dispatch(setUsersInventoriesPending(true));

        let response = await api_serv.getUsersInventories(eventId);
        dispatch(get_inventories_by_event_id(eventId));
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getUsersInventoriesSuccess(jsonRes));
        return Promise.resolve();
    }
}

export function delete_users_inventory(data) {
    return async dispatch => {
        dispatch(setUsersInventoriesPending(true));

        let response = await api_serv.setUsersInventoryDelete(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(get_users_inventories_by_event_id(data.eventId));
        return Promise.resolve();
    }
}

export function edit_users_inventory(data) {
    return async dispatch => {
        dispatch(setUsersInventoriesPending(true));

        let response = await api_serv.setUsersInventory(data);
        dispatch(get_users_inventories_by_event_id(data.eventId));
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getUsersInventoriesSuccess(jsonRes));
        return Promise.resolve();
    }
}

export function getUsersInventoriesSuccess(data) {
    return {
        type: GET_USERSINVENTORIES_SUCCESS,
        payload: data
    }
}

export function setUsersInventoriesPending(data) {
    return {
        type: SET_USERSINVENTORIES_PENDING,
        payload: data
    }
}

