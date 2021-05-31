import { ConfigService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_CONFIGS_PENDING = "GET_CONFIGS_PENDING";
export const GET_CONFIGS_SUCCESS = "GET_CONFIGS_SUCCESS";

const api_serv = new ConfigService();

export default function getConfigs() {
    return async dispatch => {
        dispatch(getConfigsPending(true));
        let response = await api_serv.getConfigsFromBack();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let configs = await response.json();
        
        dispatch(getConfigSuccess(true,configs));
        return Promise.resolve();
    }
}

export function getConfigsPending(isConfigsPending) {
    return {
        type: GET_CONFIGS_PENDING,
        isConfigsPending
    };
}
export function getConfigSuccess(isConfigsSuccess,data) {
    return {
        type: GET_CONFIGS_SUCCESS,
        isConfigsSuccess,
        payload:data
    };
}