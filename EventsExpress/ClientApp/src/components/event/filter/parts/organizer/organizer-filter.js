import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { change, Field, getFormValues } from 'redux-form';
import React, { useEffect } from 'react';
import { Chip } from '@material-ui/core';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import OrganizerAutocomplete from './organizer-autocomplete';
import { fetchUsers } from '../../../../../actions/events/filter/users-data';

const OrganizerFilter = ({ dispatch, organizers, formValues }) => {
    const classes = useOrganizerFilterStyles();

    const deleteOrganizer = organizerToDelete => {
        return () => {
            dispatch(change(
                'filter-form',
                'organizers',
                formValues.organizers.filter(organizer => organizer.id !== organizerToDelete.id)));
        };
    };

    const clear = () => dispatch(change('filter-form', 'organizers', []));

    useEffect(() => {
        dispatch(fetchUsers(''));
    }, []);

    return (
        <FilterExpansionPanel
            title="Organizer"
            onClearClick={clear}
            clearDisabled={!formValues.organizers.length}
        >
            <div className={classes.wrapper}>
                <Field
                    name="organizers"
                    options={organizers}
                    component={OrganizerAutocomplete}
                />
                <div className={classes.chips}>
                    {formValues.organizers.map(organizer => (
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

const mapStateToProps = state => {
    return {
        organizers: state.eventsFilter.users,
        formValues: getFormValues('filter-form')(state)
    };
};

export default connect(mapStateToProps)(OrganizerFilter);
