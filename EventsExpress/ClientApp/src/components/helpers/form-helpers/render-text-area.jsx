import React from "react";
import TextField from "@material-ui/core/TextField";

export default ({ input, label, defaultValue, rows, meta: { touched, error, invalid } }) => {
    return (
        <TextField
            label={label}
            defaultValue={defaultValue}
            multiline
            rows="4"
            fullWidth
            {...input}
            error={touched && invalid}
            helperText={touched && error}
            variant="outlined"
        />
    );
}
