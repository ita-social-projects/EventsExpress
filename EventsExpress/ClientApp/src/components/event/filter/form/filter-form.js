import { Button, Icon, IconButton } from '@material-ui/core';
import React, { useEffect } from 'react';
import { useFilterStyles } from '../filter-styles';
import { change, reduxForm } from 'redux-form';
import { GreenButton } from './green-button';
import OrganizerFilter from '../parts/organizer/organizer-filter';
import { applyFilters, parseFilters, resetFilters } from '../../../../actions/events/filter/actions';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

const FilterForm = ({ handleSubmit, toggleOpen, reset, pristine, history, ...props }) => {
    const classes = useFilterStyles();
    const onSubmit = formValues => applyFilters(formValues, history);
    const onReset = () => {
        reset();
        resetFilters(history);
    };

    useEffect(() => {
        if (history.location.search) {
            const filtersFromQuery = parseFilters(history.location.search);
            for (const key in filtersFromQuery) {
                props.change(key, filtersFromQuery[key]);
            }
        }
    }, []);

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
        </form>
    );
};

const mapDispatchToProps = dispatch => ({
    change: (field, value) => dispatch(change('filter-form', field, value))
});

export default withRouter(
    connect(null, mapDispatchToProps)(
        reduxForm({
            form: 'filter-form',
            initialValues: {
                organizers: []
            }
        })(FilterForm)
    )
);
