import { UserService } from '../../services';
import { setAlert} from '../alert-action';

export const editGender = {
    PENDING : "SET_EDITGENDER_PENDING",
    SUCCESS : "SET_EDITGENDER_SUCCESS",
    ERROR : "SET_EDITGENDER_ERROR",
    UPDATE: "UPDATE_GENDER"
}

const api_serv = new UserService();


export default function edit_Gender(data) {


    return dispatch => {
        dispatch(setEditGenderPending(true));
        const res = api_serv.setGender(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setEditGenderSuccess(true));
                dispatch(updateGender(data));
                dispatch(setAlert({variant:'success', message:'Set gender successed'}));
            } else {
                dispatch(setEditGenderError(response.error));
                dispatch(setAlert({variant:'error', message:'Failed'}));
            }
        });
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

export   function setEditGenderError(data) {
        return {
            type: editGender.ERROR,
            payload: data
        };
    }
