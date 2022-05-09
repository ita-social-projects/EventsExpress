import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";
import { UserService } from '../../services'

export const editLocation = {
    UPDATE: "UPDATE_LOCATION"
}

const api_serv = new UserService();

export default function edit_Location(data){
    return async dispatch =>{
        dispatch(getRequestInc());
        let response = await api_serv.setLocation(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(updateLocation(data));
        dispatch(setSuccessAllert('Location is changed'));
        return Promise.resolve();
    }
}

 function updateLocation(data) {
    return {
        type: editLocation.UPDATE,
        payload: data
    };
}
