import { connect } from 'react-redux';
import React, {Component} from 'react';
import Login  from '../components/login';
import login from '../actions/login';


class LoginWrapper extends Component {
  submit = values => {
    console.log(values);
    this.props.login(values.email, values.password);
  };
  render() {

    let { isLoginPending, isLoginSuccess, loginError } = this.props;
    
    return <Login onSubmit={this.submit} />;
  }
}
const mapStateToProps = state => {
    return {
      ...state,
    isLoginPending: state.login.isLoginPending,
    isLoginSuccess: state.login.isLoginSuccess,
    loginError: state.login.loginError
  };
};

const mapDispatchToProps = dispatch => {
  return {
    login: (email, password) => dispatch(login(email, password))
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(LoginWrapper);