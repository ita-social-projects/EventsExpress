import React from "react";
import PhoneInput from 'react-phone-number-input';
import 'react-phone-number-input/style.css';
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import { renderFieldError } from './';

const useStyles = makeStyles((theme) => ({
    formControl: {
        minWidth: 210,
    },
}));

export default ({ input, label, meta: { touched, error, invalid }, children, ...custom }) => {
    const classes = useStyles();
    return (
        <div>
            <InputLabel>{label}</InputLabel>
            <PhoneInput
                {...input}
                international
                countryCallingCodeEditable={false}
                defaultCountry="UA"
                value={input.value}
                onChange={input.onChange}
                error={touched && invalid}
                {...custom}
            />
            {renderFieldError({ touched, error })}
        </div>
    );
}
