import React from 'react';
import { dark } from '@material-ui/core/styles/createPalette';


export default class EventsExpressService {

    _baseUrl = 'api/';

    setEvent = async (data) => {
        let file = new FormData();
        if (data.id != null) {

            file.append('Id', data.id);
        }
        file.append('Photo', data.image.file);
        file.append('Title', data.title);
        file.append('Description', data.description);
        file.append('Location.CityId', data.city);
        file.append('User.Id', data.user_id);
        if (data.dateFrom != null) {
            file.append('DateFrom', new Date(data.dateFrom).toDateString());
        }
        
        if (data.dateFrom != null) {
            file.append('DateTo', new Date(data.dateTo).toDateString());
        }
        let i = 0;
        data.categories.map(x => {
            console.log(i);
            file.append('Categories[' + i + '].Id', x.id);
            i++;
        })
        const res = await this.setResourceWithData('event/edit', file);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setUserFromEvent = async (data) => {
        const res = await this.setResource('event/DeleteUserFromEvent?userId=' + data.userId + '&eventId=' + data.eventId);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setUserToEvent = async (data) => {
        const res = await this.setResource('event/AddUserToEvent?userId=' + data.userId + '&eventId=' + data.eventId);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setAvatar = async (data) => {
        let file = new FormData();
        file.append('newAva', data.image.file);
        const res = await this.setResourceWithData('users/changeAvatar', file);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setLogin = async (data) => {
        const res = await this.setResource('Authentication/login', data);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return await res.json();
    }

    setRegister = async (data) => {
        const res = await this.setResource('Authentication/register', data);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    getEvent = async (id) => {
        const res = await this.getResource('event/get?id=' + id);
        return res;
    }

    getUsers = async (filter) => {
        const res = await this.getResource(`users/get${filter}`);
        console.log(res);
        return res;
    }
    getSearchUsers = async (filter) => {
        const res = await this.getResource(`users/searchUsers${filter}`);
        console.log(res);
        return res;
    }
    getCountries = async () => {
        const res = await this.getResource('locations/countries');
        return res;
    }

    getCities = async (country) => {
        const res = await this.getResource('locations/country:' + country + '/cities');
        return res;
    }

    getRoles = async () => {
        const res = await this.getResource('roles');
        return res;
    }

    setChangeUserRole = async (userId, newRoleId) => {
        const res = await this.setResource(`users/ChangeRole/?userId=` + userId + '&roleId=' + newRoleId);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setCategoryDelete = async (data) => {
        const res = await this.setResource(`category/delete/${data.id}`);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setCommentDelete = async (data) => {
        console.log(data);
        const res = await this.setResource(`comment/delete/${data.id}`);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setCategory = async (data) => {
        const res = await this.setResource('category/edit', {
            Id: data.Id,
            Name: data.category
        });
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setComment = async (data) => {
        const res = await this.setResource('comment/edit', {
            Id: data.id,
            Text: data.comment,
            UserId: data.userId,
            EventId: data.eventId
        });
        if (!res.ok) {
            return { error: await res.text() };
        }
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
        console.log(res);
        return res;
    }

    getAllComments = async (data, page) => {
        const res = await this.getResource(`comment/all/${data}?page=${page}`);
        console.log(res);
        return res;
    }

    getResource = async (url) => {
        const res = await fetch(this._baseUrl + url, {
            method: "get",
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }),
        });
        if (!res.ok) {
            return {
                error:
                {
                    ErrorCode: res.status,
                    massage: await res.statusText
                }
            }
        }
        return await res.json();
    }

    setUsername = async (data) => {
        const res = await this.setResource('Users/EditUsername', {
            Name: data.UserName
        });
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setBirthday = async (data) => {
        const res = await this.setResource('Users/EditBirthday', {
            Birthday: data.Birthday
        });
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setGender = async (data) => {
        const res = await this.setResource('Users/EditGender', {
            Gender: data.Gender
        });
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setUserCategory = async (data) => {
        console.log(data);
        const res = await this.setResource('Users/EditUserCategory', {
            Categories: data.Categories
        });
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setUserBlock = async (id) => {
        const res = await this.setResource('Users/Block/?userId=' + id);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setUserUnblock = async (id) => {
        const res = await this.setResource('Users/Unblock/?userId=' + id);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setResource = (url, data) => fetch(
        this._baseUrl + url,
        {
            method: "post",
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }),
            body: JSON.stringify(data)
        }
    );

    setResourceWithData = (url, data) => fetch(
        this._baseUrl + url,
        {
            method: "post",
            headers: new Headers({
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }),
            body: data
        }
    );

}