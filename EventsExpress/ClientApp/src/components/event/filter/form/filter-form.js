import { Button, Icon, IconButton } from '@material-ui/core';
import React from 'react';
import { useFilterStyles } from '../filter-styles';
import { reduxForm } from 'redux-form';
import { GreenButton } from './green-button';
import CategoryFilter from '../parts/category/category-filter';
import OrganizerFilter from '../parts/organizer/organizer-filter';
import AgeFilter from '../parts/age/age-filter';
import LocationFilter from '../parts/location/location-filter';
import { useFilterActions } from '../filter-hooks';

const FilterForm = ({ handleSubmit, toggleOpen, reset, pristine }) => {
    const classes = useFilterStyles();
    const { applyFilters, resetFilters } = useFilterActions();

    const onSubmit = formValues => {
        applyFilters({ ...formValues });
    };

    const onReset = () => {
        reset();
        resetFilters();
    };

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
                        onClick={onReset}
                    >
                        Reset
                    </Button>
                    <GreenButton type="submit" disabled={pristine}>Apply</GreenButton>
                </div>
            </div>
            <CategoryFilter />
            <OrganizerFilter />
            <LocationFilter />
            <AgeFilter />
        </form>
    );
};

export default reduxForm({
    form: 'filter-form',
    enableReinitialize: true
})(FilterForm);
