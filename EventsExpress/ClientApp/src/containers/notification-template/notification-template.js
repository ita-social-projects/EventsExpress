import React, { Component } from 'react';
import { connect } from 'react-redux';
import { get_all_templates } from "../../actions/notification-templates";
import { parse } from 'query-string';
import NotificationTemplates from '../../components/notification_template';

class NotificationTemplateWrapper extends Component {

    componentDidMount = () => {
        const { search } = this.props.location;
        const { page, pageSize } = parse(search);
        
        this.props.get_all_templates(page, pageSize);
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
    get_all_templates: (pageNumber, pageSize) => dispatch(get_all_templates(pageNumber, pageSize))
})

export default connect(mapStateToProps, mapDispatchToProps)(NotificationTemplateWrapper);
