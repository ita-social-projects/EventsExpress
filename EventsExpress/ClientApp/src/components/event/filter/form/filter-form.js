import { Button, Icon, IconButton } from '@material-ui/core';
import React from 'react';
import { useFilterStyles } from '../filter-styles';
import { reduxForm } from 'redux-form';
import { GreenButton } from './green-button';
import OrganizerFilter from '../parts/organizer/organizer-filter';
import { applyFilters, resetFilters } from '../../../../actions/events/filter/actions';
import LocationFilter from '../parts/location/location-filter';
import { connect } from 'react-redux';

const FilterForm = ({ handleSubmit, toggleOpen, reset, pristine, ...props }) => {
    const classes = useFilterStyles();
    const onSubmit = formValues => props.applyFilters(formValues);
    const onReset = () => {
        reset();
        props.resetFilters();
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
            <OrganizerFilter />
            <LocationFilter />
        </form>
    );
};

const mapDispatchToProps = dispatch => ({
    applyFilters: filters => dispatch(applyFilters(filters)),
    resetFilters: () => dispatch(resetFilters())
});

export default connect(null, mapDispatchToProps)(
    reduxForm({
        form: 'filter-form',
        initialValues: {
            organizers: [],
            location:{type:null},
        }
    })(FilterForm)
);
