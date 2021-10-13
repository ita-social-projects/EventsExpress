import React, { Fragment, useState } from "react";
import { DatePicker, MuiPickersUtilsProvider } from "@material-ui/pickers";
import DateMomentUtils from "@date-io/moment";

function DatePickerRegForm() {
  const [selectedDate, setSelectedDate] = useState(new Date());
  const handleDateChange = (date) => {
    setSelectedDate(date);
  };

  return (
    <div className="App">
      <MuiPickersUtilsProvider utils={DateMomentUtils}>
        <Fragment>
          <DatePicker
            orientation="landscape"
            margin="normal"
            variant="dialog"
            label="Birth Date"
            format="DD-MM-YYYY"
            helperText="dd/mm/yyyy"
            value={selectedDate}
            onChange={handleDateChange}
          />
        </Fragment>
      </MuiPickersUtilsProvider>
    </div>
  );
}

export default DatePickerRegForm;
