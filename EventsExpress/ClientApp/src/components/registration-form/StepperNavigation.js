import React, { useContext } from 'react'
import { Grid, Button } from '@material-ui/core';
import { RegisterStepContext } from './RegistrationForm'

const StepperNavigation = ({
    showBack = true,
    showSkip = true,
    hasNext = true,
    confirmWhenSkipping = false,
}) => {
    const { goBack, skipToLast } = useContext(RegisterStepContext);

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
                    type="button"
                    className="mx-3"
                    onClick={skipToLast}
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
            <Button
                type="submit"
                className="next mx-3"
                color="primary"
                variant="contained"
                size="large"
            >
                {hasNext
                    ? 'Continue'
                    : 'Confirm'
                }
            </Button>
        </Grid>
    );
};

export default StepperNavigation;
