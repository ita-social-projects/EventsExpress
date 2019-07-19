import EventsExpressService from '../services/EventsExpressService';

export const editBirthday = {
    PENDING = "SET_EDITBIRTHDAY_PENDING",
    SUCCESS = "SET_EDITBIRTHDAY_SUCCESS",
    ERROR = "SET_EDITBIRTHDAY_ERROR"
}

const api_serv = new EventsExpressService();

export default function editBirthday(data) {
    return dispatch => {
        dispatch(setEditBirthday(true));


        /* місце для апі
        const res = api_serv.setEvent(data);*/
        res.then(response => {
            if (response.error == null) {

                dispatch(setEditBirthdaySuccess(true));

            } else {
                dispatch(setEditBirthdayError(response.error));
            }
        });
    
}

function setEditBirthday(data) {
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

function setEditBirthdayError(data) {
    return {
        type: editBirthday.ERROR,
        payload: data
    };
}

