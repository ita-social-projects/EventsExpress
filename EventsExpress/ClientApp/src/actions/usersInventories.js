import InventoryService from '../services/InventoryService';

const api_serv = new InventoryService();

export function get_users_inventories_by_event_id(eventId) {
    return dispatch => {
        const res = api_serv.getUsersInventories(eventId);
        res.then(response => {
            if (response.error == null) {
                dispatch(getUsersInventoriesSuccess(response));
            } else {
            }
        });
    }
}

export function delete_users_inventory(data) {
    return dispatch => {
        api_serv.setUsersInventoryDelete(data)
        .then(response => {
            if (response.error == null) {
                dispatch(get_users_inventories_by_event_id(data.eventId));
            } else {

            }
        });
    }
}

export function edit_users_inventory(data) {
    return dispatch => {
        api_serv.setUsersInventory(data)
        .then(response => {
            console.log('action', response);
            if (response.error == null) {
                dispatch(get_users_inventories_by_event_id(data.eventId));
            } else {

            }
        });
    }
}

export function getUsersInventoriesSuccess(data) {
    return {
        type: "GET_USERSINVENTORIES_SUCCESS",
        payload: data
    }
}