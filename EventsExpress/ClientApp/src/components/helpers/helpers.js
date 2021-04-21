import React from "react";
import TextField from "@material-ui/core/TextField";
import Multiselect from 'react-widgets/lib/Multiselect';
import DatePicker from 'react-datepicker';
import 'react-widgets/dist/css/react-widgets.css';
import "react-datepicker/dist/react-datepicker.css";
import Select from '@material-ui/core/Select';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';
import RadioGroup from '@material-ui/core/RadioGroup';
import { renderFieldError } from './form-helpers';
import moment from "moment";
import './helpers.css'

export const radioButton = ({ input, ...rest }) => (
    <FormControl>
        <RadioGroup {...input} {...rest}>
            <FormControlLabel value="blocked" control={<Radio />} label="Blocked" />
            <FormControlLabel value="active" control={<Radio />} label="Active" />
            <FormControlLabel value="all" control={<Radio />} label="All" />
        </RadioGroup>
    </FormControl>
)
export const radioLocationType = ({ input, meta: { error, touched }, ...rest }) => (
    <FormControl>

        <RadioGroup {...input} {...rest}>
            <FormControlLabel value="0" control={<Radio />} label="Map" />
            <FormControlLabel value="1" control={<Radio />} label="Online" />
        </RadioGroup>
        {renderFieldError({ touched, error })}
    </FormControl>
)

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

export const renderMyDatePicker = ({ input: { onChange, value }, defaultValue, minValue, maxValue, label }) => {
    value = value || defaultValue || new Date(2000, 1, 1, 12, 0, 0);
    minValue = new Date().getFullYear() - 115;
    maxValue = new Date().getFullYear() - 15;

    return <>
        <InputLabel>{label}</InputLabel>
        <DatePicker
            onChange={onChange}
            selected={new Date(value) || new Date()}
            minDate={new Date(minValue, 1, 1, 0, 0, 0)}
            maxDate={new Date(maxValue, 12, 31, 23, 59, 59)}
            peekNextMonth
            showMonthDropdown
            showYearDropdown
            dropdownMode="select"
        />
    </>
}

export const renderDatePicker = ({ input: { onChange, value }, minValue, label }) => {

    if (value !== null && value !== undefined && value !== '') {
        if (new Date(value) < new Date(minValue)) {
            onChange(moment(minValue).format('L'))
        }
    }

    return <TextField
        type="date"
        label={label}
        selected={moment(value).format('L')}
        value={moment(value).format('YYYY-MM-DD')}
        onChange={onChange}
        inputProps={{
            min: moment(minValue).format('YYYY-MM-DD')
        }}
    />
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

export const renderSelectPeriodicityField = ({
    input,
    label,
    text,
    data,
    meta: { touched, invalid, error },
    children,
    ...custom
}) =>
    <FormControl error={touched && error}>
        <InputLabel htmlFor="age-native-simple">{text}</InputLabel>
        <Select
            native
            {...input}
            onBlur={() => input.onBlur()}
            {...custom}
            inputProps={{
                name: text.toLowerCase() + 'Id',
                id: 'age-native-simple'
            }}
        >
            <option value=""></option>
            {data.map(x => <option key={x.value} value={x.value}>{x.label}</option>)}
        </Select>
        {renderFieldError({ touched, error })}
    </FormControl>


export const renderMultiselect = ({ input, data, valueField, textField, placeholder,
    meta: { touched, invalid, error } }) =>
    <>
        <Multiselect {...input}
            onBlur={() => input.onBlur()}
            value={input.value || []}
            data={data}
            valueField={valueField}
            textField={textField}
            placeholder={placeholder}
        />
        {renderFieldError({ touched, error })}
    </>

export const renderTextArea = ({
    label,
    defaultValue,
    input,
    rows,
    meta: { touched, invalid, error },
    ...custom
}) => (
        <TextField
            label={label}
            defaultValue={defaultValue}
            multiline
            rows="4"
            fullWidth
            {...input}
            error={touched && invalid}
            helperText={touched && error}
            variant="outlined"
        />)

export const renderTextField = ({
    label,
    defaultValue,
    input,
    inputProps,
    rows,
    fullWidth,
    meta: { touched, invalid, error },
    ...custom
}) => (
        <TextField
            rows={rows}
            fullWidth={fullWidth === undefined ? true : false}
            label={label}
            placeholder={label}
            error={touched && invalid}
            defaultValue={defaultValue}
            value={defaultValue}
            inputProps={inputProps}
            helperText={touched && error}
            {...input}
            {...custom}
        />
    )


export const renderSelectField = ({
    input,
    label,
    meta: { touched, invalid, error },
    children,
    ...custom
}) => (
        <FormControl error={touched && error}>
            <InputLabel htmlFor="age-native-simple">{label}</InputLabel>
            <Select
                fullWidth
                native
                error={touched && invalid}
                helperText={touched && error}
                {...input}
                {...custom}
                inputProps={{
                    name: { label },
                    id: 'age-native-simple'
                }}
            >
                {children}
            </Select>
            {renderFieldError({ touched, error })}
        </FormControl>
    )

export const renderCheckbox = ({ input, label }) => (
    <div>
        <FormControlLabel
            control={
                <Checkbox
                    checked={input.value ? true : false}
                    onChange={input.onChange}
                />
            }
            label={label}
        />
    </div>
)

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
        if (key == "") {
            result = { ...result, _error: value }
        }
        else {
            result = { ...result, [key]: value }
        }
    }
    return result;
}
