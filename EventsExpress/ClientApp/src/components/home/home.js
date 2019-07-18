import React, { Component } from 'react';
import './home.css';
import Event from '../event';
import Profile from '../profile/index'
import { suggestions } from '../../containers/SelectCategories';

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
            <Event 
                title="A Loving Heart is the Truest Wisdom"
                date_from="June 28, 2019"
                comment_count="5"
                description="A small river named Duden flows by their place and supplies it with the necessary regelialia."/>
              <Event 
                title="A Loving Heart is the Truest Wisdom"
                date_from="June 28, 2019"
                comment_count="5"
                description="A small river named Duden flows by their place and supplies it with the necessary regelialia."/>
    
        </div>
        
    );
    }
}