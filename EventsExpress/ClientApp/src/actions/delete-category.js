import EventsExpressService from '../services/EventsExpressService';
import get_categories from './category-list';

export const SET_CATEGORY_DELETE_PENDING = "SET_CATEGORY_DELETE_PENDING";
export const SET_CATEGORY_DELETE_SUCCESS = "SET_CATEGORY_DELETE_SUCCESS";
export const SET_CATEGORY_DELETE_ERROR = "SET_CATEGORY_DELETE_ERROR";
export const SET_CATEGORY_EDITED = "SET_CATEGORY_EDITED";


export function delete_category(data) {
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

export function set_edited_category(id) {
    console.log("action set_edited_category");
    console.log(id)

    return dispatch => {

        dispatch(setCategoryEdited(id));
    }
}

function setCategoryEdited(data) {
    return {
        type: SET_CATEGORY_EDITED,
        payload: data
    };
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

function setCategoryError(data) {
    return {
        type: SET_CATEGORY_DELETE_ERROR,
        payload: data
    };
}

