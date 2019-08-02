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
        console.log("handle. now props:");
        e.preventDefault();
        this.props.callback(this.props.newRole);
    }

    render() {
        
        return (<>
            <td className="align-middle">
                <form onSubmit={this.handleSubmit} id="user-role"> 
                    <Field className="form-control" name='role' component="select">
                        <option>Roles</option>
                        {this.renderRolesOptions(this.props.roles)}
                    </Field>

                </form>
            </td>

            <td className="align-middle">
                <Fab size="small" type="submit" form='user-role'>                    
                    <i class="fas fa-check"></i>
                </Fab>
            </td>
        </>)
    }
}

const selector = formValueSelector('user-role')

const mapStateToProps = state => {
    return {
        roles: state.roles.data,
        newRole: selector(state, 'role')
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_roles: () => dispatch(get_roles())
    }
};

UserRoleEdit = connect(mapStateToProps, mapDispatchToProps)(UserRoleEdit);

UserRoleEdit = reduxForm({
    form: 'user-role'
})(UserRoleEdit);

export default UserRoleEdit;


