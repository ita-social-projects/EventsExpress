import React, { Component } from 'react';
import './home.css';
import EventListWrapper from '../../containers/event-list';

import { connect } from 'react-redux';
import EventFilterWrapper from '../../containers/event-filter';


class Home extends Component {

    render() {

        return (<>
            <EventFilterWrapper/>
            <div className="events-container">
                <EventListWrapper params={this.props.location.search} />                   
            </div>
            
        </>);
    }
}

const mapStateToProps = state => {
    return { id: state.user.id };
};

export default connect(mapStateToProps)(Home);