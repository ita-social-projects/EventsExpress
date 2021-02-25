import React, { Component } from 'react';
import EventDraftListWrapper from '../../containers/event-draft-list';


export default class Draft extends Component {
    render() {
        return (<>
            
            
            <EventDraftListWrapper location={this.props.location}/>
            
        </>);
    }
}
