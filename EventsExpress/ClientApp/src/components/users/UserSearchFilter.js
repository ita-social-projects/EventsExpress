import React, { Component } from 'react';
import { renderTextField} from '../helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import DialogActions from "@material-ui/core/DialogActions";

class UserSearchFilter extends Component {
    render() {
        return <>
            <form onSubmit={this.props.handleSubmit} className="box">
                <Field name='search' component={renderTextField} type="input" label="Search:" /> 
                
                <DialogActions>
                    <Button fullWidth={true} type="button" color="primary" disabled={this.props.pristine || this.props.submitting} onClick={this.props.reset}>
                        CLEAR
                   </Button >
                    <Button fullWidth={true} type="submit" color="primary" disabled={this.props.submitting}>
                        Search
                </Button>
                </DialogActions>
            </form>
        </>
    }
}

export default UserSearchFilter = reduxForm({
    form: 'user-search-filter-form',
})(UserSearchFilter);