import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { Field } from 'redux-form';
import React from 'react';
import { Chip, InputAdornment, TextField } from '@material-ui/core';
import { Autocomplete } from '@material-ui/lab';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import {
    deleteOrganizerFromSelected,
    setSelectedOrganizers,
    fetchOrganizers
} from '../../../../../actions/events/filter/organizer-filter';

const organizerTextField = field => (
    <TextField
        {...field}
        variant="outlined"
        placeholder="Search by name"
        InputProps={{
            ...field.InputProps,
            startAdornment: (
                <InputAdornment position="start">
                    <i className="fas fa-search" />
                </InputAdornment>
            )
        }}
    />
);

const OrganizerFilter = ({ dispatch, organizers, selectedOrganizers }) => {
    const classes = useOrganizerFilterStyles();

    const updateOrganizers = (event, username) => {
        dispatch(fetchOrganizers(`?KeyWord=${username}`));
    };

    const updateSelectedOrganizers = (event, value) => {
        dispatch(setSelectedOrganizers(value));
    };

    const deleteOrganizer = organizer => {
        return () => {
            dispatch(deleteOrganizerFromSelected(organizer));
        };
    };

    const clear = () => updateSelectedOrganizers(null, []);

    return (
        <FilterExpansionPanel
            title="Organizer"
            onClearClick={clear}
            clearDisabled={!selectedOrganizers.length}
        >
            <div className={classes.wrapper}>
                <Autocomplete
                    id="organizer-autocomplete"
                    className={classes.fullWidth}
                    multiple={true}
                    disableClearable={true}
                    value={selectedOrganizers}
                    options={organizers}
                    getOptionLabel={option => option.username}
                    getOptionSelected={(option, value) => option.id === value.id}
                    onChange={updateSelectedOrganizers}
                    onInputChange={updateOrganizers}
                    renderInput={params => (
                        <Field
                            {...params}
                            name="organizerAutocomplete"
                            component={organizerTextField}
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
            </div>
        </FilterExpansionPanel>
    );
};

const mapStateToProps = state => state.eventsFilter.organizerFilter;

export default connect(mapStateToProps)(OrganizerFilter);
