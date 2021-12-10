import React, { Component } from 'react';
import { connect } from 'react-redux';

import add_event from '../actions/event/event-add-action';
import logout from '../actions/login/logout-action';
import Landing from '../components/landing';

class LandingWrapper extends Component {

    onSubmit = () => {
        return this.props.add_event();
    }
    render() {
        return <Landing user={this.props.user}
            onSubmit={this.onSubmit} />
    }
}

const mapStateToProps = (state) => ({
    user: state.user,
    hub: state.hubConnections.chatHub,
});

const mapDispatchToProps = dispatch => {
    return {
        add_event: () => dispatch(add_event()),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(LandingWrapper)