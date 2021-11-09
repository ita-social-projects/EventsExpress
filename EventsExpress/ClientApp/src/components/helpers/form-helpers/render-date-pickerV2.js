import React, { Fragment } from "react";
import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker,
} from "@material-ui/pickers";
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
          value={value ? moment(value).format("YYYY-MM-DD") : undefined}
          autoOK
          format="YYYY-MM-DD"
          error={touched && invalid}
          helperText={"YYYY-MM-DD" ||touched && error}
          onChange={onChange}
          disabled={disabled}
          minDate={minValue ? moment(minValue) : undefined}
          maxDate={maxValue ? moment(maxValue) : undefined}
        />
      </MuiPickersUtilsProvider>
    </Fragment>
  );
};
