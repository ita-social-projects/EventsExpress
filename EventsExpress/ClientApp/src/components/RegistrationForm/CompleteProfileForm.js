import React, { Component, useState } from "react";
import {
  TextField,
  Grid,
  Avatar,
  IconButton,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
} from "@material-ui/core";
import DateMomentUtils from "@date-io/moment";
import {
  KeyboardDatePicker,
  DatePicker,
  MuiPickersUtilsProvider,
} from "@material-ui/pickers";

export class CompleteProfileForm extends Component {
  render() {
    const { values, handleChange } = this.props;
    return (
      <>
        <div style={{ width: "97%", padding: "10px" }}>
          <Grid container spacing={3}>
            <Grid item sm={6}>
              <h1 style={{ fontSize: 20 }}>Step 2: Complete your profile. </h1>
            </Grid>
            <Grid item sm={12}>
              <IconButton>
                <Avatar
                  src="/images/example.jpg"
                  style={{
                    margin: "15px",
                    width: "90px",
                    height: "90px",
                  }}
                />
              </IconButton>
            </Grid>
            <Grid item sm={3}>
              <TextField
                variant="outlined"
                placeholder="Enter Your First Name"
                label="First Name"
                onChange={handleChange("firstName")}
                defaultValue={values.firstName}
                margin="normal"
                fullWidth
              />
            </Grid>
            <Grid item sm={3}>
              <TextField
                variant="outlined"
                placeholder="Enter Your Last Name"
                label="Last Name"
                onChange={handleChange("lastName")}
                defaultValue={values.lastName}
                margin="normal"
                fullWidth
              />
            </Grid>
            <Grid item sm={2}></Grid>
            <Grid item sm={4}>

            </Grid>
            <Grid item xs={3}>
              <TextField
                variant="outlined"
                placeholder="Enter Your Contry"
                label="Country"
                onChange={handleChange("country")}
                defaultValue={values.country}
                margin="normal"
                fullWidth
              />
            </Grid>
            <Grid item xs={3}>
              <TextField
                variant="outlined"
                placeholder="Enter Your City"
                label="City"
                onChange={handleChange("city")}
                defaultValue={values.city}
                margin="normal"
                fullWidth
              />
            </Grid>
            <Grid item sm={3}></Grid>
            <Grid item sm={3}>
              <FormControl
                variant="filled"
                defaultValue={values.gender}
                margin="normal"
                fullWidth
              >
                <InputLabel>Gender</InputLabel>
                <Select onChange={handleChange("gender")}>
                  <MenuItem value={1}>Male</MenuItem>
                  <MenuItem value={2}>Female</MenuItem>
                  <MenuItem value={3}>Other</MenuItem>
                </Select>
              </FormControl>
            </Grid>
          </Grid>
        </div>
      </>
    );
  }
}

export default CompleteProfileForm;
