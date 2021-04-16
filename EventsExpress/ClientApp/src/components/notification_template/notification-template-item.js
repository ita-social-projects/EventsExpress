import React, { Component } from 'react';
import { IconButton } from '@material-ui/core';
import { Link } from 'react-router-dom';

export default class NotificationTemplateItem extends Component {

    render() {
        const { template } = this.props;

        return (
            <tr key={template.id}>
                <td>{template.id}</td>
                <td>{template.title}</td>
                <td>{template.subject}</td>
                <td>{template.message}</td>
                <td>
                    <Link to={"/admin/notificationTemplate/" + template.id}>
                        <IconButton className="text-info" size="small">
                            <i className="fas fa-edit" />
                        </IconButton>
                    </Link>
                </td>
            </tr>
        );
    }
}
