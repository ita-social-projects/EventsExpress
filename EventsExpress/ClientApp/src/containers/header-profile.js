import React, {Component} from 'react';
import {connect} from 'react-redux';
import HeaderProfile from '../components/header-profile';
import logout from '../actions/logout';
import { setRegisterPending, setRegisterSuccess, setRegisterError } from '../actions/register';
import { setLoginPending, setLoginSuccess, setLoginError } from '../actions/login'
class HeaderProfileWrapper extends Component {

    render() {
        return <HeaderProfile user={this.props.user} onClick={this.props.logout} reset={this.props.reset} />;
    }
  }
const mapStateToProps = state => {
    return { ...state, user: state.user, register: state.register };
  };
  
  const mapDispatchToProps = dispatch => {
    return {
        logout: () => { dispatch(logout()) } ,
        reset: () => {
            dispatch(setRegisterPending(true));
            dispatch(setRegisterSuccess(false));
            dispatch(setRegisterError(null));
            dispatch(setLoginPending(true));
            dispatch(setLoginSuccess(false));
            dispatch(setLoginError(null));
        }
    };
  };

  export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(HeaderProfileWrapper);