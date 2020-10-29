export default class EventsExpressService {
    _baseUrl = 'api/';

    getResource = async (url) => {
        const call = (url) => fetch(this._baseUrl + url, {
            method: "get",
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }),
        });

        let res = await call(url);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url);
        }

        if (res.ok) {
            return await res.json();
        }
        else {
            return {
                error: {
                    ErrorCode: res.status,
                    massage: await res.statusText
                }
            };
        }
    }

    setResource = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }),
                body: JSON.stringify(data)
            }
        );

        let res = await call(url, data);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url, data);
        }

        return res;
    }

    setResourceWithData = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }),
                body: data
            }
        );

        let res = await call(url, data);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url, data);
        }

        return res;
    }

    refreshHandler = async () => {
        localStorage.removeItem("token");
        var response = await fetch('api/token/refresh-token', {
            method: "POST"
        });
        if (!response.ok) {
            return false;
        }
        let rest = await response.json();
        localStorage.setItem('token', rest.jwtToken);
        return true;
    }

    getChats = async () => {
        const res = await this.getResource('chat/GetAllChats');
        return res;
    }

    getChat = async (chatId) => {
        const res = await this.getResource(`chat/GetChat?chatId=${chatId}`);
        return res;
    }

    getEvents = async (eventIds, page) => {
        const res = await this.setResource(`event/getEvents?page=${page}`, eventIds);
        return !res.ok
            ? { error: await res.text() }
            : res.json();
    }

    getUnreadMessages = async (userId) => {
        const res = await this.getResource(`chat/GetUnreadMessages?userId=${userId}`);
        return res;
    }

    setEvent = async (data) => {
        let file = new FormData();
        if (data.id != null) {
            file.append('Id', data.id);
        }

        if (data.image != null) {
            file.append('Photo', data.image.file);
        }

        file.append('Title', data.title);
        file.append('MaxParticipants', data.maxParticipants);
        file.append('Description', data.description);
        file.append('CityId', data.cityId);
        file.append('User.Id', data.user_id);

        data.dateFrom != null
            ? file.append('DateFrom', new Date(data.dateFrom).toDateString())
            : file.append('DateFrom', new Date(Date.now()).toDateString());

        data.dateTo != null
            ? file.append('DateTo', new Date(data.dateTo).toDateString())
            : file.append('DateTo', new Date(data.dateFrom).toDateString());

        let i = 0;
        data.categories.map(x => {
            return file.append(`Categories[${i++}].Id`, x.id);
        });
        const res = await this.setResourceWithData('event/edit', file);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEventBlock = async (id) => {
        const res = await this.setResource(`Event/Block/?eventId=${id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setContactUs = async (data) => {
        const res = await this.setResource('users/ContactAdmins', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEventUnblock = async (id) => {
        const res = await this.setResource(`Event/Unblock/?eventId=${id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserFromEvent = async (data) => {
        const res = await this.setResource(`event/DeleteUserFromEvent?userId=${data.userId}&eventId=${data.eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserToEvent = async (data) => {
        const res = await this.setResource(`event/AddUserToEvent?userId=${data.userId}&eventId=${data.eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setAvatar = async (data) => {
        let file = new FormData();
        file.append('newAva', data.image.file);
        const res = await this.setResourceWithData('users/changeAvatar', file);
        return !res.ok
            ? { error: await res.text() }
            : await res.text();
    }

    //#region Authentication
    auth = async (data) => {
        const res = await this.setResource(`Authentication/verify/${data.userId}/${data.token}`);
        return !res.ok
            ? { error: await res.text() }
            : res.json();
    }

    setLogin = async (data) => {
        const res = await this.setResource('Authentication/Login', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setGoogleLogin = async (data) => {
        const res = await this.setResource('Authentication/GoogleLogin', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setFacebookLogin = async (data) => {
        const res = await this.setResource('Authentication/FacebookLogin', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setTwitterLogin = async (data) => {
        const res = await this.setResource('Authentication/TwitterLogin', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setRecoverPassword = async (data) => {
        const res = await this.setResource(`Authentication/PasswordRecovery/?email=${data.email}`);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setRegister = async (data) => {
        const res = await this.setResource('Authentication/register', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setChangePassword = async (data) => {
        const res = await this.setResource('Authentication/ChangePassword', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
    //#endregion Authentication

    getEvent = async (id) => {
        const res = await this.getResource(`event/get?id=${id}`);
        return res;
    }

    getUsers = async (filter) => {
        const res = await this.getResource(`users/get${filter}`);
        return res;
    }

    getUserById = async (id) => {
        const res = await this.getResource(`users/GetUserProfileById?id=${id}`);
        return res;
    }

    getSearchUsers = async (filter) => {
        const res = await this.getResource(`users/searchUsers${filter}`);
        return res;
    }

    getCountries = async () => {
        const res = await this.getResource('locations/countries');
        return res;
    }

    getCities = async (country) => {
        const res = await this.getResource(`locations/country:${country}/cities`);
        return res;
    }

    getRoles = async () => {
        const res = await this.getResource('roles');
        return res;
    }

    setChangeUserRole = async (userId, newRoleId) => {
        const res = await this.setResource(`users/ChangeRole/?userId=${userId}&roleId=${newRoleId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setCategoryDelete = async (data) => {
        const res = await this.setResource(`category/delete/${data}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setCommentDelete = async (data) => {
        const res = await this.setResource(`comment/delete/${data.id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setCategory = async (data) => {
        const res = await this.setResource('category/edit', {
            Id: data.Id,
            Name: data.Name
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setComment = async (data) => {
        const res = await this.setResource('comment/edit', {
            Id: data.id,
            Text: data.comment,
            UserId: data.userId,
            EventId: data.eventId,
            CommentsId: data.commentsId
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setRate = async (data) => {
        const res = await this.setResource('event/setrate', {
            Rate: data.rate,
            UserId: data.userId,
            EventId: data.eventId
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getCurrentRate = async (eventId) => {
        const res = await this.getResource(`event/GetCurrentRate/${eventId}`);
        return res;
    }

    getAverageRate = async (eventId) => {
        const res = await this.getResource(`event/GetAverageRate/${eventId}`);
        return res;
    }

    getAllEvents = async (filters) => {
        const res = await this.getResource(`event/all${filters}`);
        return res;
    }

    getAllEventsForAdmin = async (filters) => {
        const res = await this.getResource(`event/AllForAdmin${filters}`);
        return res;
    }

    getAllCategories = async () => {
        const res = await this.getResource('category/all');
        return res;
    }

    getAllComments = async (data, page) => {
        const res = await this.getResource(`comment/all/${data}?page=${page}`);
        return res;
    }

    getVisitedEvents = async (id, page) => {
        const res = await this.getResource(`event/visitedEvents?id=${id}&page=${page}`);
        return res;
    }

    getFutureEvents = async (id, page) => {
        const res = await this.getResource(`event/futureEvents?id=${id}&page=${page}`);
        return res;
    }

    getPastEvents = async (id, page) => {
        const res = await this.getResource(`event/pastEvents?id=${id}&page=${page}`);
        return res;
    }

    getEventsToGo = async (id, page) => {
        const res = await this.getResource(`event/EventsToGo?id=${id}&page=${page}`);
        return res;
    }

   

   

    setUsername = async (data) => {
        const res = await this.setResource('Users/EditUsername', {
            Name: data.UserName
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setBirthday = async (data) => {
        const res = await this.setResource('Users/EditBirthday', {
            Birthday: new Date(data.Birthday).toDateString()
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setGender = async (data) => {
        const res = await this.setResource('Users/EditGender', {
            Gender: data.Gender
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserCategory = async (data) => {
        const res = await this.setResource('Users/EditUserCategory', {
            Categories: data.Categories
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserBlock = async (id) => {
        const res = await this.setResource(`Users/Block/?userId=${id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserUnblock = async (id) => {
        const res = await this.setResource(`Users/Unblock/?userId=${id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setAttitude = async (data) => {
        const res = await this.setResource('users/SetAttitude', {
            UserFromId: data.userFromId,
            UserToId: data.userToId,
            Attitude: data.attitude
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setChangePassword = async (data) => {
        const res = await this.setResource('Authentication/ChangePassword', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

   
}
