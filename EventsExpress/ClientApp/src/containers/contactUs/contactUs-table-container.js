import React from 'react';
import ContactUsTable from '../../components/contactUs/contactUs-table-component';


export default ContactUsTableWrapper = (props) => (
    <div>
        <ContactUsTable match={props.match} />
    </div>
);
