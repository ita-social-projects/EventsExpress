import React, { Component } from 'react';
import { connect } from 'react-redux';
import SearchUsers from '../containers/SearchUsers';


export default class UsersPWrapper extends Component {
    render() {
        return (
            <SearchUsers params={this.props.location.search} />
        );
    }
}