import { ContactAdminService } from "../../services";
import { setSuccessAllert } from './../alert-action';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";


export const contactAdmin = {
    DATA: "SET_CONTACT_ADMIN_DATA",
}

const api_serv = new ContactAdminService();

export default function contact_Admin(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setContactAdmin(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(setSuccessAllert('Message was succesfully sended'));
        dispatch(reset('ContactAdmin'));
        return Promise.resolve();
    }
}