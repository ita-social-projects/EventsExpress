import React, { Component } from 'react';
import EventListWrapper from '../../containers/event-list';
import EventFilterWrapper from '../../containers/event-filter';
import './home.css';
import { Filter } from '../event/filter/filter';

export default class Home extends Component {
    render() {
        return (
            <>
                <div className="events-container">
                    <EventListWrapper location={this.props.location} />
                </div>
                <Filter />
            </>
        );
    }
}
