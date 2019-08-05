import React from "react";
import TextField from "@material-ui/core/TextField";
import Multiselect from 'react-widgets/lib/Multiselect';
import 'react-widgets/dist/css/react-widgets.css';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import {  } from 'redux-form';
import Select from '@material-ui/core/Select'
import FormControl from '@material-ui/core/FormControl'
import InputLabel from '@material-ui/core/InputLabel'
import FormHelperText from '@material-ui/core/FormHelperText'

export const validate = values => {
  const errors = {}
  const requiredFields = [
    'email',
    'password',
    'RepeatPassword',
    'image',
    'title',
    'description',
    'categories',
    'country'
  ]
  requiredFields.forEach(field => {
    if (!values[field]) {
      errors[field] = 'Required'
    }
  })
  // if(new Date(values.date_from).getTime() <= new Date().getTime()){
  //   errors.date_from  = 'Date is incorrect';
  // }
  // if(new Date(values.date_from).getTime() >= new Date(values.date_to).getTime()){
  //   errors.date_to = 'Date is too low of start';
  // }
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

export const renderDatePicker = ({ input: { onChange, value }, defaultValue, showTime }) =>{
console.log(value, defaultValue);
value = value || new Date();
defaultValue = defaultValue || new Date();
return <DatePicker
  onChange={onChange}
  minDate={new Date(defaultValue) || new Date()}
  selected={new Date(value) || new Date(defaultValue) || new Date()}
/>
}

 export const maxLength = max => value =>
    value && value.length > max ? `Must be ${max} characters or less` : undefined
export const maxLength15 = maxLength(15)
export const minLength = min => value =>
    value && value.length < min ? `Must be ${min} characters or more` : undefined
export const minLength2 = minLength(6)

export const renderMultiselect = ({ input, data, valueField, textField, placeholder }) =>
    <Multiselect {...input}
        onBlur={() => input.onBlur()}
        value={input.value || []} 
        data={data}
        valueField={valueField}
        textField={textField}
        placeholder={placeholder}
    />

export const renderTextField = ({
  label,
  defaultValue,
    input,
  meta: { touched, invalid, error },
  ...custom
}) => (
  <TextField
   fullWidth
    label={label}
    placeholder={label}
    error={touched && invalid}
    defaultValue={defaultValue}
    value={defaultValue}
    helperText={touched && error}
    {...input}
    {...custom}
  />
)

 export const renderSelectField = ({
    input,
    label,
    meta: { touched, error },
    children,
    ...custom
}) => (
        <FormControl error={touched && error}>
            <InputLabel htmlFor="age-native-simple">Role</InputLabel>
            <Select
                native
                {...input}
                {...custom}
                inputProps={{
                    name: 'Role',
                    id: 'age-native-simple'
                }}
            >
                {children}
            </Select>
            {renderFromHelper({ touched, error })}
        </FormControl>
    )

const renderFromHelper = ({ touched, error }) => {
    if (!(touched && error)) {
        return
    } else {
        return <FormHelperText>{touched && error}</FormHelperText>
    }
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