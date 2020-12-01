export default class EventsExpressService {
    _baseUrl = 'api/';

    //#region Resource Interaction Interface
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
    //#endregion Resource Interaction Interface

    getChats = async () => {
        const res = await this.getResource('chat/GetAllChats');
        return res;
    }

    getChat = async (chatId) => {
        const res = await this.getResource(`chat/GetChat?chatId=${chatId}`);
        return res;
    }

    getUnreadMessages = async (userId) => {
        const res = await this.getResource(`chat/GetUnreadMessages?userId=${userId}`);
        return res;
    }

    setEventTemplate = async (data, path) => {
        let file = new FormData();
        if (data.id != null) {
            file.append('Id', data.id);
        }

        if (data.image != null) {
            file.append('Photo', data.image.file);
        }

        if (data.isReccurent) {
            file.append('IsReccurent', data.isReccurent);
            file.append('Frequency', data.frequency);
            file.append('Periodicity', data.periodicity);
        }

        if (data.photoId) {
            file.append('PhotoId', data.photoId);
        }

        file.append('Title', data.title);
        file.append('Description', data.description);
        file.append('CityId', data.cityId);
        file.append('User.Id', data.user_id);
        file.append('IsPublic', data.isPublic);
        file.append('MaxParticipants', data.maxParticipants);
        file.append('DateFrom', new Date(data.dateFrom).toDateString());
        file.append('DateTo', new Date(data.dateTo).toDateString());

        if (data.inventories) {
            data.inventories.map((item, key) => {
                file.append(`Inventories[${key}].NeedQuantity`, item.needQuantity);
                file.append(`Inventories[${key}].ItemName`, item.itemName);
                file.append(`Inventories[${key}].UnitOfMeasuring.id`, item.unitOfMeasuring.id);
            });
        }

        let i = 0;
        data.categories.map(x => {
            return file.append(`Categories[${i++}].Id`, x.id);
        });
        const res = await this.setResourceWithData(path, file);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEvent = async(data) => {
        return this.setEventTemplate(data,`event/edit/?eventId=${data.id}`)
    }

    setEventFromParent = async (data) => {
        return this.setEventTemplate(data,'event/EditEventFromParent');
    }

    setCopyEvent = async (eventId) => {
        const res = await this.setResourceWithData(`event/CreateEventFromParent/?eventId=${eventId}`);
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

    setAvatar = async (data) => {
        let file = new FormData();
        file.append('newAva', data.image.file);
        const res = await this.setResourceWithData('users/changeAvatar', file);
        return !res.ok
            ? { error: await res.text() }
            : await res.text();
    }

    getUnitsOfMeasuring = async () => {
        return await this.getResource('unitofmeasuring/getall');
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

    //#region Events
    getAllEvents = async (filters) => {
        const res = await this.getResource(`event/all${filters}`);
        return res;
    }

    getEvent = async (id) => {
        const res = await this.getResource(`event/get?id=${id}`);
        return res;
    }

    getEvents = async (eventIds, page) => {
        const res = await this.setResource(`event/getEvents?page=${page}`, eventIds);
        return !res.ok
            ? { error: await res.text() }
            : res.json();
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

    getVisitedEvents = async (id, page) => {
        const res = await this.getResource(`event/visitedEvents?id=${id}&page=${page}`);
        return res;
    }

    setEventBlock = async (id) => {
        const res = await this.setResource(`Event/Block/?eventId=${id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEventCancel = async (data) => {
        const res = await this.setResource('EventStatusHistory/Cancel', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setEventUnblock = async (id) => {
        const res = await this.setResource(`event/Unblock/?eventId=${id}`);
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

    setApprovedUser = async (data) => {
        const res = data.buttonAction
            ? await this.setResource(`event/ApproveVisitor?userId=${data.userId}&eventId=${data.eventId}`)
            : await this.setResource(`event/DenyVisitor?userId=${data.userId}&eventId=${data.eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
    //#endregion Events

    //#region Event Schedule
    setEventSchedule = async (data) => {
        let file = new FormData();
        if (data.id != null) {
            file.append('Id', data.id);
        }

        file.append('Frequency', data.frequency);
        file.append('LastRun', data.lastRun);
        file.append('NextRun', data.nextRun);
        file.append('Periodicity', data.periodicity);
        file.append('IsActive', data.isActive);

        const res = await this.setResourceWithData('eventSchedule/edit', file);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setNextEventScheduleCancel = async (eventId) => {
        const res = await this.setResourceWithData(`eventSchedule/CancelNextEvent?eventId=${eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEventSchedulesCancel = async (eventId) => {
        const res = await this.setResourceWithData(`eventSchedule/CancelAllEvents?eventId=${eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getAllEventSchedules = async () => {
        const res = await this.getResource(`eventSchedule/all`);
        return res;
    }

    getEventSchedule = async (id) => {
        const res = await this.getResource(`eventSchedule/get?id=${id}`);
        return res;
    }
    //#endregion Event Schedule

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

    getAllCategories = async () => {
        const res = await this.getResource('category/all');
        return res;
    }

    getAllComments = async (data, page) => {
        const res = await this.getResource(`comment/all/${data}?page=${page}`);
        return res;
    }

    setUsername = async (data) => {
        const res = await this.setResource('Users/EditUsername', {
            name: data.UserName
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setBirthday = async (data) => {
        const res = await this.setResource('Users/EditBirthday', {
            birthday: new Date(data.Birthday)
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setGender = async (data) => {
        const res = await this.setResource('Users/EditGender', {
            gender: Number(data.Gender)
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserCategory = async (data) => {
        const res = await this.setResource('Users/EditUserCategory', data);
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
