import React, { Component } from 'react';
import { connect } from 'react-redux';
import setUserNotificationTypes from '../../actions/redactProfile/userNotificationType-add-action';
import get_notificationTypes from '../../actions/notificationType/notificationType-list-action';
import SelectNotificationType from '../../components/SelectNotificationTypes/SelectNotificationType';
import get_userNotificationTypes from '../../actions/notificationType/userNotificationType-action';

class SelectNotificationTypesWrapper extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentWillMount = () => {
        this.props.get_notificationTypes();
        this.props.getuserNotificationTypes();
    }

    handleSubmit(event) {
        this.props.setUserNotificationTypes({
            notificationTypes: event.notificationTypes
        });
        event.notificationTypes = [];
    }

    render() {
        return <SelectNotificationType
            items={this.props.allNotificationTypes.data}
            initialValues={{ notificationTypes: this.props.user.notificationTypes }}            
            onSubmit={this.handleSubmit}
        />;
    }
}
const mapStateToProps = (state) => {
    return {
        allNotificationTypes: state.notificationType,
        user: state.user,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        setUserNotificationTypes: (data) => dispatch(setUserNotificationTypes(data)),
        get_notificationTypes: () => dispatch(get_notificationTypes()),
        getuserNotificationTypes: () => dispatch(get_userNotificationTypes())
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SelectNotificationTypesWrapper)
