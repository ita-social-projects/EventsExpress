import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

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
            throw new SubmissionError(await buildValidationState(response));
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

