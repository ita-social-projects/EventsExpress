import EventsExpressService from '../../services/EventsExpressService';
import { SetAlert} from '../alert';

export const editPeriodicity = {
    PENDING : "SET_EDITPERIODICITY_PENDING",
    SUCCESS : "SET_EDITPERIODICITY_SUCCESS",
    ERROR : "SET_EDITPERIODICITY_ERROR",
    UPDATE: "UPDATE_PERIODICITY"
}

const api_serv = new EventsExpressService();


export default function edit_Periodicity(data) {


    return dispatch => {
        dispatch(setEditPeriodicityPending(true));
        const res = api_serv.setPeriodicity(data);
        res.then(response => {
            if (response.error == null) {
                dispatch(setEditPeriodicitySuccess(true));
                dispatch(updatePeriodicity(data));
                dispatch(SetAlert({ variant: 'success', message:'Set periodicity successed'}));
            } else {
                dispatch(setEditPeriodicityError(response.error));
                dispatch(SetAlert({variant:'error', message:'Failed'}));
            }
        });
    }
}

function updatePeriodicity(data) {
    return {
        type: editPeriodicity.UPDATE,
        payload: data
    };
}


function setEditPeriodicityPending(data) {
        return {
            type: editPeriodicity.PENDING,
            payload: data
        };
    }

function setEditPeriodicitySuccess(data) {
        return {
            type: editPeriodicity.SUCCESS,
            payload: data
        };
    }

export function setEditPeriodicityError(data) {
        return {
            type: editPeriodicity.ERROR,
            payload: data
        };
    }
