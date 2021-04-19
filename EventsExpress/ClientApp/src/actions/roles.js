import { RoleService } from '../services';
import { setErrorAllertFromResponse } from './alert-action';

export const getRoles = {
    PENDING: 'ROLES_PENDING',
    SUCCESS: 'ROLES_SUCCESS',
}


const api_serv = new RoleService();


export default function get_roles() {
    return async dispatch => {
        dispatch(setRolesPending(true));
        const response = await api_serv.getRoles();

        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        
        let jsonRes = await response.json();
        dispatch(setRolesSuccess(jsonRes));
        return Promise.resolve();

    }
}

function setRolesPending(data) {
    return {
        type: getRoles.PENDING,
        payload: data
    }
}

function setRolesSuccess(data) {
    return {
        type: getRoles.SUCCESS,
        payload: data
    }
}
