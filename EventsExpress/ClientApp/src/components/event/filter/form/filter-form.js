import { Button, Icon, IconButton } from '@material-ui/core';
import React from 'react';
import { useFilterStyles } from '../filter-styles';
import { reduxForm } from 'redux-form';
import { GreenButton } from './green-button';
import OrganizerFilter from '../parts/organizer/organizer-filter';
import LocationFilter from '../parts/location/location-filter';

const FilterForm = ({ toggleOpen, reset }) => {
    const classes = useFilterStyles();

    return (
        <form>
            <div className={classes.filterHeader}>
                <div className={classes.filterHeaderPart}>
                    <IconButton onClick={toggleOpen}>
                        <Icon className="fas fa-arrow-circle-right" />
                    </IconButton>
                    <h5 className={classes.filterHeading}>Filters</h5>
                </div>
                <div className={classes.filterHeaderPart}>
                    <Button
                        color="secondary"
                        variant="contained"
                        disableElevation={true}
                        onClick={reset}
                    >
                        Reset
                    </Button>
                    <GreenButton>Apply</GreenButton>
                </div>
            </div>
            <OrganizerFilter />
            <LocationFilter />
        </form>
    );
};

export default reduxForm({
    form: 'filter-form',
    initialValues: {
        organizers: [],
        location:{},
    }
})(FilterForm);
