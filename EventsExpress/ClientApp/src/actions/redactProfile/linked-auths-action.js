import {  AccountService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_LINKED_AUTHS_SUCCESS = "GET_LINKED_AUTHS_SUCCESS";

const api_serv = new AccountService();

export default function getLinkedAuths() {
    return async dispatch => {
        let response = await api_serv.getLinkedAuths();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        let jsonRes = await response.json();
        dispatch(pushToStateLinkedAuths(jsonRes));
        return Promise.resolve();
    };
}

export function pushToStateLinkedAuths(data) {
    return {
        type: GET_LINKED_AUTHS_SUCCESS,
        payload: data
    }
}
