import React from 'react'
import { Field, FieldArray} from 'redux-form'
import {
    renderDatePicker, LocationMapWithMarker
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
            <button type="button" className="btn btn-success" onClick={() => fields.push({})}>
                Add Event
      </button>
            {submitFailed && error && <span>{error}</span>}
        </li>
        {fields.map((member, index) => (
            <li key={index}>
                <button
                    type="button"
                    title="Remove Member"
                    className="btn btn-danger"
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
                        name={`${member}.dateFrom`}
                        label='From'
                        disabled={disabledDate}
                        component={renderDatePicker}
                    />
                </span>
                <span className="retreat">
                    <Field
                        name={`${member}.dateTo`}
                        label='To'
                        disabled={disabledDate}
                        minValue={form_values.dateFrom}
                        component={renderDatePicker}
                    />
                </span>
                <div className="mt-2">
                    <Field
                        name={`${member}.location`}
                        component={LocationMapWithMarker}
                    />
                </div>
            </li>
        ))}
    </ul>
)

const FieldArraysForm = props => {
    const { form_values, disabledDate } = props
    return (
            <FieldArray name="Events" component={renderMembers} disabledDate={disabledDate} form_values={form_values} />
            
    )
}

export default FieldArraysForm;