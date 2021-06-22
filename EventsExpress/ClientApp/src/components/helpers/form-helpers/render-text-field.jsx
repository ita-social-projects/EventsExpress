import React from "react";
import TextField from "@material-ui/core/TextField";

export default ({ input, label, inputProps, defaultValue, rows, fullWidth,
    meta: { touched, error, invalid }, ...custom }) => {
    return (
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
    );
}
