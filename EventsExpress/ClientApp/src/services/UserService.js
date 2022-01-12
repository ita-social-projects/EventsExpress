import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class UserService {

    getCount = accountStatus => baseService.getResource(`users/Count/${accountStatus}`);

    getUserById = id => baseService.getResource(`users/GetUserProfileById?id=${id}`);

    getUsers = filter => baseService.getResource(`users/get${filter}`);

    getSearchUsers = filter => baseService.getResource(`users/searchUsers${filter}`);

    getSearchUsersShortInformation = filter => baseService.getResource(`users/searchUsersShortInformation${filter}`);

    setAvatar = async(data) => {
        let file = new FormData();
        file.append('Photo', data.image.file);
        return baseService.setResourceWithData(`users/changeAvatar/${data.userId}`, file);
    }

    setChangeUserRole = data =>
        baseService.setResource('Account/ChangeRoles', data);

    setUsername = data => baseService.setResource('Users/EditUsername', {
        name: data.userName
    });

    setBirthday = data => baseService.setResource('Users/EditBirthday', {
        birthday: new Date(data.birthday)
    });

    setGender = data => baseService.setResource('Users/EditGender', {
        gender: Number(data.gender)
    });

    setUserCategory = data => baseService.setResource('Users/EditUserCategory', data);

    setUserBlock = id => baseService.setResource(`Account/Block/?userId=${id}`);

    setUserUnblock = id => baseService.setResource(`Account/${id}/Unblock`);

    setAttitude = data => baseService.setResource('users/SetAttitude', {
        userFromId: data.userFromId,
        userToId: data.userToId,
        attitude: data.attitude
    });

    setUserNotificationType = data => baseService.setResource('Users/EditUserNotificationType', data);
}
