import React, { Component } from 'react';
import { connect } from 'react-redux';
import HeaderProfile from '../components/header-profile';
import logout from '../actions/logout-action';
import { setRegisterPending, setRegisterSuccess } from '../actions/register/register-action';
import { setLoginPending, setLoginSuccess } from '../actions/login-action';

class HeaderProfileWrapper extends Component {
  logout_reset = () => {
    this.props.hub.stop();
    this.props.logout();
  }

  render() {
    return <HeaderProfile
      user={this.props.user}
      filter={this.props.events.filter}
      onClick={this.logout_reset}
      reset={this.props.reset}
      notification={this.props.notification.events.length}
    />;
  }
}

const mapStateToProps = state => {
  return {
    ...state,
    user: state.user,
    register: state.register,
    hub: state.hubConnection,
    notification: state.notification
  };
};

const mapDispatchToProps = dispatch => {
  return {
    logout: () => { dispatch(logout()) },
    reset: () => {
      dispatch(setRegisterPending(true));
      dispatch(setRegisterSuccess(false));
      dispatch(setLoginPending(true));
      dispatch(setLoginSuccess(false));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(HeaderProfileWrapper);
