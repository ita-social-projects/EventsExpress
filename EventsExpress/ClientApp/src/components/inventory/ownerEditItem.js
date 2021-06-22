import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { renderSelectField, renderTextField } from '../helpers/form-helpers';
import IconButton from "@material-ui/core/IconButton";
import ErrorMessages from '../shared/errorMessage';

const validate = values => {
    const errors = {};
    
    const requiredFields = [
        'itemName',
        'needQuantity',
        'unitOfMeasuring.id'
    ];
    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Required'
        }
    });
    if (values.itemName && values.itemName.length > 30) {
        errors.itemName = `Invalid length: 1 - 30 symbols`;
    }
    if (values.needQuantity && values.needQuantity <= 0) {
        errors.needQuantity = `Can not be 0 or negative`;
    }
    if (!values.unitOfMeasuring.id) {
        errors.unitOfMeasuring = "Required";
    }
    return errors;
}

class OwnerEditItemForm extends Component {

    render() {
        const { initialValues, unitOfMeasuringState, alreadyGet, error } = this.props;
        return (
            <form onSubmit={this.props.handleSubmit} className="form-inline w-100">
                <div className="col col-md-3 d-flex align-items-center">
                    <Field
                        name={`itemName`}
                        type="text"
                        fullWidth={false}
                        label="Item name"
                        component={renderTextField} />
                </div>
                <div className="col">{alreadyGet}</div>
                <div className="col col-md-2 d-flex align-items-center">
                    <Field
                        name="needQuantity"
                        type="number"
                        fullWidth={false}
                        label="Item count"
                        component={renderTextField} />
                </div>
                <div className="col col-md-2 d-flex align-items-center ">
                    <Field
                        name={'unitOfMeasuring.id'}
                        fullWidth={false}
                        component={renderSelectField}>
                        <option></option>
                        {unitOfMeasuringState.units.map((unit, key) =>
                            <option value={unit.id} key={key}>{unit.unitName}</option>
                        )}
                    </Field>
                </div>
                {
                    error &&
                    <ErrorMessages error={error} className="text-center" />
                }
                <div className="col col-md-2 d-flex align-items-center">
                    <IconButton type="submit">
                        <i className="fa-sm fas fa-check text-success"></i>
                    </IconButton>
                    <IconButton onClick={() => this.props.onCancel(initialValues)}>
                        <i className="fa-sm fas fa-times text-danger"></i>
                    </IconButton>
                </div>
            </form>
        );
    }
}

export default reduxForm({
    form: 'item-form',
    validate: validate,
    enableReinitialize: true
})(OwnerEditItemForm);