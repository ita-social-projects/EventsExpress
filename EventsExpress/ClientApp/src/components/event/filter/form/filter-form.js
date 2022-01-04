import { Button, Icon, IconButton } from '@material-ui/core';
import React from 'react';
import { useFilterStyles } from '../filter-styles';
import { reduxForm } from 'redux-form';
import { GreenButton } from './green-button';
import OrganizerFilter from '../parts/organizer/organizer-filter';
import { applyFilters } from '../../../../actions/events/filter/actions';

const FilterForm = ({ dispatch, handleSubmit, toggleOpen, reset }) => {
    const classes = useFilterStyles();
    const onSubmit = formValues => dispatch(applyFilters(formValues));

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
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
                    <GreenButton type="submit">Apply</GreenButton>
                </div>
            </div>
            <OrganizerFilter />
        </form>
    );
};

export default reduxForm({
    form: 'filter-form',
    initialValues: {
        organizers: []
    }
})(FilterForm);
