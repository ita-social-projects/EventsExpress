import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { renderTextField, renderSelectField } from '../helpers/helpers';
import IconButton from "@material-ui/core/IconButton";
import Module from '../helpers';

const { validate } = Module;


class WillTakeItemForm extends Component {
    maxValue = max => value =>
        value && value > max ? `Must be less or equal than ${max}` : undefined
    maxValueLimitor = this.maxValue(this.props.initialValues.needQuantity - this.props.alreadyGet)

    minValue = min => value =>
        value && value < min ? `Must be at least ${min}` : undefined
    minValueLimitor = this.minValue(1)

    render() {
        const { initialValues } = this.props;
        return (
            <form onSubmit={this.props.handleSubmit}  className="form-inline w-100">
                <div className="col col-md-4">
                    {initialValues.itemName}
                </div>
                <div className="col">0</div>
                <div className="col col-md-2">
                    <Field
                        name="willTake"
                        type="number"
                        fullWidth={false}
                        validate={initialValues.isWillTake ? [this.maxValueLimitor, this.minValueLimitor] : []}
                        label="Will take"
                        component={renderTextField}/>
                </div>
                <div className="col col-md-1">
                    {initialValues.needQuantity}
                </div>
                <div className="col col-md-1">
                    {initialValues.unitOfMeasuring.shortName}
                </div>
                <div className="col col-md-2">
                    <IconButton type="submit">
                        <i className = "fa-sm fas fa-check text-success"></i>
                    </IconButton>
                    <IconButton onClick={() => this.props.onCancel(initialValues)}>
                        <i className = "fa-sm fas fa-times text-danger"></i>
                    </IconButton>
                </div>
            </form>
        );
    }
}

export default reduxForm({
    form: 'will-take-item-form',
    validate: validate,
    enableReinitialize: true
})(WillTakeItemForm);