import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/helpers.js';

export const editBirthday = {
    PENDING: "SET_EDITBIRTHDAY_PENDING",
    SUCCESS: "SET_EDITBIRTHDAY_SUCCESS",
    UPDATE: "UPDATE_BIRTHDAY"
}

const api_serv = new UserService();

export default function edit_Birthday(data) {
    return async dispatch => {
        dispatch(setEditBirthdayPending(true));
        let response = await api_serv.setBirthday(data);
        if (!response.ok) {
            throw new SubmissionError(buildValidationState(response));
        }
        dispatch(setEditBirthdaySuccess(true));
        dispatch(updateBirthday(data.Birthday));
        dispatch(setSuccessAllert('Set date of birth successedd'));
        return Promise.resolve();
    }
}

function updateBirthday(data) {
    return {
        type: editBirthday.UPDATE,
        payload: data
    };
}

function setEditBirthdayPending(data) {
    return {
        type: editBirthday.PENDING,
        payload: data
    };
}

function setEditBirthdaySuccess(data) {
    return {
        type: editBirthday.SUCCESS,
        payload: data
    };
}
