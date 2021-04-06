
import { CategoryService } from '../../services';
import { setErrorAllertFromResponse } from './../alert-action';

export const SET_CATEGORIES_PENDING = "SET_CATEGORIES_PENDING";
export const GET_CATEGORIES_SUCCESS = "GET_CATEGORIES_SUCCESS";

const api_serv = new CategoryService();

export default function get_categories() {

    return async dispatch => {
        dispatch(setCategoryPending(true));
        let response = await api_serv.getAllCategories();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getCategories(jsonRes));
        return Promise.resolve();
    }
}

function setCategoryPending(data) {
    return {
        type: SET_CATEGORIES_PENDING,
        payload: data
    }
}

function getCategories(data) {
    return {
        type: GET_CATEGORIES_SUCCESS,
        payload: data
    }
}
