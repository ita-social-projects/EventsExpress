import React, { Component } from 'react';
import { Button, Grid } from '@material-ui/core';
import { Link } from 'react-router-dom';
import CheckMark from './checkmark/checkmark';

// TODO: extract styles to hooks
const SuccessPage = () => (
    <>
        <Grid item sm={12}>
            <h1 style={{ fontSize: 25 }}>Successful registration!</h1>
        </Grid>
        <CheckMark />
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

export default SuccessPage;
