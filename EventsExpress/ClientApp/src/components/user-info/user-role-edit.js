import React, { Component } from 'react';
import Fab from '@material-ui/core/Fab';
import { exact } from 'prop-types';
import { reduxForm, Field } from 'redux-form';

class UserRoleEdit extends Component {

    renderRolesOptions = (arr) => {
        return arr.map((item) => {
            return <option key={item.id} value={item.id} selected={(this.props.user.role.id === item.id) ? true : false}>{item.name}</option>;
        });
    }

    roles = [
        { id: "1bf45308-8120-4c14-f81e-08d7039626c7", name: "Admin"},
        { id: "ccf760e0-c17e-4e22-f81f-08d7039626c7", name: "User"}
    ]

    render() {

        return (<>
            <td className="align-middle">
                <form>
                    <Field onChange={() => { }} className="form-control" name='roles' component="select">
                        <option>Roles</option>
                        {this.renderRolesOptions(this.roles)}
                    </Field>
                </form>

            </td>

            <td className="align-middle">
                <Fab size="small" onClick={this.props.callback} >
                    <i className="fas fa-edit"></i>
                </Fab>
            </td>
        </>)
    }
}


UserRoleEdit = reduxForm({
    form: 'user-role'
})(UserRoleEdit);

export default UserRoleEdit;
