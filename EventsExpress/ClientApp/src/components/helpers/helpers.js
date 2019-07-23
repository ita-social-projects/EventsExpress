import React from "react";
import TextField from "@material-ui/core/TextField";
import {  } from 'redux-form';

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

export const renderTextField = ({
  label,
    input,
  value,
  meta: { touched, invalid, error },
  ...custom
}) => (
  <TextField
   fullWidth
    value={new Date(2019, 1, 1)}
    label={label}
    placeholder={label}
    error={touched && invalid}
            defaultValue={value}
    helperText={touched && error}
    {...input}
    {...custom}
  />
)

export const renderDateTimePicker = () =>
{

}


const sleep = ms => new Promise(resolve => setTimeout(resolve, ms))

const asyncValidate = (values) => {
  return sleep(1000).then(() => {
    if (['foo@foo.com', 'bar@bar.com'].includes(values.email)) {
      throw { email: 'Email already Exists' }
    }
  })
}

export default asyncValidate;