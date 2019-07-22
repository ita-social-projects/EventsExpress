import React, { Component } from 'react';
import './home.css';
import Event from '../event';
import Profile from '../profile/index'
import AddEventWrapper from '../../containers/add-event';
import EventListWrapper from '../../containers/event-list';
import AddCAtegory from '../../containers/add-category';
import CategoryListWrapper from '../../containers/category-list';


export default class Home extends Component{
    
    render() {
        let user = {
            id: "1",
            Name: "Misha",
            Gender: 'Other',
            Age: '41',
            suggestions: [
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
       
           