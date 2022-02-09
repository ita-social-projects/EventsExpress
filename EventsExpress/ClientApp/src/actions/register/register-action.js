import { AuthenticationService } from '../../services';
import { createBrowserHistory } from 'history';
import { getRequestInc, getRequestDec } from "../request-count-action";
import { buildValidationState } from '../../components/helpers/action-helpers';
import { SubmissionError } from 'redux-form';

export const SET_REGISTER_PENDING = "SET_REGISTER_PENDING";
export const SET_REGISTER_SUCCESS = "SET_REGISTER_SUCCESS";

const api_serv = new AuthenticationService();
const history = createBrowserHistory({ forceRefresh: true });

export default function register(email, password) {
    return async dispatch => {
        dispatch(getRequestInc());

        let response = await api_serv.setRegister({ Email: email, Password: password });
        dispatch(getRequestDec());
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(history.push('/registerSuccess'))
        return Promise.resolve();
    };
}
