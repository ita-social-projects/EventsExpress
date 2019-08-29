import React, { Component } from 'react';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import { reduxForm, Field } from 'redux-form';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import {  renderTextArea } from '../helpers/helpers';
import InputLabel from '@material-ui/core/InputLabel';


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
  

class ContactUs extends Component{
    constructor(props){
        super(props)
    }
    

    

    render(){
        const classes = useStyles;
        const { handleSubmit, pristine, reset, submitting } = this.props;
        return(
            <form className="col-7 ">
                <div className="box text text-2 pl-md-4 " >
                    <Field className="form-control" component="select">
                        <option value="newCategory">new category</option>;
                        <option value="newCategory">newCategory</option>;
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
    
})(ContactUs);