import React, { Component } from 'react';
import { connect } from 'react-redux';
import LeftSidebar from '../components/left-sidebar';

class LeftSidebarWrapper extends Component {
    searchUnreadMsg = () => {
        return this.props.notification.messages.filter(x =>
            (x.senderId != this.props.user.id));
    }

    render() {
        return <LeftSidebar
            user={this.props.user}
            msg_for_read={this.searchUnreadMsg}
            filter={this.props.filter}
        />;
    }
}

const mapStateToProps = state => ({
    user: state.user,
    notification: state.notification,
    filter: state.events.filter,
});

export default connect(
    mapStateToProps
)(LeftSidebarWrapper);
