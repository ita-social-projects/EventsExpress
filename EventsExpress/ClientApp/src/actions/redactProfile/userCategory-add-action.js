import { UserService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { SubmissionError } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";



export const addUserCategory = {
    PENDING: "SET_ADDUSERCATEGORY_PENDING",
    SUCCESS: "SET_ADDUSERCATEGORY_SUCCESS",
    UPDATE: "UPDATE_CATEGORIES",
}

const api_serv = new UserService();

export default function setUserCategory(data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setUserCategory(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getRequestDec());
        dispatch(updateCategories(data));
        dispatch(setSuccessAllert('Favorite categories are updated'));
        return Promise.resolve();
    }
}

function updateCategories(data) {
    return {
        type: addUserCategory.UPDATE,
        payload: data.categories,
    };
}


