import React, { Component } from 'react';
import { connect } from 'react-redux';
import HeaderProfile from '../components/header-profile';
import logout from '../actions/logout-action';
import { setRegisterPending, setRegisterSuccess, setRegisterError } from '../actions/register';
import { setLoginPending, setLoginSuccess } from '../actions/login-action';
import add_event from '../actions/event-add-action';

class HeaderProfileWrapper extends Component {
  logout_reset = () => {
    this.props.hub.stop();
    this.props.logout();
  }

    onSubmit = (values) => {
        return this.props.add_event({ user_id: this.props.user.id });
    }

    render() {
    return <HeaderProfile
        user={this.props.user}
        filter={this.props.events.filter}
        onClick={this.logout_reset}
        reset={this.props.reset}
        notification={this.props.notification.events.length}
        onSubmit={this.onSubmit}
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
        add_event: (data) => dispatch(add_event(data)),
    logout: () => { dispatch(logout()) },
    reset: () => {
      dispatch(setRegisterPending(true));
      dispatch(setRegisterSuccess(false));
      dispatch(setRegisterError(null));
      dispatch(setLoginPending(true));
      dispatch(setLoginSuccess(false));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(HeaderProfileWrapper);
