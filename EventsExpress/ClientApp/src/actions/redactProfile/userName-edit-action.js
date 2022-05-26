import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const editUsername = {
    UPDATE: "UPDATE_USERNAME"
}

const api_serv = new UserService();

export default function edit_Username(data) {
    return async dispatch => {
        let response = await api_serv.setUsername(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
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
