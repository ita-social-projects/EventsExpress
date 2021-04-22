import { AuthenticationService } from '../../services';
import { getUserInfo } from './login-action';

const api_serv = new AuthenticationService();

export default () => {
    return dispatch => {
        if (!api_serv.getCurrentToken()) return;

        dispatch(getUserInfo());
    }
}
