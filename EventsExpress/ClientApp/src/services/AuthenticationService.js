import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class AuthenticationService {

    auth = data => baseService.setResource(`Authentication/verify/${data.userId}/${data.token}`);

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

    setRegister = async (data) => {
        const res = await baseService.setResource('Authentication/register', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setChangePassword = async (data) => {
        const res = await baseService.setResource('Authentication/ChangePassword', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}
