'use strict';

import { stringify as queryStringStringify } from 'query-string';

const filterHelper = (function () {
    return {
        isObject: function (object) {
            return object !== null && typeof object === 'object';
        },
        compareObjects: function (objFirst, objSecond) {
            const keysObjectFirst = Object.keys(objFirst);
            const keysObjectSecond = Object.keys(objSecond);

            if (keysObjectFirst.length !== keysObjectSecond.length) {
                return false;
            }

            for (const key of keysObjectFirst) {
                const valObjectFirst = objFirst[key];
                const valObjectSecond = objSecond[key];
                const areObjects = this.isObject(valObjectFirst) && this.isObject(valObjectSecond);
                if ((areObjects && !this.compareObjects(valObjectFirst, valObjectSecond))
                    || !areObjects && valObjectFirst !== valObjectSecond) {
                    return false;
                }
            }

            return true;
        },
        getDefaultEventFilter: function () {
            return {
                page: '1',
                keyWord: undefined,
                dateFrom: undefined,
                dateTo: undefined,
                categories: [],
                statuses: [],
                selectedPos: undefined,
                radius: 8,
                x: undefined,
                y: undefined
            }
        },

        getDefaultContactUsFilter: function () {
            return {
                page: '1',
                dateCreated: undefined,
                status: [],
            }
        },

        getQueryStringByFilter: function (filter) {
            return `?${queryStringStringify(
                filter,
                { arrayFormat: 'index' }
            )}`;
        },

        trimUndefinedKeys: function (filter) {
            return JSON.parse(JSON.stringify(filter, (key, value) => value === null ? undefined : value));
        },

    }
}());

export default filterHelper;