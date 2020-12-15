import { CategoryService } from '../services';
import get_categories from './category-list';

export const SET_CATEGORY_DELETE_PENDING = "SET_CATEGORY_DELETE_PENDING";
export const SET_CATEGORY_DELETE_SUCCESS = "SET_CATEGORY_DELETE_SUCCESS";
export const SET_CATEGORY_DELETE_ERROR = "SET_CATEGORY_DELETE_ERROR";

const api_serv = new CategoryService();

export function delete_category(data) {

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

export function setCategoryDeleteError(data) {
    return {
        type: SET_CATEGORY_DELETE_ERROR,
        payload: data
    };
}

