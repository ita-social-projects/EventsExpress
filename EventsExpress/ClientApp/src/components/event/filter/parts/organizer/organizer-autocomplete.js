import { Autocomplete } from '@material-ui/lab';
import { Chip, InputAdornment, TextField } from '@material-ui/core';
import React from 'react';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import { fetchUsers } from '../../../../../actions/events/filter/users-data';
import { useDelay } from './use-delay';

export const OrganizerAutocomplete = ({ input, options, fetchUsers }) => {
    const [username, setUsername] = useDelay(username => {
        fetchUsers(`?KeyWord=${username}`);
    }, '');
    const classes = useOrganizerFilterStyles();
    const onChange = (event, value) => input.onChange(value);
    const updateUsername = event => setUsername(event.target.value);
    const deleteOrganizer = organizerToDelete => {
        return () => {
            input.onChange(input.value.filter(organizer => organizer.id !== organizerToDelete.id));
        };
    };

    return (
        <>
            <Autocomplete
                id="organizer-autocomplete"
                className={classes.fullWidth}
                multiple={true}
                disableClearable={true}
                value={input.value}
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
            <div className={classes.chips}>
                {input.value.map(organizer => (
                    <Chip
                        key={organizer.id}
                        label={organizer.username}
                        color="secondary"
                        onDelete={deleteOrganizer(organizer)}
                    />
                ))}
            </div>
        </>
    );
};

const mapDispatchToProps = dispatch => ({
    fetchUsers: filter => dispatch(fetchUsers(filter))
});

export default connect(null, mapDispatchToProps)(OrganizerAutocomplete);
