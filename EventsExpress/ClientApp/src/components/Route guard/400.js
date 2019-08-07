import React, { Component } from 'react';
import './css/404.css';
import { Link } from 'react-router-dom'
import EventListWrapper from '../../containers/event-list'
export default class NotFound extends Component {
    render() {
        return <>
            <div id="notfound">
                <div class="notfound">
                    <div class="notfound-404">
                        <h1>Oops!</h1>
                    </div>
                    <br />
                    <br />
                    <h2>400! - Bad Request</h2>
                   
                </div>
            </div>
        </>
    }
}