import React, { Component } from 'react';
import { connect } from 'react-redux';
import UsersWrapper from '../containers/users';


export default class UsersPWrapper extends Component {
    render() {
        return (
            <UsersWrapper params={this.props.location.search} />
             );
    }
}