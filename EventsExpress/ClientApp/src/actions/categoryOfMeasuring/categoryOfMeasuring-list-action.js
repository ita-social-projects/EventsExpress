import CategoryOfMeasuringService from '../../services/CategoryOfMeasuringService';
import { setErrorAllertFromResponse } from './../alert-action';

export const SET_CATEGORIES_OF_MEASURING_PENDING = "SET_CATEGORIES_OF_MEASURING_PENDING";
export const GET_CATEGORIES_OF_MEASURING_SUCCESS = "GET_CATEGORIES_OF_MEASURING_SUCCESS";

const api_serv = new CategoryOfMeasuringService();

export default function get_categoriesOfMeasuring() {

    return async dispatch => {
        dispatch(setCategoryOfMeasuringPending(true));
        let response = await api_serv.getCategoriesOfMeasuring();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getCategoriesOfMeasuring(jsonRes));
        return Promise.resolve();
    }
}

function setCategoryOfMeasuringPending(data) {
    return {
        type: SET_CATEGORIES_OF_MEASURING_PENDING,
        payload: data
    }
}

function getCategoriesOfMeasuring(data) {
    return {
        type: GET_CATEGORIES_OF_MEASURING_SUCCESS,
        payload: data
    }
}
