import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { Field } from 'redux-form';
import React from 'react';
import { Chip, InputAdornment, TextField } from '@material-ui/core';
import { Autocomplete } from '@material-ui/lab';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import {
    deleteOrganizerFromSelected,
    setSelectedOrganizers
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
    const handleChange = (event, value) => dispatch(setSelectedOrganizers(value));
    const deleteOrganizer = organizer => {
        return () => {
            dispatch(deleteOrganizerFromSelected(organizer))
        };
    };

    return (
        <FilterExpansionPanel title="Organizer">
            <div className={classes.wrapper}>
                <Autocomplete
                    id="organizer-autocomplete"
                    className={classes.fullWidth}
                    multiple={true}
                    disableClearable={true}
                    value={selectedOrganizers}
                    options={organizers}
                    getOptionLabel={option => option.name}
                    onChange={handleChange}
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
                            label={organizer.name}
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
