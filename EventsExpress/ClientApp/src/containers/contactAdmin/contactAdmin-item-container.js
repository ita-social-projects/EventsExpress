import React, { Component } from "react";
import ContactAdminItem from "../../components/contactAdmin/contactAdmin-item-component";


export default class ContactAdminItemWrapper extends Component {

    render() {

        return <tr>
            <ContactAdminItem
                item={this.props.item}
            />
        </tr>
    };
}
