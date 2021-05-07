import React from 'react';
import TextField from "@material-ui/core/TextField";
import moment from 'moment';

export default ({ input: { onChange, value }, minValue, maxValue, label }) => {

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
            min: minValue ? moment(minValue).format('YYYY-MM-DD') : null,
            max: maxValue ? moment(maxValue).format('YYYY-MM-DD') : null
        }}
    />
}