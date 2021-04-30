import React, { Component } from 'react';
import ContactUsList from '../../components/contactUs/contactUs-list-component';


export default class ContactUsListWrapper extends Component {

    render() {
        const { data } = this.props;

        return <ContactUsList data_list={data} /> 
    }
}