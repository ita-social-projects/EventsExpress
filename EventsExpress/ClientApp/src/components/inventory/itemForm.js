import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { renderTextField, renderSelectField } from '../helpers/helpers';
import IconButton from "@material-ui/core/IconButton";
import Module from '../helpers';

const { validate } = Module;

class ItemForm extends Component {

    render() {
        const { initialValues, unitOfMeasuringState } = this.props;

        return (
            <form onSubmit={this.props.handleSubmit}  className="form-inline w-100">
                <div className="col col-md-4">
                    <Field
                        name={`itemName`}
                        type="text"
                        fullWidth={false}
                        label="Item name"
                        component={renderTextField}/>
                </div>
                <div className="col">0</div>
                <div className="col">
                    <Field
                        name={`needQuantity`}
                        type="number"
                        fullWidth={false}
                        label="Item count"
                        component={renderTextField}/>
                </div>
                <div className="col">
                    <Field
                        className="selectpicker"
                        name={`unitOfMeasuring`}
                        component={renderSelectField}>
                        <option></option>
                        {unitOfMeasuringState.units.map((unit, key) => 
                            <option value={unit.id} key={key}>{unit.unitName}</option>
                        )} 
                    </Field>
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
    form: 'item-form',
    validate: validate,
    enableReinitialize: true
})(ItemForm);