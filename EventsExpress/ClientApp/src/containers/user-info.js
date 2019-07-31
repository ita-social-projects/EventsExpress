import React, { Component } from 'react';
import { connect } from 'react-redux';
import Avatar from '@material-ui/core/Avatar';
import Fab from '@material-ui/core/Fab';
import { block_user, unblock_user } from '../actions/user'

class UserInfoWpapper extends Component {
    block = () => {
        let value = this.props.user.id;
        console.log('block user: ');
        console.log(value);
        this.props.block(value);
    };

    unblock = () => {
        let value = this.props.user.id;
        console.log('unblock user: ');
        console.log(value);
        this.props.unblock(value);
    };

    render() {
        const { user } = this.props;

        return (
            <tr id={user.id} className={(user.isBlocked == true) ? "bg-warning" : ""}>
                <td className="align-middle">
                    {user.photoUrl
                        ? <Avatar src={user.photoUrl} />
                        : <Avatar>{user.email.charAt(0).toUpperCase()}</Avatar>}

                </td>

                <td className="align-middle">{user.email}</td>

                <td className="align-middle">{user.username}</td>

                <td className="align-middle">{user.role.name}</td>

                <td className="align-middle">
                    <Fab size="small" onClick={this.unSblock} >
                        <i className="fas fa-edit"></i>
                    </Fab>
                </td>

                <td className="align-middle">
                    <div className="d-flex justify-content-center align-items-center">
                        {(user.isBlocked == true)
                            ? <Fab size="small" onClick={this.unblock} >
                                <i className="fas fa-lock" ></i>
                            </Fab>
                            : <Fab size="small" onClick={this.block} >
                                <i className="fas fa-unlock-alt" ></i>
                            </Fab>}
                    </div>
                </td>
            </tr>
        )
    }
}

const mapStateToProps = (state) => ({

});

const mapDispatchToProps = (dispatch) => {
    return {
       block: (id) => dispatch(block_user(id)),
       unblock: (id) => dispatch(unblock_user(id))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(UserInfoWpapper);