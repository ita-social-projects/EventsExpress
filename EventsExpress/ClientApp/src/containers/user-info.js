import React, { Component } from 'react';
import { connect } from 'react-redux';
import Avatar from '@material-ui/core/Avatar';
import Fab from '@material-ui/core/Fab';
import { block_user, unblock_user } from '../actions/user'
import UserInfo from '../components/user-info'


class UserInfoWpapper extends Component {
    constructor(props) {
        super(props);
    }

    block = () => this.props.block(this.props.user.id)

    unblock = () => this.props.unblock(this.props.user.id)



    render() {
        const { user, currentUser } = this.props;
        
        return (
            <tr className={(user.isBlocked == true) ? "bg-warning" : ""}>
                <UserInfo user={user} />

                <td className="align-middle">{user.role.name}</td>

                <td className="align-middle">
                    <Fab size="small" onClick={this.unSblock} >
                        <i className="fas fa-edit"></i>
                    </Fab>
                </td>

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
    currentUser: state.user
});

const mapDispatchToProps = (dispatch) => {
    return {
       block: (id) => dispatch(block_user(id)),
       unblock: (id) => dispatch(unblock_user(id))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(UserInfoWpapper);