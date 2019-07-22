import React from 'react';


export default class EventsExpressService{

    _baseUrl = 'api/';

    setEvent = async (data) => {
        let file = new FormData();
        console.log(data);
        file.append('Photo', data.image.file);
        file.append('Title', data.title);
        file.append('Description', data.description);
        file.append('Location.CityId', '81996ade-9c72-45c9-e60b-08d703976546');
        file.append('User.Id', data.user_id);
        file.append('DateFrom', data.date_from);
        console.log(file);
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

    getAllEvents = async () =>{
        const res = await this.getResource('event/all');
        console.log(res);
        return res;
    }

    getResource = async (url) => {
        const res = await fetch(this._baseUrl + url);
        if(!res.ok){
            return {error: "Invalid data"}
        }
        return await res.json();
    }

    setResourceWithData = (url, data) => fetch(
        this._baseUrl + url,
        {mode: 'no-cors',
            method: "post",
            body: data
        }
    );

    setResource =  (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Content-Type': 'application/json',
                }),
                body: JSON.stringify(data)
            }
        );
        

}