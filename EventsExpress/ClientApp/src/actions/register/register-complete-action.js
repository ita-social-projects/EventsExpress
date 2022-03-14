import { SubmissionError } from 'redux-form';
import { createBrowserHistory } from 'history';
import moment from 'moment';
import jwt from 'jsonwebtoken';
import { AuthenticationService } from '../../services';
import { setSuccessAllert } from '../alert-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { jwtStorageKey } from '../../constants/constants';

const api_serv = new AuthenticationService();
const history = createBrowserHistory({ forceRefresh: true });

export default function registerComplete(data) {
    return async dispatch => {
        const body = {
            ...data,
            birthday: moment.utc(data.birthday).local().format('YYYY-MM-DD[T00:00:00]'),
            accountId: getAccountIdFromJWT(),
        };

        const response = await api_serv.setRegisterComplete(body);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }

        const jsonRes = await response.json();
        localStorage.setItem(jwtStorageKey, jsonRes.token);
        dispatch(setSuccessAllert('Your profile was updated'));
        dispatch(history.push('/home'));
        return Promise.resolve();
    };
}

export function getAccountIdFromJWT(){
    const token = localStorage.getItem(jwtStorageKey);
    const decoded = jwt.decode(token);
    return decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid'];
}
