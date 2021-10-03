import React, { Component } from 'react';
import { connect } from 'react-redux';

import add_event from '../actions/event/event-add-action';
import logout from '../actions/login/logout-action';
import Landing from '../components/landing';

class LandingWrapper extends Component {
    logout_reset = () => {
        this.props.hub.stop();
        this.props.logout();
    }

    onSubmit = () => {
        return this.props.add_event();
    }
    render() {
        return <Landing user={this.props.user}
            onSubmit={this.onSubmit}
            onLogoutClick={this.logout_reset} />
    }
}

const mapStateToProps = (state) => ({
    user: state.user,
    hub: state.hubConnections.chatHub,
});

const mapDispatchToProps = dispatch => {
    return {
        add_event: () => dispatch(add_event()),
        logout: () => { dispatch(logout()) },
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(LandingWrapper)