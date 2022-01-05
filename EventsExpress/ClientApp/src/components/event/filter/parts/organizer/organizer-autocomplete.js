import { Autocomplete } from '@material-ui/lab';
import { InputAdornment, TextField } from '@material-ui/core';
import React from 'react';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import { getFormValues } from 'redux-form';
import { fetchUsers } from '../../../../../actions/events/filter/users-data';
import { useDelay } from './use-delay';

export const OrganizerAutocomplete = ({ input, options, formValues, fetchUsers }) => {
    const [username, setUsername] = useDelay(username => {
        fetchUsers(`?KeyWord=${username}`);
    }, '');
    const classes = useOrganizerFilterStyles();
    const onChange = (event, value) => input.onChange(value);
    const updateUsername = event => setUsername(event.target.value);

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
                    inputProps={{
                        ...params.inputProps,
                        maxLength: 50
                    }}
                    value={username}
                    onChange={updateUsername}
                />
            )}
        />
    );
};

const mapStateToProps = state => ({
    formValues: getFormValues('filter-form')(state)
});

const mapDispatchToProps = dispatch => ({
    fetchUsers: filter => dispatch(fetchUsers(filter))
})

export default connect(mapStateToProps, mapDispatchToProps)(OrganizerAutocomplete);
