import React from "react";
import PhoneInput from 'react-phone-number-input';
import 'react-phone-number-input/style.css';
import InputLabel from '@material-ui/core/InputLabel';
import { renderFieldError } from './';

export default ({ input, label, meta: { touched, error, invalid }, children, ...custom }) => {
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

