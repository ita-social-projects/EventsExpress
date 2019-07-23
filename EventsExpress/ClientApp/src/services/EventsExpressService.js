import React from 'react';
import { dark } from '@material-ui/core/styles/createPalette';


export default class EventsExpressService{

    _baseUrl = 'api/';

    setEvent = async (data) => {
        let file = new FormData();
        file.append('Photo', data.image.file);
        file.append('Title', data.title);
        file.append('Description', data.description);
        file.append('Location.CityId', data.city);
        file.append('User.Id', data.user_id);
        file.append('DateFrom', data.date_from);
        const res = await this.setResourceWithData('event/edit', file);
        if(!res.ok){
            return { error: await res.text()};
        }
        return res;
    }



    setLogin = async (data) => {
        const res = await this.setResource('Authentication/login', data);
        if(!res.ok){
            return { error: await res.text()};
        }
        return await res.json();
    }

    setRegister = async (data) => {
        const res = await this.setResource('Authentication/register', data);
        if(!res.ok){
            return { error: await res.text()};
        }
        return res;
    }


    getUsers = async () => {
        const res = await this.getResource('users');
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

    setCategoryDelete = async (data) => {
        const res = await this.setResource(`category/delete/${data.id}`);
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

    getAllCategories = async () => {
        const res = await this.getResource('category/all');
        console.log(res);
        return res;
    }

    getAllEvents = async () =>{
        const res = await this.getResource('event/all');
        return res;
    }

    getAllCategories = async () => {
        const res = await this.getResource('category/all');
        console.log(res);
        return res;
    }

    getResource = async (url) => {
        const res = await fetch(this._baseUrl + url,
            {
                method: "get",
                headers: new Headers({
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                }),
            });
        if(!res.ok){
            return {error: "Invalid data"}
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
    


    setResource =  (url, data) => fetch(
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
        

}