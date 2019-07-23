import React, { Component } from 'react';
import './home.css';
import AddEventWrapper from '../../containers/add-event';
import EventListWrapper from '../../containers/event-list';
import AddCAtegory from '../../containers/add-category';
import CategoryListWrapper from '../../containers/category-list';


export default class Home extends Component{
    
    render(){
     
    return(
        <div>
            <AddEventWrapper />
            
            <EventListWrapper /> 
          
        </div>
        
    );
    }
}