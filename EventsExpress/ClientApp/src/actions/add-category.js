import EventsExpressService from '../services/EventsExpressService';
import get_categories from './category-list';


export const SET_CATEGORY_PENDING = "SET_CATEGORY_PENDING";
export const SET_CATEGORY_SUCCESS = "SET_CATEGORY_SUCCESS";
export const SET_CATEGORY_ERROR = "SET_CATEGORY_ERROR";

const api_serv = new EventsExpressService();


export function add_category(data) {
    return dispatch => {
        dispatch(setCategoryPending(true));

        const res = api_serv.setCategory(data);
        
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

