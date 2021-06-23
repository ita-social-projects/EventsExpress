import { RoleService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';
import { getRequestInc, getRequestDec } from "./request-count-action";


export const getRoles = {
    DATA: 'ROLES_SUCCESS',
}


const api_serv = new RoleService();


export default function get_roles() {
    return async dispatch => {
        dispatch(getRequestInc());
        const response = await api_serv.getRoles();
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        
        let jsonRes = await response.json();
        dispatch(setRolesSuccess(jsonRes));
        return Promise.resolve();
    }
}

function setRolesSuccess(data) {
    return {
        type: getRoles.DATA,
        payload: data
    }
}
