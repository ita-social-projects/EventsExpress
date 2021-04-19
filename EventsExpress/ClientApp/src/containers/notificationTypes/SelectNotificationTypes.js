import React, { Component } from 'react';
import { connect } from 'react-redux';
import setUserNotificationTypes from '../../actions/redactProfile/userNotificationType-add-action';
import get_notificationTypes from '../../actions/notificationType/notificationType-list-action';
import SelectNotificationType from '../../components/SelectNotificationTypes/SelectNotificationType';

class SelectNotificationTypesWrapper extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount = () => {
        this.props.get_notificationTypes();
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
        get_notificationTypes: () => dispatch(get_notificationTypes())
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SelectNotificationTypesWrapper)
