import React, {Component} from 'react';
import {connect} from 'react-redux';
 
import LeftSidebar from '../components/left-sidebar';

class LeftSidebarWrapper extends Component {

    render() {
      return <LeftSidebar user={this.props.user}/>;
    }
  }
  const mapStateToProps = state => {
      return { ...state, user: state.user};
  };
  
  const mapDispatchToProps = dispatch => {
  };

  export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(LeftSidebarWrapper);