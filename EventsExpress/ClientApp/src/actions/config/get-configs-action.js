import { ConfigService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_CONFIG_PENDING = "GET_CONFIG_PENDING";
export const GET_CONFIG_SUCCESS = "GET_CONFIG_SUCCESS";

const api_serv = new ConfigService();

export default function getConfigs() {
    return async dispatch => {
        dispatch(getConfigPending(true));
        let response = await api_serv.getConfigsFromBack();

        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        let config = await response.json();
        dispatch(getConfigSuccess(true,config));

        return Promise.resolve();
    }
}

export function getConfigPending(isConfigsPending) {
    return {
        type: GET_CONFIG_PENDING,
        isConfigsPending
    };
}

export function getConfigSuccess(isConfigsSuccess,data) {
    return {
        type: GET_CONFIG_SUCCESS,
        isConfigsSuccess,
        payload:data
    };
}
