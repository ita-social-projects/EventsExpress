import React, { Component } from 'react';
import ContactUsItemWrapper from '../../containers/contactUs/contactUs-item-container';

export default class ContactUsList extends Component {
    renderItems = arr => arr.map(item => <ContactUsItemWrapper
        key={item.senderId}
        item={item} />);

    render() {
        let { data_list} = this.props;
        return (
            <>
                <tr>
                    <td>Title</td>
                    <td className="d-flex align-items-center justify-content-center">Date created</td>
                    <td className="justify-content-center">Status</td>
                </tr>
                {this.renderItems(data_list)}
            </>);
    }
}
