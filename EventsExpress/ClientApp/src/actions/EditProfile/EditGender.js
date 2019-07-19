import EventsExpressService from '../services/EventsExpressService';

export const editGender = {
    PENDING = "SET_EDITGENDER_PENDING",
    SUCCESS = "SET_EDITGENDER_SUCCESS",
    ERROR = "SET_EDITGENDER_ERROR"
}

const api_serv = new EventsExpressService();


export default function editUsername(data) {


    return dispatch => {
        dispatch(setEditGenderPending(true));



        /* місце для апі
        const res = api_serv.setEvent(data);*/;
        res.then(response => {
            if (response.error == null) {

                dispatch(setEditGenderSuccess(true));

            } else {
                dispatch(setEditGenderError(response.error));
            }
        });
    }
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

    function setEditGenderError(data) {
        return {
            type: editGender.ERROR,
            payload: data
        };
    }
