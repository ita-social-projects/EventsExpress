import { UserService } from '../../services';
import { setSuccessAllert, setErrorAllertFromResponse } from '../alert-action';

export const editUsername = {
    PENDING : "SET_EDITUSERNAME_PENDING",
    SUCCESS : "SET_EDITUSERNAME_SUCCESS",
    UPDATE: "UPDATE_USERNAME"
}

const api_serv = new UserService();

export default function edit_Username(data) {
    return async dispatch => {
        dispatch(setEditUsernamePending(true));
        let response = await api_serv.setUsername(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setEditUsernameSuccess(true));
        dispatch(updateUsername(data));
        dispatch(setSuccessAllert('Username is changed'));
        return Promise.resolve();
    }
}

function updateUsername(data) {
    return {
        type: editUsername.UPDATE,
        payload: data
    };
}

function setEditUsernamePending(data) {
    return {
        type: editUsername.PENDING,
        payload: data
    };
}

function setEditUsernameSuccess(data) {
    return {
        type: editUsername.SUCCESS,
        payload: data
    };
}

