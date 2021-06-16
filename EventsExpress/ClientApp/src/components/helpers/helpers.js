import React from "react";
import 'react-widgets/dist/css/react-widgets.css';
import "react-datepicker/dist/react-datepicker.css";
import moment from "moment";
import './helpers.css'

export const validate = values => {
    const errors = {};
    const numberFields = ['maxParticipants', 'frequency'];
    const occurenceFields = ['periodicity', 'frequency'];
    const requiredFields = [
        'email',
        'password',
        'RepeatPassword',
        'title',
        'description',
        'categories',
        'countryId',
        'cityId',
        'RepeatPassword',
        'oldPassword',
        'newPassword',
        'repeatPassword',
        'Birthday',
        'UserName',
        'itemName',
        'needQuantity',
        'unitOfMeasuring',
        'willTake',
        'image'
    ];

    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Required'
        }
    });

    numberFields.forEach(field => {
        if (values[field] && values[field] < 1) {
            errors[field] = `Invalid data`;
        }
    })
    if (values.inventories != null) {
        const inventoriesArrayErrors = [];
        values.inventories.forEach((item, index) => {
            const inventoriesErrors = {};
            if (!item || !item.itemName) {
                inventoriesErrors.itemName = 'Required';
                inventoriesArrayErrors[index] = inventoriesErrors;
            }
            if (item.itemName && item.itemName.length > 30) {
                inventoriesErrors.itemName = 'Invalid length: 1 - 30 symbols';
                inventoriesArrayErrors[index] = inventoriesErrors;
            }
            if (!item || !item.needQuantity) {
                inventoriesErrors.needQuantity = 'Required';
                inventoriesArrayErrors[index] = inventoriesErrors;
            }
            if (item.needQuantity <= 0) {
                inventoriesErrors.needQuantity = 'Can not be negative';
                inventoriesArrayErrors[index] = inventoriesErrors;
            }
            if (!item || !item.unitOfMeasuring) {
                inventoriesErrors.unitOfMeasuring = {};
                inventoriesErrors.unitOfMeasuring.id = 'Required';
                inventoriesArrayErrors[index] = inventoriesErrors;
            }
        })
        if (inventoriesArrayErrors.length) {
            errors.inventories = inventoriesArrayErrors;
        }
    }

    if (values.image != null && values.image.file != null && values.image.file.size < 4096)
        errors.image = "Image is too small";

    if (values.categories != null && values.categories.length == 0) {
        errors.categories = "Required";
    }

    if (!values.selectedPos || values.selectedPos == "") {
        errors.selectedPos = "Required";
    }

    if (values.maxParticipants && values.maxParticipants < 1) {
        errors.maxParticipants = `Invalid data`;
    }

    occurenceFields.forEach(field => {
        if ('checkOccurence'.checked && !values[field]) {
            errors[field] = 'Required'
        }
    })

    if (values.needQuantity && values.needQuantity < 1) {
        errors.needQuantity = `Invalid data`;
    }

    if (values.visitors
        && values.maxParticipants
        && values.maxParticipants < values.visitors.length) {
        errors.maxParticipants = `${values.visitors.length} participants are subscribed to event`;
    }

    if (values.email &&
        !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
        errors.email = 'Invalid email address'
    }

    if (values.password !== values.RepeatPassword) {
        errors.RepeatPassword = 'Passwords do not match';
    }

    if (values.newPassword !== values.repeatPassword) {
        errors.repeatPassword = 'Passwords do not match';
    }

    if (new Date(values.Birthday).getTime() >= Date.now()) {
        errors.Birthday = 'Date is incorrect';
    }

    return errors;
}

export const validateEventForm = values => {

    if (!values)
        return values;

    if (!values.isPublic) {
        values.isPublic = false;
    }

    if (values.isReccurent) {
        if (!values.frequency) {
            values.frequency = 0;
        }
    }

    if (!values.maxParticipants) {
        values.maxParticipants = 2147483647;
    }

    if (!values.dateFrom) {
        values.dateFrom = new Date(Date.now());
    }

    if (!values.dateTo) {
        values.dateTo = new Date(values.dateFrom);
    }

    return values;
}

export const getAge = birthday => {
    let today = new Date();
    var date = moment(today);
    var birthDate = moment(birthday);
    let age = date.diff(birthDate, 'years');

    if (age >= 100) {
        age = "---";
    }

    return age;
}

export const maxLength = max => value =>
    value && value.length > max
        ? `Must be ${max} characters or less`
        : undefined

export const minLength = min => value =>
    value && value.length < min
        ? `Must be ${min} characters or more`
        : undefined;

export const maxLength15 = maxLength(15);
export const minLength2 = minLength(6);
export const minLength3 = minLength(4);
export const minLength5 = minLength(5);
export const required = value => value ? undefined : 'Field is required'


export const renderErrorMessage = (responseData, key) => {
    let response;
    response = JSON.parse(responseData)["errors"];
    if (response[key]) {
        return (<div className="text-danger">
            {response[key].map(item =>
                <div>
                    {item}
                </div>
            )}
        </div>
        )
    }
}

// deprecated
export const buildValidationState = (responseData) => {
    let response;
    response = JSON.parse(responseData)["errors"];
    let result = {};
    for (const [key, value] of Object.entries(response)) {
        if(key == "")
        {
            result = {...result, _error: value}
        }
        else
        {
            result = {...result, [key]: value}
        }
    }
    return result;
}
