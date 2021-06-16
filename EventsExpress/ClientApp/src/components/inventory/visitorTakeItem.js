import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { renderTextField } from '../helpers/form-helpers';
import IconButton from "@material-ui/core/IconButton";
import Module from '../helpers';

const { validate } = Module;


class VisitorEditItemForm extends Component {
    maxValue = max => value =>
        value && value > max ? `Must be less or equal than ${max}` : undefined
    maxValueLimitor = this.maxValue(this.props.initialValues.needQuantity - this.props.alreadyGet)

    minValue = min => value =>
        value && value < min ? `Must be at least ${min}` : undefined
    minValueLimitor = this.minValue(1)
    
    render() {
        const { initialValues, alreadyGet } = this.props;
        return (
            <form onSubmit={this.props.handleSubmit}  className="form-inline w-100">
                <div className="col col-md-4">
                    {initialValues.itemName}
                </div>
                <div className="col">{alreadyGet}</div>
                <div className="col col-md-2 d-flex align-items-center">
                    <Field
                        name="willTake"
                        type="number"
                        fullWidth={false}
                        validate={[this.maxValueLimitor, this.minValueLimitor]}
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
})(VisitorEditItemForm);