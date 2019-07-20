import React from 'react'
import { Field, reduxForm } from 'redux-form'
import Button from "@material-ui/core/Button";
import Select from 'react-select';
import { suggestions } from '../../containers/SelectCategories'
import { renderMultiselect } from '../helpers/helpers'


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