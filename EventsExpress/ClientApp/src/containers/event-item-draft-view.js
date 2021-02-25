import React, { Component } from 'react';
import EventListWrapper from '../../containers/event-list';
import EventFilterWrapper from '../../containers/event-filter';


export default class EventItemDraftViewWrapper extends Component
{
    render() {
        return (<>
            <EventFilterWrapper />
            <div className="events-container">
                <EventListWrapper location={this.props.location} />
            </div>
        </>);
    }
    
}
