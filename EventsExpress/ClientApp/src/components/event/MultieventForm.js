import React from 'react'
import "./MultieventForm.css";
import IconButton from "@material-ui/core/IconButton";
import { Field, FieldArray} from 'redux-form'
import {
    renderDatePicker, LocationMapWithMarker, renderTextField, renderTextArea
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
       
            
            {submitFailed && error && <span>{error}</span>}
   
        {fields.map((member, index) => (
            <li key={index} className="childEvent">
                <button
                    type="button"
                    title="Remove Member"
                    className="float-right btn btn-danger"
                    onClick={() => fields.remove(index)}>
                    <i className="fa fa-trash"></i>
                </button>
                <div className="mt-2">
                    <h4>Event #{index + 1}</h4>
                </div>
                <div className="mt-2">
                <Field
                    name={`${member}.Title`}
                    type="text"
                    component={renderTextField}
                    label="Title"
                    />
                </div>
                <div className="mt-3">
                <Field
                    name={`${member}.description`}
                    type="text"
                    component={renderTextArea}
                    label="Description"
                    />
                </div>
                <div className="mt-2">
                <span >
                    <Field
                        name={`${member}.dateFrom`}
                        label='From'
                        disabled={disabledDate}
                        minValue={form_values.dateFrom}
                        maxValue={form_values.dateTo}
                        component={renderDatePicker}
                    />
                </span>
                <span className="retreat">
                    <Field
                        name={`${member}.dateTo`}
                        label='To'
                        disabled={disabledDate}
                        minValue={form_values.Events[index].dateFrom}
                        maxValue={form_values.dateTo}
                        component={renderDatePicker}
                    />
                    </span>
               </div>
                <div className="mt-2">
                    <Field
                        name={`${member}.location`}
                        component={LocationMapWithMarker}
                    />
                </div>
            </li>
        ))}
        <IconButton
            onClick={() => fields.push({})}
            size="small">
            <span className="icon"><i className="fa-sm fas fa-plus"></i></span> Add Event
        </IconButton>
    </ul>
)

const MultieventForm = props => {
    const { form_values, disabledDate } = props
    return (
            <FieldArray name="Events" component={renderMembers} disabledDate={disabledDate} form_values={form_values} />
            
    )
}

export default MultieventForm;