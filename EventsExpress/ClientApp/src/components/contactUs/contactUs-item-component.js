import React, { Component } from "react";
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import Moment from 'moment';
import { Link } from 'react-router-dom'

export default class ContactUsItem extends Component {

    render() {
        const { item } = this.props;

        return (<>
            <td>
                <i className="fas fa-hashtag mr-1"></i>
                {item.title}
            </td>
            <td className="d-flex align-items-center justify-content-center">
                {Moment(item.dateCreated).format('YYYY-MM-DD HH:MM')}
            </td>
            <td className="justify-content-center text-center">
                {item.status}
            </td>
            <td className="justify-content-center ">
                <Link to={`/contactUs/${item.messageId}/UpdateStatus`}>
                    <Tooltip title="View">
                        <IconButton style={{ fontSize: 16 }} aria-label="view" >
                            <i className="fa fa-eye "></i>
                        </IconButton>
                    </Tooltip>
                </Link>
            </td>
        </>);
    }
}
