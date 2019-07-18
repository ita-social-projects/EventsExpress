export const SET_EDITGENDER_PENDING = "SET_EDITGENDER_PENDING";
export const SET_EDITGENDER_SUCCESS = "SET_EDITGENDER_SUCCESS";
export const SET_EDITGENDER_ERROR = "SET_EDITGENDER_ERROR";


export default function editUsername(gender) {
    return dispatch => {
        dispatch(setEditGenderPending(false));
        callEditGenderNameApi(gender, error => {
            if (!error) {
                dispatch(setEditGenderSuccess(true));
            } else {
                dispatch(setEditGenderError(error));
            }
        });
    };
}

function setEditGenderPending(isEditGenderPending) {
    return {
        type: SET_EDITGENDER_PENDING,
        isEditGenderPending
    };
}

function setEditGenderSuccess(isEditGenderSuccess) {
    return {
        type: SET_EDITGENDER_SUCCESS,
        isEditGenderSuccess
    };
}

function setEditGenderError(EditGenderError) {
    return {
        type: SET_EDITGENDER_ERROR,
        EditGenderError
    };
}

function callEditGenderNameApi(name, callback) {

}