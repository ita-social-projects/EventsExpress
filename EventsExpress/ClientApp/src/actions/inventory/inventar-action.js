import { InventoryService } from '../../services';
import { get_inventories_by_event_id } from './inventory-list-action';
import { get_users_inventories_by_event_id } from '../users/users-inventories-action';
import { setErrorAllertFromResponse } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

const api_serv = new InventoryService();

export function add_item(item, eventId) {
    return async dispatch => {
        let response = await api_serv.setItemToInventory(item, eventId);
        dispatch(get_inventories_by_event_id(eventId));
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }         
        return Promise.resolve();
    }
}

export function delete_item(itemId, eventId) {
    return async dispatch => {
        let response = await api_serv.setItemDelete(itemId, eventId);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(get_inventories_by_event_id(eventId));
        return Promise.resolve();
    }
}

export function edit_item(item, eventId) {
    return async dispatch => {
        let response = await api_serv.setItem(item, eventId);
        dispatch(get_inventories_by_event_id(eventId));
        dispatch(get_users_inventories_by_event_id(eventId));
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        return Promise.resolve();
    }
}

export function want_to_take(data) {
    return async dispatch => {
        let response = await api_serv.setWantToTake(data);
        if (response.ok) {
            dispatch(get_users_inventories_by_event_id(data.eventId));
            dispatch(get_inventories_by_event_id(data.eventId));
            return Promise.resolve();
        }
    }
}
