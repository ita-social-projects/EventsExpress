import React from "react";
import FormControl from '@material-ui/core/FormControl';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';
import RadioGroup from '@material-ui/core/RadioGroup';

export default ({ input, label, ...rest }) => {
    return (
        <FormControl>
            <RadioGroup {...input} {...rest}>
                <FormControlLabel value="blocked" control={<Radio />} label="Blocked" />
                <FormControlLabel value="active" control={<Radio />} label="Active" />
                <FormControlLabel value="all" control={<Radio />} label="All" />
            </RadioGroup>
        </FormControl>
    );
}
