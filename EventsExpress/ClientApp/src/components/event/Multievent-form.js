import React from 'react'
import { Field, FieldArray, reduxForm } from 'redux-form'
import {
    renderDatePicker, LocationMapWithMarker, renderCheckbox, radioButton,
    renderSelectField, renderTextField, renderTextArea, renderMultiselect
} from '../helpers/form-helpers';


const renderField = ({ input, label, type, meta: { touched, error } }) => (
    <div>
        <label>{label}</label>
        <div>
            <input {...input} type={type} placeholder={label} />
            {touched && error && <span>{error}</span>}
        </div>
    </div>
)



const renderMembers = ({ fields, meta: { error, submitFailed }, disabledDate, form_values }) => (
    <ul>
        <li>
            <button type="button" onClick={() => fields.push({})}>
                Add Event
      </button>
            {submitFailed && error && <span>{error}</span>}
        </li>
        {fields.map((member, index) => (
            <li key={index}>
                <button
                    type="button"
                    title="Remove Member"
                    onClick={() => fields.remove(index)}>
                    x
                    </button>
                <h4>Event #{index + 1}</h4>
                <Field
                    name={`${member}.Title`}
                    type="text"
                    component={renderField}
                    label="Title"
                />
                <Field
                    name={`${member}.description`}
                    type="text"
                    component={renderField}
                    label="Description"
                />
                <span >
                    <Field
                        name='dateFrom'
                        label='From'
                        disabled={disabledDate}
                        component={renderDatePicker}
                    />
                </span>
                <span className="retreat">
                    <Field
                        name='dateTo'
                        label='To'
                        disabled={disabledDate}
                        minValue={form_values.dateFrom}
                        component={renderDatePicker}
                    />
                </span>
            </li>
        ))}
    </ul>
)

const FieldArraysForm = props => {
    const { handleSubmit, pristine, reset, submitting, form_values, disabledDate } = props
    return (
        <form onSubmit={handleSubmit}>
            <FieldArray name="members" component={renderMembers} disabledDate={disabledDate} form_values={form_values} />
            <div>
                <button type="submit" disabled={submitting}>
                    Submit
        </button>
                <button type="button" disabled={pristine || submitting} onClick={reset}>
                    Clear Values
        </button>
            </div>
        </form>
    )
}

export default reduxForm({
    form: 'fieldArrays', // a unique identifier for this form
})(FieldArraysForm)