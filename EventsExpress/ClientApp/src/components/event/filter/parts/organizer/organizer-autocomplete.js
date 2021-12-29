import { Autocomplete } from '@material-ui/lab';
import { InputAdornment, TextField } from '@material-ui/core';
import React from 'react';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import { getFormValues } from 'redux-form';
import { fetchOrganizers } from '../../../../../actions/events/filter/organizer-filter';

export const OrganizerAutocomplete = ({ dispatch, input, options, formValues }) => {
    const classes = useOrganizerFilterStyles();
    const onChange = (event, value) => input.onChange(value);
    const onInputChange = (event, username) => dispatch(fetchOrganizers(`?KeyWord=${username}`));

    return (
        <Autocomplete
            id="organizer-autocomplete"
            className={classes.fullWidth}
            multiple={true}
            disableClearable={true}
            value={formValues.organizers}
            options={options}
            getOptionLabel={option => option.username}
            getOptionSelected={(option, value) => option.id === value.id}
            onChange={onChange}
            onInputChange={onInputChange}
            renderInput={params => (
                <TextField
                    {...params}
                    variant="outlined"
                    placeholder="Search by name"
                    InputProps={{
                        ...params.InputProps,
                        startAdornment: (
                            <InputAdornment position="start">
                                <i className="fas fa-search" />
                            </InputAdornment>
                        )
                    }}
                />
            )}
        />
    );
};

const mapStateToProps = state => {
    return {
        formValues: getFormValues('filter-form')(state)
    };
};

export default connect(mapStateToProps)(OrganizerAutocomplete);
