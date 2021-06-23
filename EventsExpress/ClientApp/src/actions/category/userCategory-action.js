import { CategoryService } from '../../services';
import { setErrorAllertFromResponse } from './../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_USER_CATEGORIES_DATA = "GET_USER_CATEGORIES_DATA";

const api_serv = new CategoryService();

export default function get_userCategories() {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getUserCategories();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getUserCategories(jsonRes));
        return Promise.resolve();
    }
}

export function getUserCategories(data) {
    return {
        type: GET_USER_CATEGORIES_DATA,
        payload: data
    }
}