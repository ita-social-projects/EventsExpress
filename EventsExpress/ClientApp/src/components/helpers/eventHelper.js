'use strict';

import { stringify as queryStringStringify } from 'query-string';

const eventHelper = (function () {
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

        getQueryStringByEventFilter: function (filter) {
            return `?${queryStringStringify(
                filter,
                { arrayFormat: 'index' }
            )}`;
        },

        trimUndefinedKeys: function (eventFilter) {
            return JSON.parse(JSON.stringify(eventFilter, (key, value) => value === null ? undefined : value));
        },

    }
}());

export default eventHelper;