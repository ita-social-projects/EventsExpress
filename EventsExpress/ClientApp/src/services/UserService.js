import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class UserService {

    getCount = accountStatus => baseService.getResource(`users/Count/${accountStatus}`);

    getUserById = id => baseService.getResource(`users/GetUserProfileById?id=${id}`);

    getUsers = filter => baseService.getResource(`users/get${filter}`);

    getSearchUsers = filter => baseService.getResource(`users/searchUsers${filter}`);

    getUsersShortInformation = ids => baseService.getResource(`users/getUsersShortInformation${ids}`);

    getSearchUsersShortInformation = filter => baseService.getResource(`users/searchUsersShortInformation${filter}`);

    setAvatar = async data => {
        let file = new FormData();
        file.append('Photo', data.image.file);
        return baseService.setResourceWithData(`UserPhoto/changeAvatar/${data.userId}`, file);
    }

    setChangeUserRole = data =>
        baseService.setResource('Account/ChangeRoles', data);

    setUsername = data => baseService.setResource('Users/EditUsername', {
        name: data.userName
    });

    setBirthday = data => baseService.setResource('Users/EditBirthday', {
        birthday: data.birthday
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

    setLocation = data => baseService.setResource('Users/EditLocation',{
        latitude: data.latitude,
        longitude: data.longitude,
        type: 0,
        onlineMeeting : null
    });

    setFirstname = data => baseService.setResource('Users/EditFirstName',{
        firstName : data.firstName
    });

    setLastname = data => baseService.setResource('Users/EditLastName',{
        lastName: data.lastName
    });
}
