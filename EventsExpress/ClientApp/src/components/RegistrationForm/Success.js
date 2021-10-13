import React, { Component } from "react";
import "./CheckMarkAnimation.css";
import { Button, Grid } from "@material-ui/core";
import { Link } from "react-router-dom";

export class Success extends Component {
  render() {
    return (
      <>
        <Grid item sm={12}>
          <h1 style={{ fontSize: 25 }}>Successful registration!</h1>
        </Grid>
        <div>
          <svg class="checkmark" viewBox="0 0 52 52">
            <circle
              class="checkmark__circle"
              cx="26"
              cy="26"
              r="25"
              fill="none"
            />
            <path
              class="checkmark__check"
              fill="none"
              d="M14.1 27.2l7.1 7.2 16.7-16.8"
            />
          </svg>
        </div>

        <br />
        <br />
        <br />
        <Grid container spacing={3}>
          <Grid item sm={3} />
          <Grid item sm={3}>
            <Button
              component={Link}
              to="/profile"
              color="primary"
              variant="contained"
              size="large"
            >
              Profile
            </Button>
          </Grid>
          <Grid item sm={3}>
            <Button
              component={Link}
              to="/home/events"
              color="primary"
              variant="contained"
              size="large"
            >
              Events
            </Button>
          </Grid>
          <Grid item sm={3} />
        </Grid>
      </>
    );
  }
}

export default Success;
