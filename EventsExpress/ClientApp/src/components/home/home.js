import React, { Component } from 'react';
import EventListWrapper from '../../containers/event-list';
import EventFilterWrapper from '../../containers/event-filter';
import './home.css';

export default class Home extends Component {
    render() {
        return (<>
            <EventFilterWrapper />
            <div className="events-container">
                <EventListWrapper location={this.props.location} />
            </div>
        </>);
    }
}
