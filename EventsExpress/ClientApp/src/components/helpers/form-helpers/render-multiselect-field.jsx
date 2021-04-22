import React from "react";
import Multiselect from 'react-widgets/lib/Multiselect';
import { renderFieldError } from './';

export default ({ input, data, valueField, textField, placeholder,
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
