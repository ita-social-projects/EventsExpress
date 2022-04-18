import { UserService,PhotoService } from '../../services';
import { setSuccessAllert} from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers.js'
import { getRequestInc, getRequestDec } from "../request-count-action";

export const changeAvatar = {
    PENDING: "SET_CHANGE_AVATAR_PENDING",
    SUCCESS: "SET_CHANGE_AVATAR_SUCCESS",
    UPDATE: "UPDATE_CHANGE_AVATAR"
}

const userService = new UserService();
const photoService = new PhotoService();

export default function change_avatar(data) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await userService.setAvatar(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(updateAvatar());
        dispatch(setSuccessAllert('Avatar is successfully updated'));
        return Promise.resolve();
    }
}

export  function delete_avatar(data) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await photoService.deleteUserPhoto(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(setSuccessAllert('Avatar is successfully deleted'));
        return Promise.resolve();
    }
}




export function updateAvatar() {
    return {
        type: changeAvatar.UPDATE,
    };
}

