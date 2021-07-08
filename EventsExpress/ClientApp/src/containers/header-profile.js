import React, { Component } from 'react';
import { connect } from 'react-redux';
import HeaderProfile from '../components/header-profile';
import logout from '../actions/login/logout-action';
import add_event from '../actions/event/event-add-action';

class HeaderProfileWrapper extends Component {
  logout_reset = () => {
    this.props.hub.stop();
    this.props.logout();
  }

  onSubmit = () => {
    return this.props.add_event();
  }

  render() {
    return <HeaderProfile
      user={this.props.user}
      filter={this.props.events.filter}
      onClick={this.logout_reset}
      notification={this.props.notification.events.length}
      onSubmit={this.onSubmit} />
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
    add_event: () => dispatch(add_event()),
    logout: () => { dispatch(logout()) },
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(HeaderProfileWrapper);
