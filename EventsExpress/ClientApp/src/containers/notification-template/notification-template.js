import React, { Component } from 'react';
import { connect } from 'react-redux';
import { get_all_templates } from "../../actions/notification-templates";
import NotificationTemplates from '../../components/notification_template';

class NotificationTemplateWrapper extends Component {

    componentDidMount = () => {
        this.props.get_all_templates();
    }

    render() {
        const { data } = this.props.notificationTemplates;
        return <NotificationTemplates templates={data} />;
    }
}

const mapStateToProps = (state) => ({
    notificationTemplates: state.NotificationTemplates
});

const mapDispatchToProps = (dispatch) => ({
    get_all_templates: () => dispatch(get_all_templates())
})

export default connect(mapStateToProps, mapDispatchToProps)(NotificationTemplateWrapper);
