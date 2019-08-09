import EventsExpressService from '../services/EventsExpressService';
import get_categories from './category-list';

import { setCategoryPending, setCategoryError, setCategorySuccess } from './add-category'

export const SET_CATEGORY_DELETE_PENDING = "SET_CATEGORY_DELETE_PENDING";
export const SET_CATEGORY_DELETE_SUCCESS = "SET_CATEGORY_DELETE_SUCCESS";
export const SET_CATEGORY_DELETE_ERROR = "SET_CATEGORY_DELETE_ERROR";
export const SET_CATEGORY_EDITED = "SET_CATEGORY_EDITED";




export function delete_category(data) {
    const api_serv = new EventsExpressService();

    return dispatch => {
        dispatch(setCategoryDeletePending(true));

        const res = api_serv.setCategoryDelete(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setCategoryDeleteSuccess(true));
                dispatch(get_categories());
            } else {
                dispatch(setCategoryDeleteError(response.error));
            }
        });
    }
}

export function set_edited_category(id) {
    return dispatch => {
        dispatch(setCategoryEdited(id));
        dispatch(setCategoryPending(false));
        dispatch(setCategoryError(null));
        dispatch(setCategorySuccess(false));
    }
}

function setCategoryEdited(data) {
    return {
        type: SET_CATEGORY_EDITED,
        payload: data
    };
}

function setCategoryDeleteSuccess(data) {
    return {
        type: SET_CATEGORY_DELETE_SUCCESS,
        payload: data
    };
}

function setCategoryDeletePending(data) {
    return {
        type: SET_CATEGORY_DELETE_PENDING,
        payload: data
    };
}

function setCategoryDeleteError(data) {
    return {
        type: SET_CATEGORY_DELETE_ERROR,
        payload: data
    };
}

