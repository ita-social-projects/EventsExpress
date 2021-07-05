
import { CategoryService } from '../../services';
import { setErrorAllertFromResponse } from './../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CATEGORIES_DATA = "GET_CATEGORIES_DATA";

const api_serv = new CategoryService();

export default function get_categories() {

    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getAllCategories();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getCategories(jsonRes));
        return Promise.resolve();
    }
}

export function getCategories(data) {
    return {
        type: GET_CATEGORIES_DATA,
        payload: data
    }
}
