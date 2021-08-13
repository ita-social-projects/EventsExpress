import React, { Component } from 'react';
import { connect } from 'react-redux';
import Landing from '../components/landing';
import logout from '../actions/login/logout-action';

class LandingWrapper extends Component {
    logout_reset = () => {
        this.props.hub.stop();
        this.props.logout();
    }

    onSubmit = () => {
        return this.props.add_event();
    }

    render() {
        return <Landing
            onLogoutClick={this.logout_reset}/>
    }
}

const mapStateToProps = state => {
    return {
        ...state,
        user: state.user,
        register: state.register,
        hub: state.hubConnections.chatHub,
        notification: state.notification
    };
};

const mapDispatchToProps = dispatch => {
    return {
        logout: () => { dispatch(logout()) },
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LandingWrapper);
