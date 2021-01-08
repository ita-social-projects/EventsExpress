import InventoryService from '../services/InventoryService';
import { get_inventories_by_event_id } from './inventory-list';

export const GET_USERSINVENTORIES_SUCCESS = 'GET_USERSINVENTORIES_SUCCESS';
export const SET_USERSINVENTORIES_PENDING = 'SET_USERSINVENTORIES_PENDING';
export const SET_USERSINVENTORIES_ERROR = 'SET_USERSINVENTORIES_ERROR';

const api_serv = new InventoryService();

export function get_users_inventories_by_event_id(eventId) {
    return dispatch => {
        dispatch(setUsersInventoriesPending(true));
        api_serv.getUsersInventories(eventId)
            .then(response => {
                if (response.error) {
                    dispatch(setUsersInventoriesError(response.error));
                } else {
                    dispatch(getUsersInventoriesSuccess(response));
                }
                dispatch(get_inventories_by_event_id(eventId));
            });
    }
}

export function delete_users_inventory(data) {
    return dispatch => {
        dispatch(setUsersInventoriesPending(true));
        api_serv.setUsersInventoryDelete(data)
            .then(response => {
                if (response.error) {
                    dispatch(setUsersInventoriesError(response.error));
                } 
                dispatch(get_users_inventories_by_event_id(data.eventId));
            });
    }
}

export function edit_users_inventory(data) {
    return dispatch => {
        dispatch(setUsersInventoriesPending(true));
        api_serv.setUsersInventory(data)
            .then(response => {
                if (response.error == null) {
                    dispatch(setUsersInventoriesError(response.error));
                } else {
                    dispatch(getUsersInventoriesSuccess(response));
                }
                dispatch(get_users_inventories_by_event_id(data.eventId));
            });
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

export function setUsersInventoriesError(data) {
    return {
        type: SET_USERSINVENTORIES_ERROR,
        payload: data
    }
}