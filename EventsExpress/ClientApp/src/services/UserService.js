import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class UserService {

    getUserById = async (id) => {
        return await baseService.getResource(`users/GetUserProfileById?id=${id}`);
    }

    getUsers = async (filter) => {
        return await baseService.getResource(`users/get${filter}`);
    }

    getSearchUsers = async (filter) => {
        return await baseService.getResource(`users/searchUsers${filter}`);
    }

    setContactUs = async (data) => {
        const res = await baseService.setResource('users/ContactAdmins', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setAvatar = async (data) => {
        let file = new FormData();
        file.append('newAva', data.image.file);
        const res = await baseService.setResourceWithData('users/changeAvatar', file);
        return !res.ok
            ? { error: await res.text() }
            : await res.text();
    }

    setChangeUserRole = async (userId, newRoleId) => {
        const res = await baseService.setResource(`users/ChangeRole/?userId=${userId}&roleId=${newRoleId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUsername = async (data) => {
        const res = await baseService.setResource('Users/EditUsername', {
            name: data.UserName
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setBirthday = async (data) => {
        const res = await baseService.setResource('Users/EditBirthday', {
            birthday: new Date(data.Birthday)
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setGender = async (data) => {
        const res = await baseService.setResource('Users/EditGender', {
            gender: Number(data.Gender)
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserCategory = async (data) => {
        const res = await baseService.setResource('Users/EditUserCategory', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserBlock = async (id) => {
        const res = await baseService.setResource(`Users/Block/?userId=${id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserUnblock = async (id) => {
        const res = await baseService.setResource(`Users/${id}/Unblock`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setAttitude = async (data) => {
        const res = await baseService.setResource('users/SetAttitude', {
            userFromId: data.userFromId,
            userToId: data.userToId,
            attitude: data.attitude
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
    setUserNotificationType = async (data) => {
        const res = await baseService.setResource('Users/EditUserNotificationType', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}