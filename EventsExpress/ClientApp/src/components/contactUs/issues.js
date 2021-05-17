import React, { Component } from 'react';
import ContactUsListWrapper from '../../containers/contactUs/contactUs-list-container';
import ContactUsFilterWrapper from '../../containers/contactUs/contactUs-filter-container';

export default class Issues extends Component {
    render() {
        return (<>
            <ContactUsFilterWrapper />
            <div className="events-container">
                <ContactUsListWrapper location={this.props.location} />
            </div>
        </>);
    }
}
