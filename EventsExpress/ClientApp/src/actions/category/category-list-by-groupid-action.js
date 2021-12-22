import { CategoryService } from '../../services';
import { setErrorAllertFromResponse } from './../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CATEGORIES_BY_GROUP_ID = "GET_CATEGORIES_BY_GROUP_ID";

const api_serv = new CategoryService();

export default function get_categories_by_groupId(id) {

    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getCategoriesByGroup(id);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getRequestDec());
        dispatch(getCategoriesByGroupId(jsonRes));
        return Promise.resolve();
    }

}

export function getCategoriesByGroupId(data) {
    return {
        type: GET_CATEGORIES_BY_GROUP_ID,
        payload: data
    }
}