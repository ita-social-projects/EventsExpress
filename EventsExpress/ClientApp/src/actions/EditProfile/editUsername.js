export const SET_EDITUSERNAME_PENDING = "SET_EDITUSERNAME_PENDING";
export const SET_EDITUSERNAME_SUCCESS = "SET_EDITUSERNAME_SUCCESS";
export const SET_EDITUSERNAME_ERROR = "SET_EDITUSERNAME_ERROR";


export default function editUsername(name) {
    return dispatch => {
        dispatch(setEditUsernamePending(false));
        callEditUserNameApi(name, error => {
            if (!error) {
                dispatch(setEditUsernameSuccess(true));
            } else {
                dispatch(setEditUsernameError(error));
            }
        });
    };
}

function setEditUsernamePending(isEditUsernamePending) {
    return {
        type: SET_EDITUSERNAME_PENDING,
        isEditUsernamePending
    };
}

function setEditUsernameSuccess(isEditUsernameSuccess) {
    return {
        type: SET_EDITUSERNAME_SUCCESS,
        isEditUsernameSuccess
    };
}

function setEditUsernameError(EditUsernameError) {
    return {
        type: SET_EDITUSERNAME_ERROR,
        EditUsernameError
    };
}

function callEditUserNameApi(name, callback) {
    
}