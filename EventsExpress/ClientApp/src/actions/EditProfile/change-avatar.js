import EventsExpressService from '../../services/EventsExpressService';
export const changeAvatar = {
    PENDING: "SET_CHANGE_AVATAR_PENDING",
    SUCCESS: "SET_CHANGE_AVATAR_SUCCESS",
    ERROR: "SET_CHANGE_AVATAR_ERROR",
    UPDATE: "UPDATE_CHANGE_AVATAR"
}

const api_serv = new EventsExpressService();

export default  function change_avatar(data) {
    return dispatch => {
        dispatch(setAvatarPending(true));
        const res = api_serv.setAvatar(data);
        console.log(res);
        res.then(response => {
            if (response.error == null) {
                dispatch(setAvatarSuccess(true));
                //console.log( response.text());
                console.log(response);
                
                dispatch(updateAvatar(response));
            } else {
                dispatch(setAvatarError(response.error));
            }
        });

    }
}

function updateAvatar(data) {
    return {
        type: changeAvatar.UPDATE,
        payload: data
    };
}

function setAvatarPending(data) {
    return {
        type: changeAvatar.PENDING,
        payload: data
    };
}

function setAvatarSuccess(data) {
    return {
        type: changeAvatar.SUCCESS,
        payload: data
    };
}

function setAvatarError(data) {
    return {
        type: changeAvatar.ERROR,
        payload: data
    };
}

