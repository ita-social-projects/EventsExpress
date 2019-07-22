import EventsExpressService from '../../services/EventsExpressService';

export const addUserCategory = {
    PENDING: "SET_ADDUSERCATEGORY_PENDING",
    SUCCESS: "SET_ADDUSERCATEGORY_SUCCESS",
    ERROR: "SET_ADDUSERCATEGORY_ERROR"
}

const api_serv = new EventsExpressService();

export default function add_UserCategory(data) {
    return dispatch => {
        dispatch(setAddUserCategoryPending(true));
        const res = api_serv.setUserCategories(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setAddUserCategorySuccess(true));

            } else {
                dispatch(setAddUserCategorySuccess(response.error));
            }
        });

    }
}

function setAddUserCategoryPending(data) {
    return {
        type: addUserCategory.PENDING,
        payload: data
    };
}

function setAddUserCategorySuccess(data) {
    return {
        type: addUserCategory.SUCCESS,
        payload: data
    };
}

function setAddUserCategoryError(data) {
    return {
        type: addUserCategory.ERROR,
        payload: data
    };
}

