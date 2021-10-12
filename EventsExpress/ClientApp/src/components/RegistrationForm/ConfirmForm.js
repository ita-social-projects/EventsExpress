import React, { Component } from "react";
import { Grid, List, ListItem, ListItemText } from "@material-ui/core";

export class AdditionalInfoForm extends Component {
  render() {
    const {
      values: { email, firstName, lastName, city, country, gender, birthDate },
    } = this.props;
    return (
      <>
        <div style={{ width: "97%", padding: "10px" }}>
          <Grid container spacing={3}>
            <Grid item sm={7}>
              <h1 style={{ fontSize: 20 }}>Step 5: Confirm your user data.</h1>
            </Grid>
            <Grid item sm={5}/>
            <Grid item xs={4}>
              <List>
                <ListItem>
                  <ListItemText primary="Email" secondary={email} />
                </ListItem>
                <ListItem>
                  <ListItemText primary="First Name" secondary={firstName} />
                </ListItem>
                <ListItem>
                  <ListItemText primary="Last Name" secondary={lastName} />
                </ListItem>
                <ListItem>
                  <ListItemText primary="Birth Date" secondary={birthDate} />
                </ListItem>
              </List>
            </Grid>
            <Grid item xs={3}>
              <List>
                <ListItem>
                  <ListItemText primary="Gender" secondary={gender} />
                </ListItem>
                <ListItem>
                  <ListItemText primary="Country" secondary={country} />
                </ListItem>
                <ListItem>
                  <ListItemText primary="City" secondary={city} />
                </ListItem>
              </List>
            </Grid>
            <Grid item xs={5}>
              <h5>Some step 3/4 data</h5>
            </Grid>
          </Grid>
        </div>
      </>
    );
  }
}

export default AdditionalInfoForm;
