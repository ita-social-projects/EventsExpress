import React, { Component } from 'react';
import { connect } from 'react-redux';
import Fab from '@material-ui/core/Fab';
import { UserRoleDisplay } from './../components/user-info/user-role-display'
import UserRoleEdit from './../components/user-info/user-role-edit'

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



    render() {
        const { role } = this.props.user;

        return (<> {(this.state.mode === "display")
            ? <UserRoleDisplay role={role} callback={this.toggleMode} />
            : <UserRoleEdit user={this.props.user} callback={this.toggleMode} />
                }
            </>)
    }
}


export default connect(null, null)(UserRoleWpapper);