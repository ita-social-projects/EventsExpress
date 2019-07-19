import EventsExpressService from '../services/EventsExpressService';

export const editUserName = {
    PENDING = "SET_EDITUSERNAME_PENDING",
    SUCCESS = "SET_EDITUSERNAME_SUCCESS",
    ERROR = "SET_EDITUSERNAME_ERROR"

}

const api_serv = new EventsExpressService();

export default function editUsername(data) {
   

    return dispatch => {
        dispatch(setEditUsernamePending(true));



        /* місце для апі
        const res = api_serv.setEvent(data);*/;
        res.then(response => {
            if (response.error == null) {

                dispatch(setEditUsernameSuccess(true));

            } else {
                dispatch(setEditUsernameError(response.error));
            }
        });
    }
}

function setEditUsernamePending(data) {
    return {
        type: editUserName.PENDING,
        payload: data
    };
}

function setEditUsernameSuccess(data) {
    return {
        type: editUserName.SUCCESS,
        payload: data
    };
}

function setEditUsernameError(data) {
    return {
        type: editUserName.ERROR,
        payload: data
    };
}

