import React, { Component } from 'react';
import '../home/home.css';

import AdminEventListWrapper from '../../containers/events-for-admin';
import { connect } from 'react-redux';
import EventFilterWrapper from '../../containers/event-filter';

class EventsForAdmin extends Component {

    render() {
      

        return (
            <div className="row">
                <div className='col-9'> 
                    <AdminEventListWrapper params={this.props.location.search} />
                </div>
                <div className="col-3">
                    <EventFilterWrapper />
                </div>
            </div>

        );
    }
}

const mapStateToProps = state => {
    return { id: state.user.id };
};

export default connect(mapStateToProps)(EventsForAdmin);