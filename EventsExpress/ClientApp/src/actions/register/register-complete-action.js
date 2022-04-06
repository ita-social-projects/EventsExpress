import { SubmissionError } from 'redux-form';
import moment from 'moment';
import { AuthenticationService } from '../../services';
import { setSuccessAllert, setErrorAllertFromResponse } from '../alert-action';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { jwtStorageKey } from '../../constants/constants';
import { getUserInfo } from '../login/login-action';
import change_avatar from '../redactProfile/avatar-change-action';

const api_serv = new AuthenticationService();

export default function registerComplete(data, { shouldSaveMoreInfo }) {
    return async dispatch => {
        const body = {
            ...data,
            birthday: moment.utc(data.birthday).local().format('YYYY-MM-DD[T00:00:00]'),
        };

        const profileResponse = await api_serv.setRegisterComplete(body);
        if (!profileResponse.ok) {
            dispatch(setErrorAllertFromResponse(profileResponse.clone()));
            throw new SubmissionError(await buildValidationState(profileResponse));
        }
        
        const jsonRes = await profileResponse.json();
        localStorage.setItem(jwtStorageKey, jsonRes.token);

        if (data.image !== undefined) {
            dispatch(change_avatar(data));
        }

        if (shouldSaveMoreInfo === true) {
            const moreInfoResponse = await api_serv.setMoreInfo(data);
            if (!moreInfoResponse.ok) {
                dispatch(setErrorAllertFromResponse(moreInfoResponse.clone()));
                throw new SubmissionError(await buildValidationState(moreInfoResponse));
            }
        }

        dispatch(getUserInfo());
        dispatch(setSuccessAllert('Your profile was updated'));
        return Promise.resolve();
    };
}
