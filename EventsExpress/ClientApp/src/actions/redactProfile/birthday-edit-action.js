import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";
import moment from 'moment';

export const editBirthday = {
    UPDATE: "UPDATE_BIRTHDAY"
}

const api_serv = new UserService();

export default function edit_Birthday(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let body = {
            ...data,
            birthday: moment.utc(data.birthday).local().format('YYYY-MM-DD[T00:00:00]')
        };
        let response = await api_serv.setBirthday(body);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(updateBirthday(data.birthday));
        dispatch(setSuccessAllert('Date of birth is successfully set'));
        return Promise.resolve();
    }
}

function updateBirthday(data) {
    return {
        type: editBirthday.UPDATE,
        payload: data
    };
}
