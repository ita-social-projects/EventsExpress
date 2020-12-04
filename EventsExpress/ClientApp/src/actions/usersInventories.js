import EventsExpressService from '../services/EventsExpressService';

const api_serv = new EventsExpressService();

export function get_users_inventories_by_event_id(eventId) {
    return dispatch => {
        const res = api_serv.getUsersInventories(eventId);
        res.then(response => {
            console.log('action userInventories', response);
            if (response.error == null) {
                dispatch(getUsersInventoriesSuccess(response));
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