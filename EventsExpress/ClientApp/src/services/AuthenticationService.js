import EventsExpressService from './EventsExpressService';
import { jwtStorageKey } from '../constants/constants'

const baseService = new EventsExpressService();

export default class AuthenticationService {
    getCurrentToken = () => localStorage.getItem(jwtStorageKey);

    auth = data => baseService.setResource(`Authentication/verify/${data.userId}/${data.token}`);

    getUserInfo = () => baseService.getResource('Users/GetUserInfo');

    setLogin = data => baseService.setResource('Authentication/Login', data);

    setGoogleLogin = data => baseService.setResource('Authentication/GoogleLogin', data);

    setFacebookLogin = data => baseService.setResource('Authentication/FacebookLogin', data);

    setTwitterLogin = data => baseService.setResource('Authentication/TwitterLogin', data);

    revokeToken = () => baseService.setResource('token/revoke-token');

    setRecoverPassword = data => baseService.setResource(`Authentication/PasswordRecovery/?email=${data.email}`);

    setRegister = data => baseService.setResource('Authentication/RegisterBegin', data);

    setRegisterBindAccount = data => baseService.setResource('Authentication/RegisterBindExternalAccount', data);

    setRegisterComplete = data => baseService.setResource('Authentication/RegisterComplete', data);

    setMoreInfo = data => baseService.setResource('UserMoreInfo/Create', data);

    setChangePassword = data =>  baseService.setResource('Authentication/ChangePassword', data);
}
