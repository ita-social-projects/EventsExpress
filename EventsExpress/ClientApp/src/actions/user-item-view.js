
import EventsExpressService from '../services/EventsExpressService';


export const GET_PROFILE_PENDING = "GET_PROFILE_PENDING";
export const GET_PROFILE_SUCCESS = "GET_PROFILE_SUCCESS";
export const GET_PROFILE_ERROR = "GET_PROFILE_ERROR";


const api_serv = new EventsExpressService();

export default function get_user(id) {

    return dispatch => {
        dispatch(getProfilePending(true));

        const res = api_serv.getUserById(id);
        res.then(response => {
            if (response.error == null) {
                dispatch(getProfile(response));

            } else {
                dispatch(getProfileError(response.error));
            }
        });
    }
}

export function setAttitude(data) {
    return dispatch => {
        const res = api_serv.setAttitude(data);
            res.then(response => {
                if (response.error == null) {
                    const res1 = api_serv.getUserById(data.userToId);
                    res1.then(response => {
                        if (response.error == null) {
                            dispatch(getProfile(response));
                        } else {
                            dispatch(getProfileError(response.error));
                        }
                    });
                }
            });
    }
}

function getProfilePending(data) {
    return {
        type: GET_PROFILE_PENDING,
        payload: data
    }
}

function getProfile(data) {
    return {
        type: GET_PROFILE_SUCCESS,
        payload: data
    }
}

function getProfileError(data) {
    return {
        type: GET_PROFILE_ERROR,
        payload: data
    }
}