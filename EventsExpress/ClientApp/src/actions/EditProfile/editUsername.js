import { UserService } from '../../services';
import { setAlert} from '../alert-action';

export const editUsername = {
    PENDING : "SET_EDITUSERNAME_PENDING",
    SUCCESS : "SET_EDITUSERNAME_SUCCESS",
    ERROR : "SET_EDITUSERNAME_ERROR",
    UPDATE: "UPDATE_USERNAME"

}

const api_serv = new UserService();

export default function edit_Username(data) {
   

    return dispatch => {
        dispatch(setEditUsernamePending(true));
        const res = api_serv.setUsername(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setEditUsernameSuccess(true));
                dispatch(updateUsername(data));
                dispatch(setAlert({variant:'success', message:'Username is changed'}));
            } else {
                dispatch(setEditUsernameError(response.error));
                dispatch(setAlert({variant:'error', message:'Failed'}));
            }
        });
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

export function setEditUsernameError(data) {
    return {
        type: editUsername.ERROR,
        payload: data
    };
}

