import React from 'react';


export default class EventsExpressService{

    _baseUrl = 'http://localhost:64144/api/';

    getResource = async (url) => {
        const res = await fetch(this._baseUrl + url);

        if(!res.ok){
            throw new Error(`Could not fetch ${url}` + `, received ${res.status}`);
        }
        return await res.json();
    }

    setResource = async (url, data) =>{
        const res = await fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Content-Type': 'application/json'
                }),
                body: JSON.stringify(data)
            }
        );
        if(!res.ok){
            return await { error: `Could not fetch ${this._baseUrl + url}` +
            `, received ${res.status}`};
        }
        
        return await res.json();
    }

}