import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class AuthenticationService {
    getCurrentToken = () => localStorage.getItem('token');
    
    setAuth = () => baseService.setResource('Authentication/login_token');

    auth = data => baseService.setResource(`Authentication/verify/${data.userId}/${data.token}`);

    getUserInfo = () => baseService.getResourceNew('Users/GetUserInfo');

    setLogin = data => baseService.setResource('Authentication/Login', data);

    setGoogleLogin = data => baseService.setResource('Authentication/GoogleLogin', data);

    setFacebookLogin = data => baseService.setResource('Authentication/FacebookLogin', data);

    setTwitterLogin = data => baseService.setResource('Authentication/TwitterLogin', data);

    revokeToken = () => baseService.setResource('token/revoke-token');

    setRecoverPassword = data => baseService.setResource(`Authentication/PasswordRecovery/?email=${data.email}`);

    setRegister = data => baseService.setResource('Authentication/RegisterBegin', data);

    setRegisterComplete = data => baseService.setResource('Authentication/RegisterComplete', data)

    setChangePassword = data =>  baseService.setResource('Authentication/ChangePassword', data);
}
