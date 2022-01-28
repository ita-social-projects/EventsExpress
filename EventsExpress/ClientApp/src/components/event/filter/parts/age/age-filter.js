import React from 'react';
import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { renderCheckbox } from '../../../../helpers/form-helpers';
import { connect } from 'react-redux';
import { change, Field, getFormValues } from 'redux-form';

const AgeFilter = ({ formValues, ...props }) => {
    const clear = () => props.changeAll(false);

    return (
        <FilterExpansionPanel
            title="Age"
            onClearClick={clear}
            clearDisabled={!formValues.withChildren && !formValues.onlyAdult}
        >
            <div>
                <Field
                    type="checkbox"
                    label="With children"
                    name="withChildren"
                    component={renderCheckbox}
                />
                <Field
                    type="checkbox"
                    label="Only adult"
                    name="onlyAdult"
                    component={renderCheckbox}
                />
            </div>
        </FilterExpansionPanel>
    );
};

const mapStateToProps = state => ({
    formValues: getFormValues('filter-form')(state)
});

const mapDispatchToProps = dispatch => ({
    changeAll: value => {
        for (let fieldName of ['withChildren', 'onlyAdult']) {
            dispatch(change('filter-form', fieldName, value));
        }
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(AgeFilter);
