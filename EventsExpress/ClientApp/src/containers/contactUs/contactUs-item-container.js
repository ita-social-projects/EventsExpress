import React, { Component } from "react";
import ContactUsItem from "../../components/contactUs/contactUs-item-component";


export default class ContactUsItemWrapper extends Component {

    render() {

        return <tr>
            <ContactUsItem
                item={this.props.item}
            />
        </tr>
    };
}
