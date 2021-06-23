import { ConfigService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CONFIGS_DATA = "GET_CONFIGS_DATA";

const api_serv = new ConfigService();

export default function getConfigs() {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getConfigsFromBack();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let configs = await response.json();
        dispatch(getConfigSuccess(configs));
        dispatch(getRequestDec());
        return Promise.resolve();
    }
}

export function getConfigSuccess(data) {
    return {
        type: GET_CONFIGS_DATA,
        payload: data
    };
}