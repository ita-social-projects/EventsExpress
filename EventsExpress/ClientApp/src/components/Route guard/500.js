import React, { Component } from 'react';
import './css/error.css';
import { Link } from 'react-router-dom'


export default class InternalServerError extends Component {
    render() {
        return <>
            <div id="notfound">
                <div class="notfound">
                    <div class="notfound-404">
                        <h1>Oops!</h1>
                    </div>
                    <h2>500! - Internal Server Error</h2>
                </div>
            </div>
        </>
    }
}