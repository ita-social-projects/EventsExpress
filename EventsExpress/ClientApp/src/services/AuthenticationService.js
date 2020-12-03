import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class AuthenticationService {

    auth = async (data) => {
        const res = await baseService.setResource(`Authentication/verify/${data.userId}/${data.token}`);
        return !res.ok
            ? { error: await res.text() }
            : res.json();
    }

    setLogin = async (data) => {
        const res = await baseService.setResource('Authentication/Login', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setGoogleLogin = async (data) => {
        const res = await baseService.setResource('Authentication/GoogleLogin', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setFacebookLogin = async (data) => {
        const res = await baseService.setResource('Authentication/FacebookLogin', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setTwitterLogin = async (data) => {
        const res = await baseService.setResource('Authentication/TwitterLogin', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

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