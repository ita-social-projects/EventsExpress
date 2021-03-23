import { CategoryService } from '../../services';
import get_categories from './category-list-action';
import { setErrorAllertFromResponse } from './../alert-action';

export const SET_CATEGORY_DELETE_PENDING = "SET_CATEGORY_DELETE_PENDING";
export const SET_CATEGORY_DELETE_SUCCESS = "SET_CATEGORY_DELETE_SUCCESS";

const api_serv = new CategoryService();

export function delete_category(data) {

    return async dispatch => {
        dispatch(setCategoryDeletePending(true));

        let response = await api_serv.setCategoryDelete(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setCategoryDeleteSuccess(true));
        dispatch(get_categories());
        return Promise.resolve();
    }
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


