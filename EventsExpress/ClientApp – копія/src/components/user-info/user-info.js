import React, { Component } from 'react';
import CustomAvatar from '../avatar/custom-avatar';
import RatingAverage from '../rating/rating-average';

export default class UserInfo extends Component {

    render() {
        const { user } = this.props;

        return (
            <>
                <td className="align-middle">
                    <CustomAvatar photoUrl={user.photoUrl} name={user.username} />                                        
                </td>

                <td className="align-middle">
                    <RatingAverage value={user.rating} direction='col' />
                </td>

                <td className="align-middle">{user.email}</td>

                <td className="align-middle">{user.username}</td>
            </>
                
 
        )
    }
}
