import React, { Component } from 'react';
import { enumLocationType } from '../../../constants/EventLocationType';
import DisplayMap from './display-map';
import DisplayOnline from './display-online';

class DisplayLocation extends Component {
    constructor(props) {
        super(props);
    }

    render() {

        if (this.props.location && this.props.location.type == enumLocationType.map) {
            return (

                <DisplayMap location={this.props.location} />
            )
        }
        else if (this.props.location && this.props.location.type == enumLocationType.online) {
            return (
                <DisplayOnline locationPath={this.props.location.onlineMeeting} />
            )
        }
        else {
            return null
        }
    }
}

export default DisplayLocation;

