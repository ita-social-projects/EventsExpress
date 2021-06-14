import { CategoryService } from '../../services';
import get_categories from './category-list-action';
import { setErrorAllertFromResponse } from './../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";


const api_serv = new CategoryService();

export function delete_category(data) {

    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.setCategoryDelete(data);
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(get_categories());
        return Promise.resolve();
    }
}


