import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class UserService {

    getUserById = id => baseService.getResourceNew(`users/GetUserProfileById?id=${id}`);

    getUsers = filter => baseService.getResourceNew(`users/get${filter}`);

    getSearchUsers = filter => baseService.getResourceNew(`users/searchUsers${filter}`);

    setContactUs = data => baseService.setResource('contactUs/ContactAdmins', data);

    getAllIssues = () => baseService.getResourceNew('contactUs/all');

    setAvatar = async(data) => {
        let file = new FormData();
        file.append('newAva', data.image.file);
        await baseService.setResourceWithData('users/changeAvatar', file);
    }

    setChangeUserRole = (userId, newRoleId) =>
        baseService.setResource(`users/ChangeRole/?userId=${userId}&roleId=${newRoleId}`);

    setUsername = data => baseService.setResource('Users/EditUsername', {
        name: data.UserName
    });

    setBirthday = data => baseService.setResource('Users/EditBirthday', {
        birthday: new Date(data.Birthday)
    });

    setGender = data => baseService.setResource('Users/EditGender', {
        gender: Number(data.Gender)
    });

    setUserCategory = data => baseService.setResource('Users/EditUserCategory', data);

    setUserBlock = id => baseService.setResource(`Users/Block/?userId=${id}`);

    setUserUnblock = id => baseService.setResource(`Users/${id}/Unblock`);

    setAttitude = data => baseService.setResource('users/SetAttitude', {
        userFromId: data.userFromId,
        userToId: data.userToId,
        attitude: data.attitude
    });

    setUserNotificationType = data => baseService.setResource('Users/EditUserNotificationType', data);
}