import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class AccountService {

    getLinkedAuths = () => baseService.getResource('Account/GetLinkedAuth');

    setGoogleLoginAdd = data => baseService.setResource('Account/AddGoogleLogin', data);

    setFacebookLoginAdd = data => baseService.setResource('Account/AddFacebookLogin', data);

    setTwitterLoginAdd = data => baseService.setResource('Account/AddTwitterLogin', data);

    setLocalLoginAdd = data => baseService.setResource('Account/AddLocalLogin', data);
}
