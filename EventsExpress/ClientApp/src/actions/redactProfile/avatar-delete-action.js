import { PhotoService } from '../../services';
import { setSuccessAllert} from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers.js'
import { getRequestInc, getRequestDec } from "../request-count-action";

export const deleteAvatar = {
    PENDING: "SET_DELETE_AVATAR_PENDING",
    SUCCESS: "SET_DELETE_AVATAR_SUCCESS",
    UPDATE: "UPDATE_DELETE_AVATAR"
}

const api_serv = new PhotoService();

export default function delete_avatar(data) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.deleteUserPhoto(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(updateAvatar());
        dispatch(setSuccessAllert('Avatar is successfully deleted'));
        return Promise.resolve();
    }
}



export function updateAvatar() {
    return {
        type: deleteAvatar.UPDATE,
    };
}

