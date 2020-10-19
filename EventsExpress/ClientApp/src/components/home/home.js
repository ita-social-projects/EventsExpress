import React, { Component } from 'react';
import './home.css';
import EventListWrapper from '../../containers/event-list';
import { connect } from 'react-redux';
import EventFilterWrapper from '../../containers/event-filter';

class Home extends Component {
    render() {
        return (<>
            <EventFilterWrapper />
            <div className="events-container">
                {/* <EventListWrapper params={this.props.location.search} /> */}
                <EventListWrapper params={this.props.searchParams} />
            </div>
        </>);
    }
}

const mapStateToProps = state => {
    return {
        id: state.user.id,
        searchParams: state.events.searchParams,
    };
};

export default connect(mapStateToProps)(Home);
