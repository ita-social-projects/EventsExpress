export const SET_EDITBIRTHDAY_PENDING = "SET_EDITBIRTHDAY_PENDING";
export const SET_EDITBIRTHDAY_SUCCESS = "SET_EDITBIRTHDAY_SUCCESS";
export const SET_EDITBIRTHDAY_ERROR = "SET_EDITBIRTHDAY_ERROR";


export default function editBirthday(date) {
    return dispatch => {
        dispatch(setEditBirthday(false));
        callEditBirthdayNameApi(date, error => {
            if (!error) {
                dispatch(setEditBirthdaySuccess(true));
            } else {
                dispatch(setEditBirthdayError(error));
            }
        });
    };
}

function setEditBirthday(isEditBirthdayPending) {
    return {
        type: SET_EDITBIRTHDAY_PENDING,
        isEditBirthdayPending
    };
}

function setEditBirthdaySuccess(isEditBirthdaySuccess) {
    return {
        type: SET_EDITBIRTHDAY_SUCCESS,
        isEditBirthdaySuccess
    };
}

function setEditBirthdayError(EditBirthdayError) {
    return {
        type: SET_EDITBIRTHDAY_ERROR,
        EditBirthdayError
    };
}

function callEditBirthdayNameApi(date, callback) {

}