import { ContactUsService } from "../../services";
import { setSuccessAllert } from './../alert-action';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const contactUs = {
    PENDING: "SET_CONTACTUS_PENDING",
    SUCCESS: "SET_CONTACTUS_SUCCESS",
}

const api_serv = new ContactUsService();

export default function contact_Us(data) {
    return async dispatch => {
        dispatch(setContactUsPending(true));
        let response = await api_serv.setContactUs(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(setContactUsSuccess(true));
        dispatch(setSuccessAllert('Message was succesfully sended'));
        dispatch(reset('ContactUs'));
        return Promise.resolve();
    }
}

function setContactUsPending(data) {
    return {
        type: contactUs.PENDING,
        payload: data
    };
}

function setContactUsSuccess(data) {
    return {
        type: contactUs.SUCCESS,
        payload: data
    };
}


