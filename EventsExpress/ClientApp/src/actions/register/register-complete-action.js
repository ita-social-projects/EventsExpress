import { SubmissionError } from 'redux-form';
import moment from 'moment';
import jwt from 'jsonwebtoken';
import { AuthenticationService } from '../../services';
import { setSuccessAllert, setErrorAllertFromResponse } from '../alert-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { jwtStorageKey } from '../../constants/constants';

const api_serv = new AuthenticationService();

export default function registerComplete(data, { shouldSaveMoreInfo }) {
    return async dispatch => {
        const body = {
            ...data,
            birthday: moment.utc(data.birthday).local().format('YYYY-MM-DD[T00:00:00]'),
            accountId: getAccountIdFromJWT(),
        };

        const profileResponse = await api_serv.setRegisterComplete(body);
        if (!profileResponse.ok) {
            dispatch(setErrorAllertFromResponse(profileResponse.clone()));
            throw new SubmissionError(await buildValidationState(profileResponse));
        }
        
        const jsonRes = await profileResponse.json();
        localStorage.setItem(jwtStorageKey, jsonRes.token);

        if (shouldSaveMoreInfo === true) {
            const moreInfoResponse = await api_serv.setMoreInfo(data);
            if (!moreInfoResponse.ok) {
                dispatch(setErrorAllertFromResponse(moreInfoResponse.clone()));
                throw new SubmissionError(await buildValidationState(moreInfoResponse));
            }
        }

        dispatch(setSuccessAllert('Your profile was updated'));
        return Promise.resolve();
    };
}

export function getAccountIdFromJWT(){
    const token = localStorage.getItem(jwtStorageKey);
    const decoded = jwt.decode(token);
    return decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid'];
}
