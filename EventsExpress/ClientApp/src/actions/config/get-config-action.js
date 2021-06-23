import { ConfigService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_CONFIG_SUCCESS = "GET_CONFIG_SUCCESS";

const api_serv = new ConfigService();

export default function getConfig() {
    return async dispatch => {
        let response = await api_serv.getConfigFromBack();

        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }

        let config = await response.json();
        dispatch(getConfigSuccess(config));

        return Promise.resolve();
    }
}

export function getConfigSuccess(data) {
    return {
        type: GET_CONFIG_SUCCESS,
        payload: data
    };
}
