import React from 'react';

import TextField from "@material-ui/core/TextField";

export const renderTextField = ({
    input,
    label,
    meta: { touched, error },
    ...custom
  }) => (
    <TextField
      fullWidth
      hintText={label}
      label={label}
      errorText={touched && error}
      {...input}
      {...custom}
    />
  );