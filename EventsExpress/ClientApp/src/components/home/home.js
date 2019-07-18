import React, { Component } from 'react';
import './home.css';
import Event from '../event';
import Profile from '../profile/index'
export default class Home extends Component{
    
    render() {
        let user = {
            id: "1",
            Name: "DimaKundiy"
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