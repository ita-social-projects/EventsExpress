import React from 'react';


export default class EventsExpressService{

    _baseUrl = window.location.origin + '/api/';

    setEvent = async (data) => {
        const res = await this.setResource('event/edit', {Title: data.title, 
                                                        Description: data.description, 
                                                        DateFrom: data.date_from,
                                                        Location: {
                                                            CityId: 'aab36e16-7004-415b-2932-08d70456082e'
                                                          },
                                                        User: {
                                                            Id: data.user_id
                                                        }
                                                    });
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