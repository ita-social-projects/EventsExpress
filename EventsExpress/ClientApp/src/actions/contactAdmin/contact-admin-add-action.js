import { ContactAdminService } from "../../services";
import { setSuccessAllert } from './../alert-action';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const contactAdmin = {
    PENDING: "SET_CONTACT_ADMIN_PENDING",
    SUCCESS: "SET_CONTACT_ADMIN_SUCCESS",
}

const api_serv = new ContactAdminService();

export default function contact_Admin(data) {
    return async dispatch => {
        dispatch(setContactAdminPending(true));
        let response = await api_serv.setContactAdmin(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setContactAdminSuccess(true));
        dispatch(setSuccessAllert('Message was succesfully sended'));
        dispatch(reset('ContactAdmin'));
        return Promise.resolve();
    }
}

function setContactAdminPending(data) {
    return {
        type: contactAdmin.PENDING,
        payload: data
    };
}

function setContactAdminSuccess(data) {
    return {
        type: contactAdmin.SUCCESS,
        payload: data
    };
}
