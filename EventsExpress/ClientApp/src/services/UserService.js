import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class UserService {

    getUserById = async (id) => {
        const res = await baseService.getResource(`users/GetUserProfileById?id=${id}`);
        return res;
    }

    getUsers = async (filter) => {
        const res = await baseService.getResource(`users/get${filter}`);
        return res;
    }

    getSearchUsers = async (filter) => {
        const res = await baseService.getResource(`users/searchUsers${filter}`);
        return res;
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
        const res = await baseService.setResource(`Users/Unblock/?userId=${id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setAttitude = async (data) => {
        const res = await baseService.setResource('users/SetAttitude', {
            UserFromId: data.userFromId,
            UserToId: data.userToId,
            Attitude: data.attitude
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}