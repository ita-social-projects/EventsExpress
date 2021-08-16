import React, { Component } from 'react';
import { connect } from 'react-redux';
import { get_template, get_template_properties, update_template } from '../../actions/notification-templates';
import NotificationTemplateForm from '../../components/notification-template/notification-template-form';

class NotificationInfoWrapper extends Component {

    handleSubmit = async (values) => {
        await this.props.update_template(values);
        this.props.history.push('/admin/notificationTemplates');
    }

    componentDidMount() {
        const { id } = this.props.match.params;
        this.props.get_template(id);
        this.props.get_properties(id);
    }

    render() {
        const { notificationTemplate,
            notificationTemplate: { availableProperties } } = this.props;

        return <>
            <NotificationTemplateForm
                initialValues={notificationTemplate}
                availableProps={availableProperties}
                onSubmit={this.handleSubmit}
            />
        </>
    }
}

const mapStateToProps = (state) => ({
    notificationTemplate: state.NotificationTemplate
});

const mapDispatchToProps = (dispatch) => ({
    get_template: (id) => dispatch(get_template(id)),
    get_properties: (template_id) => dispatch(get_template_properties(template_id)),
    update_template: (template) => dispatch(update_template(template))
})

export default connect(mapStateToProps, mapDispatchToProps)(NotificationInfoWrapper);
