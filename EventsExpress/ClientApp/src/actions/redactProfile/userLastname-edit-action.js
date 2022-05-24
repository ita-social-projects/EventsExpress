import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const editLastname = {
    UPDATE: "UPDATE_LASTNAME"
}

const api_serv = new UserService();

export default function edit_Lastname(data) {
    return async dispatch => {
        let response = await api_serv.setLastname(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(updateLastname(data));
        dispatch(setSuccessAllert('Lastname is changed'));
        return Promise.resolve();
    }
}

function updateLastname(data) {
    return {
        type: editLastname.UPDATE,
        payload: data
    };
}
