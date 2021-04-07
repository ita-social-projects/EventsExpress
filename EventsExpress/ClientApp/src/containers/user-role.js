import React, { Component } from 'react';
import { connect } from 'react-redux';
import { UserRoleDisplay } from './../components/user-info/user-role-display'
import UserRoleEdit from './../components/user-info/user-role-edit'
import { change_user_role, set_edited_user } from './../actions/user/user-action'

class UserRoleWpapper extends Component {

    saveChanges = values => {
        let {roles} = values
        // if (role && role.id !== this.props.user.role.id) { // todo Array compare
            this.props.set_new_role(this.props.user.id, roles)
        // }
        this.props.set_mode_display();
    }

    render() {
        const { user, isCurrentUser, set_mode_display, set_mode_edit } = this.props;

        return (this.props.isEdit)
            ? <UserRoleEdit initialValues={user} onSubmit={this.saveChanges} cancel={set_mode_display} />
            : <UserRoleDisplay user={user} isCurrentUser={isCurrentUser} callback={set_mode_edit} />
    }
}

const mapDispatchToProps = (dispatch, props) => {
    return {
        set_new_role: (uid, role) => dispatch(change_user_role(uid, role)),
        set_mode_display: () => dispatch(set_edited_user()),
        set_mode_edit: () => dispatch(set_edited_user(props.user.id))
    }
};

export default connect(null, mapDispatchToProps)(UserRoleWpapper);