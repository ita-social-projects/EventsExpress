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
          selected={moment(value).format("L")}
          value={value ? moment(value).format("L") : undefined}
          autoOK={true}
          format="DD-MM-YYYY"
          error={touched && invalid}
          helperText={touched && error}
          onChange={onChange}
          disabled={disabled}
          minDate={minValue ? moment(minValue) : undefined}
          maxDate={maxValue ? moment(maxValue) : undefined}
        />
      </MuiPickersUtilsProvider>
    </Fragment>
  );
};
