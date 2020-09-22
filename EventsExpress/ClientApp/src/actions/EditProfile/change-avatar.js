import EventsExpressService from '../../services/EventsExpressService';
import { SetAlert } from '../alert';

export const changeAvatar = {
    PENDING: "SET_CHANGE_AVATAR_PENDING",
    SUCCESS: "SET_CHANGE_AVATAR_SUCCESS",
    ERROR: "SET_CHANGE_AVATAR_ERROR",
    UPDATE: "UPDATE_CHANGE_AVATAR"
}

const api_serv = new EventsExpressService();

export default function change_avatar(data) {
    return dispatch => {
        dispatch(setAvatarPending(true));
        const res = api_serv.setAvatar(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setAvatarSuccess(true));
                dispatch(updateAvatar(response));
                dispatch(SetAlert({ variant: 'success', message: 'Avatar is update' }));
            } else {
                dispatch(setAvatarError(response.error));
                dispatch(SetAlert({ variant: 'error', message: 'Failed' }));
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
