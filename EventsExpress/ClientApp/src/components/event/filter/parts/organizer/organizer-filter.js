import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { Field } from 'redux-form';
import React, { useState } from 'react';
import { Chip, InputAdornment, TextField } from '@material-ui/core';
import { Autocomplete } from '@material-ui/lab';
import { useOrganizerFilterStyles } from './organizer-filter-styles';

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

export const OrganizerFilter = () => {
    const classes = useOrganizerFilterStyles();
    const [selected, setSelected] = useState([]);
    const handleChange = (event, value) => setSelected(value);
    const deleteOrganizer = organizerToDelete => {
        return () => {
            setSelected(selected.filter(organizer => organizer.name !== organizerToDelete.name));
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
                    value={selected}
                    options={[]}
                    getOptionLabel={option => option.name}
                    onChange={handleChange}
                    renderInput={params => (
                        <Field {...params} component={organizerTextField} />
                    )}
                />
                <div className={classes.chips}>
                    {selected.map(organizer => (
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
