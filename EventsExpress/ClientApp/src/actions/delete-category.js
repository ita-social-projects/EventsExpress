import EventsExpressService from '../services/EventsExpressService';
import get_categories from './category-list';

export const SET_CATEGORY_DELETE_PENDING = "SET_CATEGORY_DELETE_PENDING";
export const SET_CATEGORY_DELETE_SUCCESS = "SET_CATEGORY_DELETE_SUCCESS";
export const SET_CATEGORY_DELETE_ERROR = "SET_CATEGORY_DELETE_ERROR";

export default function delete_category(data) {

    const api_serv = new EventsExpressService();

    return dispatch => {
        dispatch(setCategoryPending(true));

        const res = api_serv.setCategoryDelete(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setCategorySuccess(true));
                dispatch(get_categories());
            } else {
                dispatch(setCategoryError(response.error));
            }
        });
    }
}


function setCategorySuccess(data) {
    return {
        type: SET_CATEGORY_DELETE_SUCCESS,
        payload: data
    };
}

function setCategoryPending(data) {
    return {
        type: SET_CATEGORY_DELETE_PENDING,
        payload: data
    };
}

export function setCategoryError(data) {
    return {
        type: SET_CATEGORY_DELETE_ERROR,
        payload: data
    };
}

