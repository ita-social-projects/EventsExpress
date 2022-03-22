import React from 'react';
import { Button, Grid } from '@material-ui/core';
import { Link } from 'react-router-dom';
import CheckMark from './checkmark/CheckMark';

const SuccessPage = () => (
    <>
        <Grid item sm={12}>
            <h1>Successful registration!</h1>
        </Grid>
        <CheckMark />
        <div className="stepper-submit">
            <Grid item sm={12}>
                <Button
                    component={Link}
                    className="mx-4"
                    to="/editProfile"
                    color="primary"
                    variant="contained"
                    size="large"
                >
                    Profile
                </Button>
                <Button
                    component={Link}
                    className="mx-4"
                    to="/home/events"
                    color="primary"
                    variant="contained"
                    size="large"
                >
                    Events
                </Button>
            </Grid>
        </div>
    </>
);

export default SuccessPage;
