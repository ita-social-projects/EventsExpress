import React from "react";
import TextField from "@material-ui/core/TextField";
import Multiselect from 'react-widgets/lib/Multiselect'
export const validate = values => {
  const errors = {}
  const requiredFields = [
    'email',
    'password',
    'RepeatPassword'
  ]
  requiredFields.forEach(field => {
    if (!values[field]) {
      errors[field] = 'Required'
    }
  })
  
  if (
    values.email &&
    !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)
  ) {
    errors.email = 'Invalid email address'
    }
  if(values.password!== values.RepeatPassword){
    errors.RepeatPassword = 'Passwords do not match';
  }
  return errors
}

 export const maxLength = max => value =>
    value && value.length > max ? `Must be ${max} characters or less` : undefined
export const maxLength15 = maxLength(15)
export const minLength = min => value =>
    value && value.length < min ? `Must be ${min} characters or more` : undefined
export const minLength2 = minLength(2)

export const renderMultiselect = ({ input, data, valueField, textField }) =>
    <Multiselect {...input}
        onBlur={() => input.onBlur()}
        value={input.value || []} // requires value to be an array\
        data={data}
        valueField={valueField}
        textField={textField}

    />

export const renderTextField = ({
  label,
  input,
  meta: { touched, invalid, error },
  ...custom
}) => (
  <TextField
   fullWidth
    label={label}
    placeholder={label}
    error={touched && invalid}
    helperText={touched && error}
    {...input}
    {...custom}
  />
)

const sleep = ms => new Promise(resolve => setTimeout(resolve, ms))

const asyncValidate = (values) => {
  return sleep(1000).then(() => {
    if (['foo@foo.com', 'bar@bar.com'].includes(values.email)) {
      throw { email: 'Email already Exists' }
    }
  })
}

export default asyncValidate;