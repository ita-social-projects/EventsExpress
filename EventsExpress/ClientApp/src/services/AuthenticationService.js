import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class AuthenticationService {

    auth = data => baseService.setResource(`Authentication/verify/${data.userId}/${data.token}`);

    getUserInfo = () => baseService.getResourceNew('Users/GetUserInfo');

    setLogin = data => baseService.setResource('Authentication/Login', data);

    setGoogleLogin = data => baseService.setResource('Authentication/GoogleLogin', data);

    setFacebookLogin = data => baseService.setResource('Authentication/FacebookLogin', data);

    setTwitterLogin = data => baseService.setResource('Authentication/TwitterLogin', data);

    revokeToken = () => baseService.setResource('token/revoke-token');

    setRecoverPassword = async (data) => {
        const res = await baseService.setResource(`Authentication/PasswordRecovery/?email=${data.email}`);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setRegister = data => baseService.setResource('Authentication/RegisterBegin', data);

    setRegisterComplete = data => baseService.setResource('Authentication/RegisterComplete', data)

    setChangePassword = async (data) => {
        const res = await baseService.setResource('Authentication/ChangePassword', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}
