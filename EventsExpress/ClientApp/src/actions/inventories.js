import EventsExpressService from '../services/EventsExpressService';

export const ADD_ITEM_TO_INVENTAR = 'ADD_ITEM_TO_INVENTAR';

export default function addItemToInventar(item) {
    return dispatch => {
        dispatch({
            type: ADD_ITEM_TO_INVENTAR,
            payload: item
        });
    }
}