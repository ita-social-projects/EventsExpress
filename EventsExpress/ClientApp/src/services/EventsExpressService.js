import React from 'react';


export default class EventsExpressService{

    _baseUrl = 'https://localhost:44315/api/';

    getResource = async (url) => {
        const res = await fetch(this._baseUrl + url);

        if(!res.ok){
            throw new Error(`Could not fetch ${url}` + `, received ${res.status}`);
        }
        return await res.json();
    }

}