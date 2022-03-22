import React, { Component } from 'react';
import { compose } from 'redux';
import { connect } from 'react-redux';
import { reduxForm, Field } from 'redux-form';
import IconButton from "@material-ui/core/IconButton";
import { renderMultiselect } from '../helpers/form-helpers';
import ErrorMessages from '../shared/errorMessage';

class UserRoleEdit extends Component {
    render() {
        let { pristine, submitting, handleSubmit, error } = this.props
        return (<>
            <td className="align-middle">
                <form onSubmit={handleSubmit} id="user-role">
                    <Field
                        className="form-control"
                        name="roles"
                        component={renderMultiselect}
                        data={this.props.roles}
                        valueField={"id"}
                        textField={"name"}
                    />
                    {error && <ErrorMessages error={error} />}
                </form>
            </td>

            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center">
                    <IconButton className="text-success" size="small" type="submit" form='user-role' disabled={pristine || submitting} >
                        <i className="fas fa-check" />
                    </IconButton>
                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times" />
                    </IconButton>
                </div>
            </td>
        </>)
    }
}

const mapStateToProps = state => ({
    roles: state.roles.data,
});

export default compose(
    connect(mapStateToProps),
    reduxForm({ form: "user-role" })
)(UserRoleEdit)
