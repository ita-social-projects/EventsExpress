import React, { Component } from 'react';
import { connect } from 'react-redux';

import LeftSidebar from '../components/left-sidebar';

class LeftSidebarWrapper extends Component {

  search_unread_msg = () => {
    return this.props.notification.messages.filter(x => (x.senderId != this.props.user.id) );
  }

  render() {
    return <LeftSidebar user={this.props.user} msg_for_read={this.search_unread_msg} />;
  }
}
const mapStateToProps = state => {
  return { user: state.user, notification: state.notification };
};

export default connect(
  mapStateToProps
)(LeftSidebarWrapper);