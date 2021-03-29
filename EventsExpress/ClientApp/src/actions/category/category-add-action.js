import { CategoryService } from '../../services';
import get_categories from './category-list-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const SET_CATEGORY_PENDING = "SET_CATEGORY_PENDING";
export const SET_CATEGORY_SUCCESS = "SET_CATEGORY_SUCCESS";
export const SET_CATEGORY_EDITED = "SET_CATEGORY_EDITED";

const api_serv = new CategoryService();

export default function add_category(data) {
    return async dispatch => {
        dispatch(setCategoryPending(true));
        let response;
        if (!!(data.id)) {
            response = await api_serv.editCategory(data)
        } else {
            response = await api_serv.setCategory(data)
        }
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setCategorySuccess(true));
        dispatch(get_categories());
        return Promise.resolve();
    }
}

export function set_edited_category(id) {
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


