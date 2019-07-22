import React from 'react';


export default class EventsExpressService{

    _baseUrl = window.location.origin + '/api/';

    setEvent = async (data) => {
        const res = await this.setResource('event/edit', {Title: data.title, 
                                                        Description: data.description, 
                                                        DateFrom: data.date_from,
                                                        Location: {
                                                            CityId: '81996ade-9c72-45c9-e60b-08d703976546'
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

    setCategoryDelete = async (data) => {
        const res = await this.setResource(`category/delete/${data.id}`);
        if (!res.ok) {
            return { error: await res.text() };
        }
        return res;
    }

    setCategory = async (data) => {
        const res = await this.setResource('category/edit', {
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