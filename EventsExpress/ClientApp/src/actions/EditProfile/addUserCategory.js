import { UserService } from '../../services';
import { setAlert } from '../alert';

export const addUserCategory = {
    PENDING: "SET_ADDUSERCATEGORY_PENDING",
    SUCCESS: "SET_ADDUSERCATEGORY_SUCCESS",
    ERROR: "SET_ADDUSERCATEGORY_ERROR",
    UPDATE: "UPDATE_CATEGORIES",
}

const api_serv = new UserService();

export default function setUserCategory(data) {
    return dispatch => {
        dispatch(setAddUserCategoryPending(true));
        const res = api_serv.setUserCategory(data);
        res.then(response => {
            if (!response.error) {
                dispatch(setAddUserCategorySuccess(true));
                dispatch(updateCategories(data));
                dispatch(setAlert({ variant: 'success', message: 'Favarote categoris is updated' }));
            } else {
                dispatch(setAddUserCategoryError(response.error));
                dispatch(setAlert({ variant: 'error', message: 'Failed' }));
            }
        });
    }
}

function updateCategories(data) {
    return {
        type: addUserCategory.UPDATE,
        payload: data.categories,
    };
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

export function setAddUserCategoryError(data) {
    return {
        type: addUserCategory.ERROR,
        payload: data
    };
}

