import React from "react";
import { Grid, List, ListItem, ListItemText, Button } from "@material-ui/core";
import { reduxForm, getFormValues } from "redux-form";
import { connect } from "react-redux";

import reasonsForUsingTheSiteEnum from '../../constants/reasonsForUsingTheSiteEnum';
import eventTypeEnum from '../../constants/eventTypeEnum';

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
                        <ListItemText secondary={props.formValues.parentstatus} />
                    </ListItem>

                <ListItem>
                    <ListItemText primary="Reasons for using the site:"/>
                </ListItem>

                <ListItem>
                    <AddListItemText value={props.formValues.isBeMoreActive} stringValue={reasonsForUsingTheSiteEnum.BeMoreActive} />
                    <AddListItemText value={props.formValues.isDevelopASkill} stringValue={reasonsForUsingTheSiteEnum.DevelopASkill} />
                    <AddListItemText value={props.formValues.isMeetPeopleLikeMe} stringValue={reasonsForUsingTheSiteEnum.MeetPeopleLikeMe} />
                </ListItem>
                
                <ListItem>
                    <ListItemText primary="Event type:" />
                </ListItem>

                <ListItem>
                    <AddListItemText value={props.formValues.isAnyDistance} stringValue={eventTypeEnum.AnyDistance} />
                    <AddListItemText value={props.formValues.isFree} stringValue={eventTypeEnum.Free} />
                    <AddListItemText value={props.formValues.isNearMe} stringValue={eventTypeEnum.NearMe} />
                    <AddListItemText value={props.formValues.isOffline} stringValue={eventTypeEnum.Offline} />
                    <AddListItemText value={props.formValues.isOnline} stringValue={eventTypeEnum.Online} />
                    <AddListItemText value={props.formValues.isPaid} stringValue={eventTypeEnum.Paid} />
                    </ListItem>

                    <ListItem>
                        <ListItemText primary="Relationship status:" />
                    </ListItem>

                    <ListItem>
                        <ListItemText secondary={props.formValues.relationshipstatus} />
                    </ListItem>

                    <ListItem>
                        <ListItemText primary="The type of leisure status:" />
                    </ListItem>

                    <ListItem>
                        <ListItemText secondary={props.formValues.thetypeofleisure} />
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

function AddListItemText(props) {
    if (props.value) {
        return <ListItem>
            <ListItemText secondary={props.stringValue} />
        </ListItem>
        
    }
    return null
}

export default connect(mapStateToProps)(
  reduxForm({
    form: "registrationForm",
    destroyOnUnmount: false,
    forceUnregisterOnUnmount: true,
  })(ConfirmForm)
);
