import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';


export const editGender = {
    UPDATE: "UPDATE_GENDER"
}

const api_serv = new UserService();

export default function edit_Gender(data) {
    return async dispatch => {
        let response = await api_serv.setGender(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(updateGender(data));
        dispatch(setSuccessAllert('Gender is successfully set'));
        return Promise.resolve();
    }
}

function updateGender(data) {
    return {
        type: editGender.UPDATE,
        payload: data
    };
}
