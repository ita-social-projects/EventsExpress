import { CategoryService } from '../services';
import get_categories from './category-list';


export const SET_CATEGORY_PENDING = "SET_CATEGORY_PENDING";
export const SET_CATEGORY_SUCCESS = "SET_CATEGORY_SUCCESS";
export const SET_CATEGORY_ERROR = "SET_CATEGORY_ERROR";
export const SET_CATEGORY_EDITED = "SET_CATEGORY_EDITED";

const api_serv = new CategoryService();


export function add_category(data) {
    return dispatch => {
        dispatch(setCategoryPending(true));

        let res;
        if  (data.Id) {
            res = api_serv.editCategory(data);
        } else {
            res = api_serv.setCategory(data);
        }

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
    return dispatch => {
        dispatch(setCategoryError(null));
        dispatch(setCategoryEdited(id));     
    }
}

function setCategoryEdited(data) {
    return {
        type: SET_CATEGORY_EDITED,
        payload: data
    };
}

export function setCategoryPending(data) {
    return {
        type: SET_CATEGORY_PENDING,
        payload: data
    };
}

export function setCategorySuccess(data) {
    return {
        type: SET_CATEGORY_SUCCESS,
        payload: data
    };
}

export function setCategoryError(data) {
    return {
        type: SET_CATEGORY_ERROR,
        payload: data
    };
}

