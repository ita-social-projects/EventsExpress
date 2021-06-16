import React from "react";
import FormControl from '@material-ui/core/FormControl';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';
import RadioGroup from '@material-ui/core/RadioGroup';
import { renderFieldError } from '.';

export default ({ input, meta: { error, touched }, ...rest }) => {
    return (
        <FormControl>
            <RadioGroup {...input} {...rest}>
                <FormControlLabel value="0" control={<Radio />} label="Map" />
                <FormControlLabel value="1" control={<Radio />} label="Online" />
            </RadioGroup>
            {renderFieldError({ touched, error })}
        </FormControl>
    );
}
