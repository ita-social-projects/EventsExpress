import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const editFirstname = {
    UPDATE: "UPDATE_FIRSTNAME"
}

const api_serv = new UserService();

export default function edit_Firstname(data) {
    return async dispatch => {
        let response = await api_serv.setFirstname(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(updateFirstname(data));
        dispatch(setSuccessAllert('Firstname is changed'));
        return Promise.resolve();
    }
}

function updateFirstname(data) {
    return {
        type: editFirstname.UPDATE,
        payload: data
    };
}
