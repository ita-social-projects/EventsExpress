import React, { Component } from 'react';
import EventListWrapper from '../../containers/event-list';
import './home.css';
import { Filter } from '../event/filter/filter';
import QuickFilters from '../event/quick-filters/quick-filters';

export default class Home extends Component {
    render() {
        return (
            <>
                <div className="events-container">
                    <QuickFilters />
                    <EventListWrapper location={this.props.location} />
                </div>
                <Filter />
            </>
        );
    }
}
