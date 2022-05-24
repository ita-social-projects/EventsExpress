import { UserService,PhotoService } from '../../services';
import { setSuccessAllert,setErrorAlert } from '../alert-action';
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
        let response = await userService.setAvatar(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(updateAvatar());
        dispatch(setSuccessAllert('Avatar has been successfully updated'));
        return Promise.resolve();
    }
}

export  function delete_avatar(data) {
    return async dispatch => {
        let response = await photoService.deleteUserPhoto(data);
        if (!response.ok) {
            dispatch(setErrorAlert("Something went wrong"))
        }
        else{
            dispatch(updateAvatar());
            dispatch(setSuccessAllert('Avatar has been successfully deleted'));
        }
        return Promise.resolve();
    }
}




export function updateAvatar() {
    return {
        type: changeAvatar.UPDATE,
    };
}

