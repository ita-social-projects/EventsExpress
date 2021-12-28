import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { Field } from 'redux-form';
import React, { useState } from 'react';
import { InputAdornment, TextField } from '@material-ui/core';
import { Autocomplete } from '@material-ui/lab';
import { useOrganizerFilterStyles } from './organizer-filter-styles';

const organizerTextField = ({
    label,
    input,
    meta: { touched, invalid, error },
    ...custom
}) => (
    <TextField
        {...input}
        {...custom}
        variant="outlined"
        placeholder="Search by name"
        InputProps={{
            ...custom.InputProps,
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

    return (
        <FilterExpansionPanel title="Organizer">
            <div className={classes.wrapper}>
                <Autocomplete
                    style={{ width: '100%' }}
                    id="organizer-autocomplete"
                    options={[]}
                    getOptionLabel={option => option.name}
                    onChange={handleChange}
                    multiple={true}
                    disableClearable={true}
                    value={selected}
                    renderInput={params => (
                        <Field {...params} component={organizerTextField} />
                    )}
                />
            </div>
        </FilterExpansionPanel>
    );
};
