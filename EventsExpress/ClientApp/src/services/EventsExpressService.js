export default class EventsExpressService {
    _baseUrl = 'api/';

    // Obsolete
    getResource = async (url) => {
        const call = (url) => fetch(this._baseUrl + url, {
            method: "get",
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }),
        });

        let res = await call(url);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url);
        }

        if (res.ok) {
            return await res.json();
        }
        else {
            return {
                error: {
                    ErrorCode: res.status,
                    massage: await res.statusText
                }
            };
        }
    }

    getResourceNew = async url => {
        const call = _url => fetch(this._baseUrl + _url, {
            method: "get",
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }),
        });

        let res = await call(url);
        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url);
        }
        return res;
    }

    setResource = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
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
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
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
        localStorage.setItem('token', rest.jwtToken);
        return true;
    }
	setWantToTake = async (data) => {
        const res = await this.setResource(`UserEventInventory/MarkItemAsTakenByUser`, data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getUsersInventories = async (eventId) => {
        const res = await this.getResource(`UserEventInventory/GetAllMarkItemsByEventId/?eventId=${eventId}`);
        return res;
    }
}
