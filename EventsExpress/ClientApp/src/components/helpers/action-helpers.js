import React from "react";
import TextField from "@material-ui/core/TextField";
import Multiselect from 'react-widgets/lib/Multiselect';
import DatePicker from 'react-datepicker';
import 'react-widgets/dist/css/react-widgets.css';
import "react-datepicker/dist/react-datepicker.css";
import Select from '@material-ui/core/Select';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import FormHelperText from '@material-ui/core/FormHelperText';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';
import RadioGroup from '@material-ui/core/RadioGroup';

export const buildValidationState = async (responseData) => {
    let result = {};
    let x = await responseData.json();

    for (const [key, value] of Object.entries(x.errors)) {
        if (key == "") {
            result = { ...result, _error: value };
        }
        else {
            result = { ...result, [key]: value };
        }
    }
    return result;
}

export const getErrorMessege = async (responseData) => {
    let x = await responseData.json();

    for (const [key, value] of Object.entries(x.errors)) {
        if (key == "") {
            return `Error : ${value[0]}!`;
        }
        return `Error for ${key}: ${value[0]}!`;
    }
    return "Something went wrong";
}
