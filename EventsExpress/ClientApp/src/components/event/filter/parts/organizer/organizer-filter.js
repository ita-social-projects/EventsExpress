import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { change, Field, getFormValues } from 'redux-form';
import React from 'react';
import { useOrganizerFilterStyles } from './organizer-filter-styles';
import { connect } from 'react-redux';
import OrganizerAutocomplete from './organizer-autocomplete';

const OrganizerFilter = ({ organizers, formValues, ...props }) => {
    const classes = useOrganizerFilterStyles();
    const clear = () => props.change([]);

    return (
        <FilterExpansionPanel
            title="Organizer"
            onClearClick={clear}
            clearDisabled={!formValues.organizers.length}
            clearButton={true}
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
    change: value => dispatch(change('filter-form', 'organizers', value))
});

export default connect(mapStateToProps, mapDispatchToProps)(OrganizerFilter);
