import React, { Component } from 'react';
import './home.css';
import LoginWrapper from '../../containers/login';
 
export default class Home extends Component{
    
    render(){
    return(
        <div>
            <LoginWrapper />
        </div>
    );
    }
}