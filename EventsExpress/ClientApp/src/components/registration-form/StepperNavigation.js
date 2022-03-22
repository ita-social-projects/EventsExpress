import React, { useContext } from 'react'
import { Grid, Button } from '@material-ui/core';
import { RegisterStepContext } from './RegistrationForm'

const StepperNavigation = ({
    showBack = true,
    showSkip = true,
    hasNext = true,
    confirmWhenSkipping = false,
}) => {
    const { goBack, goToNext } = useContext(RegisterStepContext);

    return (
        <Grid item sm={12}>
            {showBack && (
                <Button
                    type="button"
                    className="previous mx-3"
                    onClick={goBack}
                    color="primary"
                    variant="text"
                    size="large"
                >
                    Back
                </Button>
            )}
            {showSkip && (
                <Button
                    type="submit"
                    className="mx-3"
                    color="primary"
                    variant="text"
                    size="large"
                >
                    {confirmWhenSkipping
                        ? 'Confirm & Finish'
                        : 'Skip & Finish'
                    }
                </Button>
            )}
            {hasNext ? (
                <Button
                    type="button"
                    className="next mx-3"
                    onClick={goToNext}
                    color="primary"
                    variant="contained"
                    size="large"
                >
                    Continue
                </Button>
            ) : (
                <Button
                    type="submit"
                    className="next mx-3"
                    color="primary"
                    variant="contained"
                    size="large"
                >
                    Confirm
                </Button>
            )}
        </Grid>
    );
};

export default StepperNavigation;
