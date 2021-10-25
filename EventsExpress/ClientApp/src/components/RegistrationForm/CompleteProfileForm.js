import React from "react";
import { Grid, Avatar, IconButton, Button } from "@material-ui/core";
import { reduxForm, Field } from "redux-form";
import {
  renderDatePicker,
  renderTextField,
  renderSelectField,
} from "../helpers/form-helpers";
import moment from "moment";

const CompleteProfileForm = (props) => {
  const { handleSubmit } = props;
  return (
    <>
      <div style={{ width: "97%", padding: "10px" }}>
        <form onSubmit={handleSubmit}>
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
              <Field
                name="firstName"
                variant="outlined"
                component={renderTextField}
                type="input"
                label="First Name"
              />
            </Grid>
            <Grid item sm={3}>
              <Field
                name="lastName"
                variant="outlined"
                component={renderTextField}
                type="input"
                label="Last Name"
              />
            </Grid>
            <Grid item sm={3}></Grid>
            <Grid item sm={3}>
              <Field
                name="birthDate"
                label="Birth Date"
                minValue={moment(new Date()).subtract(115, "years")}
                maxValue={moment(new Date()).subtract(15, "years")}
                component={renderDatePicker}
                <Field/>
            </Grid>
            <Grid item xs={3}>
              <Field
                name="country"
                variant="outlined"
                component={renderTextField}
                type="input"
                label="County"
              />
            </Grid>
            <Grid item xs={3}>
              <Field
                name="city"
                variant="outlined"
                component={renderTextField}
                type="input"
                label="City"
              />
            </Grid>
            <Grid item sm={3}></Grid>
            <Grid item sm={3}>
              <Field
                minWidth={140}
                name="gender"
                component={renderSelectField}
                label="Gender"
              >
                <option aria-label="None" value="" />
                <option value="1">Male</option>
                <option value="2">Female</option>
                <option value="3">Other</option>
              </Field>
            </Grid>
            <Grid item sm={12} justify="space-around">
              <Button
                type="submit"
                className="next"
                color="primary"
                variant="contained"
                size="large"
              >
                Continue
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </>
  );
};

export default reduxForm({
  form: "registrationForm",
  destroyOnUnmount: false,
  forceUnregisterOnUnmount: true,
})(CompleteProfileForm);
