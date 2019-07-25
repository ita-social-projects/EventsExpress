import React, { Component } from 'react';
import './users.css';


export default class Users extends Component{
    
    renderUsers = (arr) => {
        return arr.map(x => {
            return <p>{x.username}</p>
        }            );
    }

    render(){
     
        const {data} = this.props.users;

    return(
        <div>
            {this.renderUsers(data)}
        </div>
        
    );
    }
}