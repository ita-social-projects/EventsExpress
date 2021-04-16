import React, { Component } from 'react';
import UsersWrapper from '../containers/users';


export default class UsersPWrapper extends Component {
    render() {
        return (
            <UsersWrapper params={this.props.location.search}  />
        );
    }
}