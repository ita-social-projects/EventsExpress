import React, { Component } from 'react';
import Fab from '@material-ui/core/Fab';
import { connect } from 'react-redux';
import { reduxForm, formValueSelector, Field } from 'redux-form';
import get_roles from '../../actions/roles'

class UserRoleEdit extends Component {
    componentDidMount = () => this.props.get_roles();

    renderRolesOptions = (arr) => {
        return arr.map((item) => {
            return <option key={item.id} value={item.id} selected={(this.props.user.role.id === item.id) ? true : false}>{item.name}</option>;
        });
    }

    handleSubmit = (e) => {
        e.preventDefault();
        var newRole = this.props.roles.find(r => r.id === this.props.newRoleId);
        this.props.callback(newRole);
    }

    render() {
        
        return (<>
            <td className="align-middle">
                <form onSubmit={this.handleSubmit} id="user-role"> 
                    <Field className="form-control" name={"role-for-"+ this.props.user.id} component="select">
                        <option>Roles</option>
                        {this.renderRolesOptions(this.props.roles)}
                    </Field>
                </form>
            </td>

            <td className="align-middle d-flex align-items-center">
                <Fab size="small" type="submit" form='user-role' className="mr-1">
                    <i className="fas fa-check"></i>
                </Fab>
                <Fab size="small" onClick={this.props.cancel}>
                    <i className="fas fa-times"></i>
                </Fab>
            </td>
        </>)
    }
}

const selector = formValueSelector("user-role")

const mapStateToProps = (state,props) => {
    return {
        roles: state.roles.data,
        newRoleId: selector(state, "role-for-" + props.user.id)
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_roles: () => dispatch(get_roles())
    }
};

UserRoleEdit = connect(mapStateToProps, mapDispatchToProps)(UserRoleEdit);

UserRoleEdit = reduxForm({
    form: "user-role"
})(UserRoleEdit);

export default UserRoleEdit;