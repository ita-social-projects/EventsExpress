import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { renderTextField, renderSelectField } from '../helpers/helpers';
import  get_unitsOfMeasuring  from '../../actions/unitsOfMeasuring';
import { connect } from 'react-redux';
import Module from '../helpers';

const { validate } = Module;

class ItemForm extends Component {

    render() {
        const { initialValues, unitOfMeasuringState } = this.props;

        return (
            <form onSubmit={this.props.handleSubmit}  className="form-inline w-100">
                <div className="col col-md-5">
                    <Field
                        name={`itemName`}
                        type="text"
                        fullWidth={false}
                        label="Item name"
                        component={renderTextField}/>
                </div>
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
                <div className="col">
                <button type="submit" className='btn'>
                    ok
                </button>
                <button type="button" onClick={() => this.props.onCancel(initialValues)} className='btn'>
                    cancel
                </button>
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