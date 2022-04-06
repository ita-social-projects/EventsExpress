import React from 'react';
import { Field, reduxForm } from 'redux-form';
import IconButton from "@material-ui/core/IconButton";
import Button from "@material-ui/core/Button";
import ErrorMessages from '../../shared/errorMessage';
import { renderTextField } from '../../helpers/form-helpers';
import './editFieldsStyles.css';

const EditUsername = props => {
    const { handleSubmit, pristine, reset, submitting, onClose } = props;
    return (
        <form onSubmit={handleSubmit} className='field'>
            <div className='content'>
                <Field
                    name="userName"
                    component={renderTextField}
                    label="UserName"
                />
                {
                    props.error &&
                    <ErrorMessages error={props.error} className="text-center" />
                }
            </div>
            <div className='editFieldButtons'>
                {/* <Button type="submit" color="primary" disabled={pristine || submitting}  >Submit</Button>
                <Button type="button" color="primary" disabled={submitting} onClick={reset && onClose}>
                    Clear
                </Button> */}
                <IconButton className="text-success" size="small" type="submit" disabled={pristine || submitting} >
                        <i className="fas fa-check" />
                </IconButton>
                <IconButton className="text-danger" size="small" onClick={reset && onClose}>
                    <i className="fas fa-times" />
                </IconButton>
            </div>
        </form>
    );
};

export default reduxForm({
    form: 'EditUsername',
    
})(EditUsername);