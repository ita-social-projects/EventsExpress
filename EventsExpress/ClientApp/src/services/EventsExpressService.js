import React from 'react';


export default class EventsExpressService{

    _baseUrl = window.location.origin + '/api/';

    setEvent = async (data) => {
        const res = await this.setResource('event/edit', {Title: data.title, 
                                                        Description: data.description, 
                                                        OwnerId: data.user_id,
                                                        DateFrom: data.date_from,
                                                        CityId: '81996ade-9c72-45c9-e60b-08d703976546'  });
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


    setResource =  (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Content-Type': 'application/json'
                }),
                body: JSON.stringify(data)
            }
        );

}