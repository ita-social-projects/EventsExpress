import React, { Component } from 'react';
import '../home/home.css';

import AdminEventListWrapper from '../../containers/events-for-admin';
import { connect } from 'react-redux';
import EventFilterWrapper from '../../containers/event-filter';

class EventsForAdmin extends Component {

    render() {
      

        return (<>
            <EventFilterWrapper/>
            <div className="events-container">
                <AdminEventListWrapper params={this.props.location.search} />       
            </div>
            
        </>);
    }
}

const mapStateToProps = state => {
    return { id: state.user.id };
};

export default connect(mapStateToProps)(EventsForAdmin);