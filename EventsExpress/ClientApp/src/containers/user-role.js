import React, { Component } from 'react';
import { connect } from 'react-redux';
import { UserRoleDisplay } from './../components/user-info/user-role-display'
import UserRoleEdit from './../components/user-info/user-role-edit'
import { change_user_role } from './../actions/user'

class UserRoleWpapper extends Component {
    constructor(props) {
        super(props);

        this.state = {
            mode: "display"
        }
    }

    toggleMode = () => {
        let nextMode = (this.state.mode === "display") ? "edit" : "display";
        this.setState({ mode: nextMode });
    }

    saveChanges = (role) => {
        
        if (role && role.id !== this.props.user.role.id) {
            this.props.set_new_role(this.props.user.id, role)
        }
        this.toggleMode();
    }

    render() {
        const { user, currentUser} = this.props;

        return (<> {(this.state.mode === "display")
            ? <UserRoleDisplay user={user} currentUser={currentUser} callback={this.toggleMode} 
                />
            : <UserRoleEdit user={this.props.user} callback={this.saveChanges} cancel={this.toggleMode}/>
                }
            </>)
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        set_new_role: (uid, role) => dispatch(change_user_role(uid, role))
    }
};

export default connect(null, mapDispatchToProps)(UserRoleWpapper);