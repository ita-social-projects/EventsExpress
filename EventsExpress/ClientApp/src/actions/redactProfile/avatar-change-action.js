import { UserService } from '../../services';
import { setSuccessAllert} from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers.js'
import { getRequestInc, getRequestDec } from "../request-count-action";

export const changeAvatar = {
    PENDING: "SET_CHANGE_AVATAR_PENDING",
    SUCCESS: "SET_CHANGE_AVATAR_SUCCESS",
    UPDATE: "UPDATE_CHANGE_AVATAR"
}

const api_serv = new UserService();

export default function change_avatar(data) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.setAvatar(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
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

