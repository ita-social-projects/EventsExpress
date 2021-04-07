import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reduxForm, Field } from 'redux-form';
import get_roles from '../../actions/roles'
import IconButton from "@material-ui/core/IconButton";
import { renderMultiselect } from '../helpers/form-helpers';

class UserRoleEdit extends Component {
    componentDidMount = () => {
        this.props.get_roles();
    }

    render() {

        return (<>
            <td className="align-middle">
                <form onSubmit={this.props.handleSubmit} id="user-role">
                    <Field
                        className="form-control"
                        name="roles"
                        component={renderMultiselect}
                        data={this.props.roles}
                        valueField={"id"}
                        textField={"name"}
                    />
                </form>
            </td>

            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center">
                    <IconButton className="text-success" size="small" type="submit" form='user-role' >
                        <i className="fas fa-check"></i>
                    </IconButton>
                    <IconButton className="text-danger" size="small" onClick={this.props.cancel}>
                        <i className="fas fa-times"></i>
                    </IconButton>
                </div>
            </td>
        </>)
    }
}

const mapStateToProps = state => {
    return {
        roles: state.roles.data,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_roles: () => dispatch(get_roles())
    }
};

UserRoleEdit = connect(mapStateToProps, mapDispatchToProps)(UserRoleEdit);

UserRoleEdit = reduxForm({
    form: "user-role",
    enableReinitialize: true
})(UserRoleEdit);

export default UserRoleEdit;
