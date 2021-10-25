import React from 'react';
import TextField from "@material-ui/core/TextField";
import moment from 'moment';

export default ({ input: { onChange, value }, meta: { touched, invalid, error }, 
    minValue, maxValue, label, disabled }) => {

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
        error={touched && invalid}
        helperText={touched && error}
        onChange={onChange}
        disabled={disabled}
        inputProps={{
            min: minValue ? moment(minValue).format('YYYY-MM-DD') : null,
            max: maxValue ? moment(maxValue).format('YYYY-MM-DD') : null
        }}
    />
}