import React, { Component } from 'react';
import './home.css';
import Event from '../event';
import Profile from '../profile/index'
import { suggestions } from '../../containers/SelectCategories';

import AddEventWrapper from '../../containers/add-event';
import EventListWrapper from '../../containers/event-list';


export default class Home extends Component{
    
    render() {
        let user = {
            id: "1",
            Name: "Misha",
            Gender: 'Other',
            Age: '41',
            suggestions:[
                { id: 1, label: 'Summer' },
                { id: 2, label: 'Mount' },
                { id: 3, label: 'Party' },
                { id: 4, label: 'Gaming' },
            ],
        }
        
    return(
        <div>
            <Profile user={user} />        
            <AddEventWrapper />
        
            <EventListWrapper /> 

        </div>
        
    );
    }
}