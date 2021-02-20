import { SubmissionError } from 'redux-form';
import { UserService } from '../../services';
import { setAlert } from '../alert-action';
import { buildValidationState } from '../../components/helpers/helpers.js'

export const changeAvatar = {
    PENDING: "SET_CHANGE_AVATAR_PENDING",
    SUCCESS: "SET_CHANGE_AVATAR_SUCCESS",
    ERROR: "SET_CHANGE_AVATAR_ERROR",
    UPDATE: "UPDATE_CHANGE_AVATAR"
}

const api_serv = new UserService();

export default function change_avatar(data) {
    return dispatch => {
        dispatch(setAvatarPending(true));
        return api_serv.setAvatar(data).then(response => {
            if (response.error == null) {
                dispatch(setAvatarSuccess(true));
                dispatch(updateAvatar(response));
                dispatch(setAlert({ variant: 'success', message: 'Avatar is update' }));
                return Promise.resolve('success');
            } else {
                dispatch(setAlert({ variant: 'error', message: 'Failed' }));
                throw new SubmissionError(buildValidationState(response.error));
            }
        });
    }
}

function updateAvatar(data) {
    return {
        type: changeAvatar.UPDATE,
        payload: data
    };
}

function setAvatarPending(data) {
    return {
        type: changeAvatar.PENDING,
        payload: data
    };
}

function setAvatarSuccess(data) {
    return {
        type: changeAvatar.SUCCESS,
        payload: data
    };
}

export function setAvatarError(data) {
    return {
        type: changeAvatar.ERROR,
        payload: data
    };
}
