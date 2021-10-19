import React, { Component } from "react";
import { Grid, List, ListItem, ListItemText, Button } from "@material-ui/core";
import { Field, reduxForm, getFormValues } from "redux-form";
import { connect } from "react-redux";
import validate from "./validate";

const ConfirmForm = (props) => {
  const { handleSubmit, pristine, previousPage, submitting } = props;
  return (
    <>
      <div style={{ width: "97%", padding: "10px" }}>
        <form onSubmit={handleSubmit}>
          <Grid container spacing={3}>
            <Grid item sm={7}>
              <h1 style={{ fontSize: 20 }}>Step 5: Confirm your user data.</h1>
            </Grid>
            <Grid item sm={5} />
            <Grid item xs={4}>
              <List>
                <ListItem>
                  <ListItemText primary="Email" />
                </ListItem>
                <ListItem>
                  <ListItemText
                    primary="First Name"
                    secondary={props.formValues.firstName}
                  />
                </ListItem>
                <ListItem>
                  <ListItemText
                    primary="Last Name"
                    secondary={props.formValues.lastName}
                  />
                </ListItem>
                <ListItem>
                  <ListItemText
                    primary="Birth Date"
                    secondary={props.formValues.birthDate}
                  />
                </ListItem>
              </List>
            </Grid>
            <Grid item xs={3}>
              <List>
                <ListItem>
                  <ListItemText
                    primary="Gender"
                    secondary={gendersArray[props.formValues.gender]}
                  />
                </ListItem>
                <ListItem>
                  <ListItemText primary="Country" secondary={props.formValues.country} />
                </ListItem>
                <ListItem>
                  <ListItemText primary="City" secondary={props.formValues.city} />
                </ListItem>
              </List>
            </Grid>
            <Grid item xs={5}>
              <h5>Some step 3/4 data</h5>
            </Grid>

            <Grid item sm={12} justify="center">
              <Button
                type="button"
                className="previous"
                onClick={previousPage}
                color="primary"
                variant="text"
                size="large"
              >
                Back
              </Button>
              <Button
                type="submit"
                className="next"
                disabled={pristine || submitting}
                color="primary"
                variant="contained"
                size="large"
              >
                Confirm
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </>
  );
};

const gendersArray = ["", "Male", "Female", "Other"];

const mapStateToProps = (state) => {
  return {
    initialValues: {
      firstName: state.registereFields,
      lastName: state.registereFields ,
      birthday: state.registereFields,
      country: state.registereFields,
      city: state.registereFields,
      gender: state.registereFields,
    },
    formValues: getFormValues("registrationForm")(state),
  };
};

export default connect(mapStateToProps)(
  reduxForm({
    form: "registrationForm",
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
  })(ConfirmForm)
);
