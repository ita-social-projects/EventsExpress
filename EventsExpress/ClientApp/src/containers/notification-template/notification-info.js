import React, { Component } from 'react';
import { connect } from 'react-redux';
import { get_template, update_template } from "../../actions/notification-templates";
import NotificationTemplateForm from "../../components/notification_template/notification-template-info";

class NotificationInfoWrapper extends Component {
    
    handleSubmit = async (values) => {
        await this.props.update_template(values);
        this.props.history.push("/admin/notificationTemplates");
    }

    componentWillMount = () => {
        const { id } = this.props.match.params;
        this.props.get_template(id);
    }

    render() {
        const { notificationTemplate } = this.props;
        return <NotificationTemplateForm
            initialValues={notificationTemplate}
            onSubmit={this.handleSubmit}/>
    }
}

const mapStateToProps = (state) => ({
    notificationTemplate: state.NotificationTemplate
});

const mapDispatchToProps = (dispatch) => ({
    get_template: (id) => dispatch(get_template(id)),
    update_template: (template) => dispatch(update_template(template))
})

export default connect(mapStateToProps, mapDispatchToProps)(NotificationInfoWrapper);
