import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { change, Field, getFormValues } from 'redux-form';
import React, { useEffect } from 'react';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import OrganizerAutocomplete from './organizer-autocomplete';
import { fetchUsers } from '../../../../../actions/events/filter/users-data';

const OrganizerFilter = ({ organizers, formValues, ...props }) => {
    const classes = useOrganizerFilterStyles();
    const clear = () => props.change([]);

    useEffect(() => {
        props.fetchUsers('');
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
            </div>
        </FilterExpansionPanel>
    );
};

const mapStateToProps = state => ({
    organizers: state.eventsFilter.users,
    formValues: getFormValues('filter-form')(state)
});

const mapDispatchToProps = dispatch => ({
    fetchUsers: filter => dispatch(fetchUsers(filter)),
    change: value => dispatch(change('filter-form', 'organizers', value))
});

export default connect(mapStateToProps, mapDispatchToProps)(OrganizerFilter);
