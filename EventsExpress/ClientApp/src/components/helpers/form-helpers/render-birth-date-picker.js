import React, { Fragment } from "react";
import { MuiPickersUtilsProvider, KeyboardDatePicker } from "@material-ui/pickers";
import MomentUtils from "@date-io/moment";
import moment from "moment";
export default ({
  input: { onChange, value, ...inputProps },
  meta: { touched, invalid, error },
  minValue,
  maxValue,
  label,
  disabled,
}) => {
  return (
    <Fragment>
      <MuiPickersUtilsProvider libInstance={moment} utils={MomentUtils}>
        <KeyboardDatePicker
      {...inputProps}
          label={label}
          value={value ? moment(value) : undefined}
          autoOk
          format="DD-MM-YYYY"
          error={touched && error && invalid}
          helperText={touched && error}
          onChange={onChange}
          disabled={disabled}
          minDate={minValue ? moment(minValue) : undefined}
          minDateMessage
          maxDate={maxValue ? moment(maxValue) : undefined}
          minDateMessage
        />
      </MuiPickersUtilsProvider>
    </Fragment>
  );
};
