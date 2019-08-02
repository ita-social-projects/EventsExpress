﻿import React, { Component } from 'react';
import { connect } from 'react-redux';
import Fab from '@material-ui/core/Fab';
import { block_user, unblock_user } from '../actions/user'
import UserInfo from '../components/user-info'
import UserRoleWrapper from '../containers/user-role'


class UserInfoWpapper extends Component {
    constructor(props) {
        super(props);
    }

    block = () => this.props.block(this.props.user.id)

    unblock = () => this.props.unblock(this.props.user.id)


    componentDidUpdate(){
        var role = this.props.roles.find(x => x.id = this.props.user.role.id);
        this.props.user.role = role;
    }

    render() {
        const { user, currentUser } = this.props;
        
        return (
            <tr className={(user.isBlocked == true) ? "bg-warning" : ""}>
                <UserInfo user={user} />

                <UserRoleWrapper user={user} currentUser={currentUser} />
                
                <td className="align-middle">
                    {(user.id !== currentUser.id)
                        ? <div className="d-flex justify-content-center align-items-center">
                            {(user.isBlocked == true)
                                ? <Fab size="small" onClick={this.unblock} >
                                    <i className="fas fa-lock" ></i>
                                </Fab>
                                : <Fab size="small" onClick={this.block} >
                                    <i className="fas fa-unlock-alt" ></i>
                                </Fab>}
                        </div>
                        : <div> </div>
                    }
                </td>
            </tr>
        )
    }
}

const mapStateToProps = (state) => ({
    currentUser: state.user,
    roles: state.roles.data
});

const mapDispatchToProps = (dispatch) => {
    return {
       block: (id) => dispatch(block_user(id)),
       unblock: (id) => dispatch(unblock_user(id))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(UserInfoWpapper);