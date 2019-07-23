import React from 'react'
import { Field, reduxForm } from 'redux-form'
import Button from "@material-ui/core/Button";
import { renderMultiselect } from '../helpers/helpers'

function SelectCategories(props) {

    const { handleSubmit, submitting, items } = props
    console.log(items)
    return ( 
        <div >
          
        <form onSubmit={handleSubmit}>
                <Field
                    name="Categories"
                    component={renderMultiselect}
                    data={items}
                    valueField={"id"}
                    textField={"name"}
                 
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