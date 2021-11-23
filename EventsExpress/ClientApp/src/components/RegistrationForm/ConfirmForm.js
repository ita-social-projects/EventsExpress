import React from "react";
import { Grid, List, ListItem, ListItemText, Button } from "@material-ui/core";
import { reduxForm, getFormValues } from "redux-form";
import { connect } from "react-redux";

import moment from "moment";


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
            <Grid item xs={6}>
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

                    secondary={
                      props.formValues.birthDate
                        ? moment(props.formValues.birthDate).format("DD-MM-YYYY")
                        : "Not entered."
                    }


                  />
                </ListItem>
              </List>
            </Grid>
            <Grid item xs={6}>
                <List>
                <ListItem>
                  <ListItemText
                    primary="Gender"
                    secondary={gendersArray[props.formValues.gender]}
                  />
                </ListItem>
                <ListItem>
                  <ListItemText
                    primary="Country"
                    secondary={props.formValues.country}
                  />
                </ListItem>
                <ListItem>
                  <ListItemText
                    primary="City"
                    secondary={props.formValues.city}
                  />
                </ListItem>
                </List>
                </Grid>
                <Grid item xs={6}>
                <List>
                    <ListItem>
                        <ListItemText primary="Parent status:" />
                    </ListItem>
                    <ListItem>
                        <ListItemText secondary={props.formValues.parentstatus ? "No Kids" : "Kids"} />
                    </ListItem>

                <ListItem>
                    <ListItemText primary="Reasons for using the site:"/>
                </ListItem>

                <ListItem>
                    <AddReasonsForUsingTheSite number={props.formValues.reasonsForUsingTheSite[0]}></AddReasonsForUsingTheSite>
                    <AddReasonsForUsingTheSite number={props.formValues.reasonsForUsingTheSite[1]}></AddReasonsForUsingTheSite>
                    <AddReasonsForUsingTheSite number={props.formValues.reasonsForUsingTheSite[2]}></AddReasonsForUsingTheSite>
                </ListItem>
                
                <ListItem>
                    <ListItemText primary="Event type:" />
                </ListItem>

                <ListItem>
                    <AddEventType number={props.formValues.eventType[0]}></AddEventType>
                    <AddEventType number={props.formValues.eventType[1]}></AddEventType>
                    <AddEventType number={props.formValues.eventType[2]}></AddEventType>
                    <AddEventType number={props.formValues.eventType[3]}></AddEventType>
                    <AddEventType number={props.formValues.eventType[4]}></AddEventType>
                </ListItem>

                <ListItem>
                    <ListItemText primary="Relationship status:" />
                </ListItem>

                <ListItem>
                    <ListItemText secondary={props.formValues.relationshipstatus ? "In a relationship" : "Single"} />
                </ListItem>

                <ListItem>
                    <ListItemText primary="The type of leisure status:" />
                </ListItem>

                <ListItem>
                    <ListItemText secondary={props.formValues.thetypeofleisure ? "Passive" : "Active"} />
                </ListItem>
              </List>
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
    formValues: getFormValues("registrationForm")(state),
  };
};

function AddReasonsForUsingTheSite(props) {
    const number = props.number;

    const project = () => {
        switch (number) {
            case 0:
                return <ListItem>
                    <ListItemText secondary={"Develop a skill"} />
                </ListItem>;
            case 1:
                return <ListItem>
                    <ListItemText secondary={"Meet people like me"} />
                </ListItem>;
            case 2:
                return <ListItem>
                    <ListItemText secondary={"Be more active"} />
                </ListItem>;
            default:
                return null;
        }
    }

    return (<div>{project()} </div>);
}

function AddEventType(props) {
    const number = props.number;

    const project = () => {
        switch (number) {
            case 0:
                return <ListItem>
                    <ListItemText secondary={"Online"} />
                </ListItem>;
            case 1:
                return <ListItem>
                    <ListItemText secondary={"Offline"} />
                </ListItem>;
            case 2:
                return <ListItem>
                    <ListItemText secondary={"Free"} />
                </ListItem>;
            case 3:
                return <ListItem>
                    <ListItemText secondary={"Paid"} />
                </ListItem>;
            case 4:
                return <ListItem>
                    <ListItemText secondary={"Near me"} />
                </ListItem>;
            case 5:
                return <ListItem>
                    <ListItemText secondary={"Any distance"} />
                </ListItem>;
            default:
                return null;
        }
    }

    return (<div>{project()} </div>);
}

export default connect(mapStateToProps)(
  reduxForm({
    form: "registrationForm",
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
  })(ConfirmForm)
);
