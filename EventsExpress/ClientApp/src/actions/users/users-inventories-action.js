import InventoryService from '../../services/InventoryService';
import { get_inventories_by_event_id } from '../inventory/inventory-list-action';
import { setErrorAllertFromResponse } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { getRequestInc, getRequestDec } from "../request-count-action";
import { buildValidationState } from '../../components/helpers/action-helpers';

export const GET_USERSINVENTORIES_DATA = 'GET_USERSINVENTORIES_SUCCESS';

const api_serv = new InventoryService();

export function get_users_inventories_by_event_id(eventId) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.getUsersInventories(eventId);
        dispatch(get_inventories_by_event_id(eventId));
        dispatch(getRequestDec());
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
        dispatch(getRequestInc());

        let response = await api_serv.setUsersInventoryDelete(data);
        dispatch(get_users_inventories_by_event_id(data.eventId));
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }        
        return Promise.resolve();
    }
}

export function edit_users_inventory(data) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.setUsersInventory(data);
        dispatch(get_users_inventories_by_event_id(data.eventId));
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        let jsonRes = await response.json();
        dispatch(getUsersInventoriesSuccess(jsonRes));
        dispatch(getRequestDec());
        return Promise.resolve();
    }
}

export function getUsersInventoriesSuccess(data) {
    return {
        type: GET_USERSINVENTORIES_DATA,
        payload: data
    }
}
