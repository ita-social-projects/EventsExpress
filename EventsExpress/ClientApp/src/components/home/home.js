import React, { Component } from 'react';
import './home.css';
import Event from '../event';
 
export default class Home extends Component{
    
    render(){
    return(
        <div>
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