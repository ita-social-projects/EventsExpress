import React, { Component } from 'react';
import { renderTextField} from '../helpers/helpers';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
class UserSearchFilter extends Component {
    render() {
        return <>
            <form onSubmit={this.props.handleSubmit} className="box">
                <Field name='search' component={renderTextField} type="input" label="Search:" /> 
                <Button fullWidth={true} type="submit" color="primary" disabled={this.props.submitting}>
                    Search
                </Button>
            </form>
        </>
    }
}

export default UserSearchFilter = reduxForm({
    form: 'user-search-filter-form',
})(UserSearchFilter);