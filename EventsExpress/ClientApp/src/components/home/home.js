import React, { Component } from 'react';
import './home.css';
import EventListWrapper from '../../containers/event-list';
import { connect } from 'react-redux';
import EventFilterWrapper from '../../containers/event-filter';

export default class Home extends Component {
    render() {
        return (<>
            <EventFilterWrapper />
            <div className="events-container">
                {/* TODO: Remove comment after debug. */}
                <EventListWrapper location={this.props.location} />
                {/* <EventListWrapper params={this.props.searchParams} /> */}
                {/* <EventListWrapper /> */}
            </div>
        </>);
    }
}

// const mapStateToProps = state => {
//     return {
//         id: state.user.id,
//         searchParams: state.events.searchParams,
//     };
// };

// export default connect(mapStateToProps)(Home);
