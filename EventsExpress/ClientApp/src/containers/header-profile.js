import React, {Component} from 'react';
import {connect} from 'react-redux';
import HeaderProfile from '../components/header-profile';
import logout from '../actions/logout';
 
class HeaderProfileWrapper extends Component {

    render() {
      return <HeaderProfile user={this.props.user} onClick={this.props.logout}/>;
    }
  }
  const mapStateToProps = state => {
      return { ...state, user: state.user};
  };
  
  const mapDispatchToProps = dispatch => {
    return {
      logout: () => { dispatch(logout()) } 
    };
  };

  export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(HeaderProfileWrapper);