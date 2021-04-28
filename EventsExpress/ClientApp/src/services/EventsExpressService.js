import { jwtStorageKey } from '../constants/constants';
import {userImage} from "../constants/userImage";

export default class EventsExpressService {
    _baseUrl = 'api/';

    getResource = async url => {
        const call = _url => fetch(this._baseUrl + _url, {
            method: "get",
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem(jwtStorageKey)}`
            }),
        });

        let res = await call(url);
        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url);
        }
        return res;
    }

    getPhoto = async (url, imgTagId) => {
        const call = _url => fetch(this._baseUrl + url);

        let res = await call(url);

        if(res.ok){
            let blob = await res.blob();
            document.getElementById(imgTagId).src = URL.createObjectURL(blob);
        }
    }

    setResource = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem(jwtStorageKey)}`
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
                    'Authorization': `Bearer ${localStorage.getItem(jwtStorageKey)}`
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
        localStorage.setItem(jwtStorageKey, rest.jwtToken);
        return true;
    }
    setWantToTake = data => this.setResource(`UserEventInventory/MarkItemAsTakenByUser`, data);


    getUsersInventories = eventId => this.getResource(`UserEventInventory/GetAllMarkItemsByEventId/?eventId=${eventId}`);
}
