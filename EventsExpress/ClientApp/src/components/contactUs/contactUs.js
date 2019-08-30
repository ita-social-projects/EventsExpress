import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import {  renderTextArea } from '../helpers/helpers';
import Module from '../helpers';


const useStyles = makeStyles(theme => ({
    root: {
      display: 'flex',
      flexWrap: 'wrap',
    },
    formControl: {
      margin: theme.spacing(1),
      minWidth: 120,
    },
    selectEmpty: {
      marginTop: theme.spacing(2),
    },
  }));
  
  const { validate, renderTextField, asyncValidate } = Module;

class ContactUs extends Component{
    constructor(props){
        super(props)
    }
    

    

    render(){
        const classes = useStyles;
        const {  pristine, reset, submitting } = this.props;
        return(
            <form className="col-7 " onSubmit={this.props.handleSubmit}>
                <div className="box text text-2 pl-md-4 " >
                    <Field name='type' className="form-control" component="select">
                        <option value="" >--</option>;
                        <option value="newCategory">New Category</option>;
                        <option value="bugReport">Bug Report</option>;
                        <option value="badEvent">Bad Event</option>;
                        <option value="bugUser">Bad User</option>;
                    </Field>
                    <Field name='description' component={renderTextArea}
                     type="input" label="Description" />
                    
                
                <Button type="submit" color="primary" disabled={pristine || submitting}>Submit</Button>
                <Button type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
                    Clear
                </Button>
                </div>
            </form>
        )
    }
}

export default reduxForm({
    form: "ContactUs", // a unique identifier for this form
    validate
})(ContactUs);