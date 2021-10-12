import React, { Component } from "react";
import { Grid, List, ListItem, ListItemText } from "@material-ui/core";

export class AdditionalInfoForm extends Component {


    render() {
        const {
            values: { firstName, lastName, city , country},
        } = this.props;
        return (
            <>
                <div style={{ width: "97%", padding: "10px" }}>
                    <Grid container spacing={3}>


                        <Grid item sm={6}>
                            <h1 style={{ fontSize: 20 }}>Step 5: Confirm your user data.</h1>
                        </Grid>
                        <Grid item xs={5}></Grid>
                        <List>
                            <ListItem>
                                <ListItemText primary="First Name" secondary={firstName} />
                            </ListItem>
                            <ListItem>
                                <ListItemText primary="Last Name" secondary={lastName} />
                            </ListItem>
                            <ListItem>
                                <ListItemText primary="Email" secondary={lastName} />
                            </ListItem>
                            <ListItem>
                                <ListItemText primary="Country" secondary={country} />
                            </ListItem>
                            <ListItem>
                                <ListItemText primary="City" secondary={city} />
                            </ListItem>
                            <ListItem>
                                <ListItemText primary="Gender" secondary={lastName} />
                            </ListItem>
                        </List>
                    </Grid>
                </div>
            </>
        );
    }
}

export default AdditionalInfoForm;
