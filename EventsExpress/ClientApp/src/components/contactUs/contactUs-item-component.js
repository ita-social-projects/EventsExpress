import React, { Component } from "react";

export default class ContactUsItem extends Component {
    
    render() {
        const { item } = this.props;
        
        return (<>
            <td>
                <i className="fas fa-hashtag mr-1"></i>
                {item.title}
            </td>
            <td className="d-flex align-items-center justify-content-center">
                {item.dateCreated}
            </td>
            <td className="justify-content-center">
                {item.status}
            </td>

        </>);
    }
}