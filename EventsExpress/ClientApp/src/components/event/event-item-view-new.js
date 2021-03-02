import React, { Component } from 'react';
import EditEventWrapper from '../../containers/edit-event';
import 'moment-timezone';
import '../layout/colorlib.css';
import './event-item-view.css';

export default class EventItemViewNew extends Component {

    render() {        
        return <>
                                <EditEventWrapper/>   
        </>
    }
}

