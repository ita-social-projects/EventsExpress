import { Autocomplete } from '@material-ui/lab';
import { Chip, InputAdornment, TextField } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import { fetchUsers } from '../../../../../actions/events/filter/users-data';
import { useDelay } from './delay-hook';

export const OrganizerAutocomplete = ({ input, options, ...props }) => {
    const [username, setUsername] = useDelay(delayedUsername => {
        props.fetchUsers(`?KeyWord=${delayedUsername}`);
    }, '');
    const [selectedOrganizers, setSelectedOrganizers] = useState([]);
    const classes = useOrganizerFilterStyles();

    const updateUsername = event => setUsername(event.target.value);

    const updateOrganizers = (event, values) => {
        const last = values[values.length - 1].id;
        setSelectedOrganizers(
            selectedOrganizers.concat(
                options.find(organizer => organizer.id === last)
            )
        );

        input.onChange(
            input.value.concat(last)
        );
    };

    const deleteOrganizer = organizerToDelete => {
        return () => {
            setSelectedOrganizers(
                selectedOrganizers.filter(organizer => organizer.id !== organizerToDelete.id)
            );

            input.onChange(
                input.value.filter(id => id !== organizerToDelete.id)
            );
        };
    };

    useEffect(() => {
        const inputErased = input.value.length === 0;
        if (inputErased) {
            setSelectedOrganizers([]);
        }
    }, [input.value]);

    return (
        <>
            <Autocomplete
                id="organizer-autocomplete"
                className={classes.fullWidth}
                multiple={true}
                disableClearable={true}
                value={input.value}
                options={options.filter(organizer => !input.value.includes(organizer.id))}
                getOptionLabel={option => option.username}
                getOptionSelected={(option, value) => option.id === value}
                onChange={updateOrganizers}
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
                {selectedOrganizers.map(organizer => (
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
