import { AuthenticationService } from '../../services';
import { createBrowserHistory } from 'history';
import { setSuccessAllert } from '../alert-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { SubmissionError } from 'redux-form';
import { jwtStorageKey } from '../../constants/constants';

const api_serv = new AuthenticationService();
const history = createBrowserHistory({ forceRefresh: true });

export default function registerBindAccount(data) {
    return async dispatch => {

        let response = await api_serv.setRegisterBindAccount(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }

        let jsonRes = await response.json();
        localStorage.setItem(jwtStorageKey, jsonRes.token);
        dispatch(setSuccessAllert('Your profile was updated'));
        dispatch(history.push('/home'))
        return Promise.resolve();
    };
}
