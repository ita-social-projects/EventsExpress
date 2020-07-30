import React, { Component } from 'react';
import { connect } from 'react-redux';
import Fab from '@material-ui/core/Fab';
import { block_user, unblock_user } from '../actions/user'
import { set_dialog } from '../actions/dialog'
import UserInfo from '../components/user-info'
import { UserBlock } from '../components/user-info/user-block'
import UserRoleWrapper from '../containers/user-role'
import IconButton from "@material-ui/core/IconButton";


class UserInfoWpapper extends Component {
    constructor(props) {
        super(props);

        this.isCurrentUser = props.user.id === props.currentUser 
    }

    block = () => this.props.block(this.props.user.id);
        
    

    unblock = () =>  this.props.unblock(this.props.user.id)


    render() {
        const { user, currentUser, editedUser } = this.props;
        
        return (
            <tr className={(user.isBlocked == true) ? "bg-warning" : ""}>
                <UserInfo key={user.id} user={user} />

                <UserRoleWrapper 
                    key={user.id + user.role.id} 
                    user={user} 
                    isCurrentUser={this.isCurrentUser} 
                    isEdit={user.id === editedUser}
                />
                
                <UserBlock 
                    user={user}
                    isCurrentUser={this.isCurrentUser}
                    block={this.block}
                    unblock={this.unblock}
                    
                />


            </tr>
        )
    }
}



const mapStateToProps = (state) => ({
    currentUser: state.user.id,
    editedUser: state.users.editedUser,
    roles: state.roles.data
});

const mapDispatchToProps = (dispatch) => {
    return {
       block: (id) => dispatch(block_user(id)),
       unblock: (id) => dispatch(unblock_user(id)),
       
       
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(UserInfoWpapper);