import { ConfigService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CONFIG_DATA = "GET_CONFIG_DATA";

const api_serv = new ConfigService();

export default function getConfig() {
    return async dispatch => {
        dispatch(getRequestInc())
        let response = await api_serv.getConfigFromBack();
        dispatch(getRequestDec())
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
        type: GET_CONFIG_DATA,
        payload: data
    };
}
