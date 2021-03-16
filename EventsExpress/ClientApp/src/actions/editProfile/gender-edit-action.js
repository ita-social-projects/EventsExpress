import { UserService } from '../../services';
import { setSuccessAllert, setErrorAllertFromResponse } from '../alert-action';

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
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
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
