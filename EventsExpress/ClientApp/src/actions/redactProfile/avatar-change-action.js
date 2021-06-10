import { UserService } from '../../services';
import { setSuccessAllert} from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers.js'

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
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setAvatarSuccess(true));
        dispatch(updateAvatar());
        dispatch(setSuccessAllert('Avatar is successfully updated'));
        return Promise.resolve();
    }
}



export function updateAvatar() {
    return {
        type: changeAvatar.UPDATE,
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

