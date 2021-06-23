import React from "react";
import FormControl from '@material-ui/core/FormControl';
import RadioGroup from '@material-ui/core/RadioGroup';
import { renderFieldError } from '.';

export default ({ input, label, children, meta: { error, touched }, ...rest }) => {
    return (
        <FormControl>
            <RadioGroup {...input} {...rest}>
                {children}
            </RadioGroup>
            {renderFieldError({ touched, error })}
        </FormControl>
    );
}
