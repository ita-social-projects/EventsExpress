import { AuthenticationService } from '../../services';
import { createBrowserHistory } from 'history';
import { setSuccessAllert } from '../alert-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { SubmissionError } from 'redux-form';
import { jwtStorageKey } from '../../constants/constants';
import jwt from 'jsonwebtoken';

const api_serv = new AuthenticationService();
const history = createBrowserHistory({ forceRefresh: true });

export default function registerComplete(data) {
    return async dispatch => {
        data.accountId = getAccountIdFromJWT()

        let response = await api_serv.setRegisterComplete(data);
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

export function getAccountIdFromJWT(){
    let token = localStorage.getItem(jwtStorageKey);
    let decoded = jwt.decode(token);
    return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"];
}
