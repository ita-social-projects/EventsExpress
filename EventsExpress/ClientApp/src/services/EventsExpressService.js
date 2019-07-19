import React from 'react';


export default class EventsExpressService{

    _baseUrl = window.location.origin + '/api/';

    setEvent = async (data) => {
        
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

    getResource = async (url) => {
        const res = await fetch(this._baseUrl + url);

        if(!res.ok){
            throw new Error(`Could not fetch ${url}` + `, received ${res.status}`);
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