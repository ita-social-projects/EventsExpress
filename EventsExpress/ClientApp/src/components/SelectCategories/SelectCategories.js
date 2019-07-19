import React from 'react'
import { Field, reduxForm } from 'redux-form'
import Multiselect from 'react-widgets/lib/Multiselect'
import Button from "@material-ui/core/Button";
import Select from 'react-select';


let suggestions = [
    { id: 1, label:'Summer' },
    { id: 2, label: 'Mount' },
    { id: 3, label: 'Party' },
    { id: 4, label: 'Gaming' },
];


const renderMultiselect = ({ input, data, valueField, textField }) =>
    <Multiselect {...input}
        onBlur={() => input.onBlur()}
        value={input.value || []} // requires value to be an array\
        data={data}
        valueField={valueField}
        textField={textField}
       
    />

function SelectCategories(props) {
    const { handleSubmit, submitting } = props

    return ( 
        <div >
          
        <form onSubmit={handleSubmit}>
                <Field
                    name="Categories"
                    component={renderMultiselect}
                    data={suggestions}
                    
                    valueField={"id"}
                    textField={"label"}
                    />
                    <div>
                        <Button type="submit" color="primary" disabled={ submitting} >
                            Save
                        </Button >
            </div>
            </form>
        </div>
  )
} 
export default reduxForm({
    form: 'SelectCategories',  // a unique identifier for this form
})(SelectCategories)