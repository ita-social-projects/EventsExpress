import EventExpressService from "../services/EventsExpressService";
import {setAlert} from './alert';
import { reset} from 'redux-form';

export const contactUs={
    PENDING : "SET_CONTACTUS_PENDING",
    SUCCESS : "SET_CONTACTUS_SUCCESS",
    ERROR : "SET_CONTACTUS_ERROR",
}

const api_serv=new EventExpressService();

export default function contact_Us(data){
    return dispatch=>{
        
        dispatch(setContactUsPending(true));
        const res=api_serv.setContactUs(data);

        res.then(response=>{
            if(response.error == null){
                dispatch(setContactUsSuccess(true));
                dispatch(setAlert({variant:'success', message:'Message was succesfully sended'}));
                dispatch(reset('ContactUs'));
            }else{
                dispatch(setContactUsError(response.error));
                dispatch(setAlert({variant:'error', message:'Failed'}));
            }
        }

        );
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

export function setContactUsError(data) {
    return {
        type: contactUs.ERROR,
        payload: data
    };
}
