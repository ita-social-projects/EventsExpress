import React, { Component } from 'react';
import Avatar from '@material-ui/core/Avatar';


export default class UserInfo extends Component {

    render() {
        const { user } = this.props;

        return (
            <>
                <td className="align-middle">
                    {user.photoUrl
                        ? <Avatar src={user.photoUrl} />
                        : <Avatar>{user.email.charAt(0).toUpperCase()}</Avatar>}

                </td>

                <td className="align-middle">{user.email}</td>

                <td className="align-middle">{user.username}</td>
            </>
                
 
        )
    }
}
