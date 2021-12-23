import { Button, Icon, IconButton } from '@material-ui/core';
import React from 'react';
import { useFilterStyles } from './filter-styles';
import { reduxForm } from 'redux-form';
import { green } from '@material-ui/core/colors';
import { withStyles } from '@material-ui/core/styles';

const GreenButton = withStyles({
    root: {
        color: '#fff',
        backgroundColor: green[500],
        '&:hover': {
            backgroundColor: green[700]
        }
    }
})(Button);

const FilterForm = ({ toggleOpen }) => {
    const classes = useFilterStyles();

    return (
        <form className={classes.filterForm}>
            <div className={classes.filterContent}>
                <div className={classes.filterHeader}>
                    <IconButton onClick={toggleOpen}>
                        <Icon className="fas fa-arrow-right" />
                    </IconButton>
                    <h5 className={classes.filterHeading}>Filters</h5>
                    <Button color="secondary">Clear</Button>
                </div>
            </div>
            <div className={classes.filterFooter}>
                <Button variant="outlined" color="secondary">Reset</Button>
                <GreenButton>Apply</GreenButton>
            </div>
        </form>
    );
};

export default reduxForm({
    form: 'filter-form'
})(FilterForm);
