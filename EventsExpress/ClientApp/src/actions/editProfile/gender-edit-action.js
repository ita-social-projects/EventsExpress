import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/helpers.js'

export const editGender = {
    PENDING: "SET_EDITGENDER_PENDING",
    SUCCESS: "SET_EDITGENDER_SUCCESS",
    UPDATE: "UPDATE_GENDER"
}

const api_serv = new UserService();

export default function edit_Gender(data) {
    return async dispatch => {
        dispatch(setEditGenderPending(true));
        let response = await api_serv.setGender(data);
        if (!response.ok) {
            throw new SubmissionError(buildValidationState(response));
        }
        dispatch(setEditGenderSuccess(true));
        dispatch(updateGender(data));
        dispatch(setSuccessAllert('Set gender successed'));
        return Promise.resolve();
    }
}

function updateGender(data) {
    return {
        type: editGender.UPDATE,
        payload: data
    };
}

function setEditGenderPending(data) {
    return {
        type: editGender.PENDING,
        payload: data
    };
}

function setEditGenderSuccess(data) {
    return {
        type: editGender.SUCCESS,
        payload: data
    };
}
