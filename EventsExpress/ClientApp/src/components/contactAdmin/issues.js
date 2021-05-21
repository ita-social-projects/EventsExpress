import React, { Component } from 'react';
import ContactAdminListWrapper from '../../containers/contactAdmin/contactAdmin-list-container';
import ContactAdminFilterWrapper from '../../containers/contactAdmin/contactAdmin-filter-container';

export default class Issues extends Component {
    render() {
        return (<>
            <ContactAdminFilterWrapper />
            <div className="events-container">
                <ContactAdminListWrapper location={this.props.location} />
            </div>
        </>);
    }
}
