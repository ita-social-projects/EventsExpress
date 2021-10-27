import React, { Fragment } from "react";
import { MuiPickersUtilsProvider, DatePicker } from "@material-ui/pickers";
import MomentUtils from "@date-io/moment";
import moment from "moment";

export default ({
  input: { onChange, value, defaultValue },
  meta: { touched, invalid, error },
  minValue,
  labelSecond,
  label,
  disabled,
}) => {
  return (
    <div>
      <Fragment>
        <MuiPickersUtilsProvider libInstance={moment} utils={MomentUtils}>
          <DatePicker
            name="dateFrom"
            label={label}
            value={moment(value)}
            defaultValue={defaultValue}
            format="DD-MM-YYYY"
            error={touched && invalid}
            helperText={touched && error}
            onChange={onChange}
            disabled={disabled}
            minDate={new Date()}
            maxDate={minValue ? moment(minValue) : null}
          />
        </MuiPickersUtilsProvider>
      </Fragment>
      <Fragment>
        <MuiPickersUtilsProvider libInstance={moment} utils={MomentUtils}>
          <DatePicker
            name="dateTo"
            label={labelSecond}
            value={moment(value)}
            defaultValue={defaultValue}
            format="DD-MM-YYYY"
            error={touched && invalid}
            helperText={touched && error}
            onChange={onChange}
            disabled={disabled}
            minDate={minValue ? moment(minValue) : null}
          />
        </MuiPickersUtilsProvider>
      </Fragment>
    </div>
  );
};
