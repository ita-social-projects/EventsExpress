import { CategoryGroupService } from "../../services";
import { setErrorAllertFromResponse } from "./../alert-action";
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CATEGORY_GROUPS_DATA = "GET_CATEGORY_GROUPS_DATA";

const api_serv = new CategoryGroupService();

export default function get_category_groups() {
  return async (dispatch) => {
    dispatch(getRequestInc());
    let response = await api_serv.getAllCategoryGroups();
    if (!response.ok) {
      dispatch(setErrorAllertFromResponse(response));
      return Promise.reject();
    }
    let jsonRes = await response.json();
    dispatch(getRequestDec());
    dispatch(getCategoryGroups(jsonRes));
    return Promise.resolve();
  };
}

export function getCategoryGroups(data) {
  return {
    type: GET_CATEGORY_GROUPS_DATA,
    payload: data,
  };
}
