import { UserService } from '../../services';
import { setSuccessAllert} from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/helpers.js'

export const changeAvatar = {
    PENDING: "SET_CHANGE_AVATAR_PENDING",
    SUCCESS: "SET_CHANGE_AVATAR_SUCCESS",
    UPDATE: "UPDATE_CHANGE_AVATAR"
}

const api_serv = new UserService();

export default function change_avatar(data) {
    return async dispatch => {
        dispatch(setAvatarPending(true));

        let response = await api_serv.setAvatar(data);
        if (!response.ok) {
            throw new SubmissionError(buildValidationState(response));
        }
        dispatch(setAvatarSuccess(true));
        dispatch(updateAvatar(response));
        dispatch(setSuccessAllert('Avatar is update'));
        return Promise.resolve();
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

