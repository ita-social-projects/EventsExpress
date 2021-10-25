import React, { Fragment } from "react";
import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker,
  DatePicker,
} from "@material-ui/pickers";
import MomentUtils from "@date-io/moment";
import moment from "moment";
export default ({
  input: { onChange, value },
  meta: { touched, invalid, error },
  minValue,
  maxValue,
  label,
  disabled,
}) => {
  return (
    <Fragment>
      <MuiPickersUtilsProvider libInstance={moment} utils={MomentUtils}>
        <DatePicker
          fullWidth
          label={label}
          value={value ? moment(value) : null}
          format="DD-MM-YYYY"
          error={touched && invalid}
          helperText={touched && error}
          onChange={onChange}
          disabled={disabled}
          minDate={minValue ? moment(minValue) : null}
          minDateMessage
          maxDate={maxValue ? moment(maxValue) : null}
          maxDateMessage

        />
      </MuiPickersUtilsProvider>
    </Fragment>
  );
};
