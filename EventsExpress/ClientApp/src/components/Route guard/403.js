import React, { Component } from 'react';
import './css/error.css';


export default class Forbidden extends Component {
    render() {
        return <>
            <div id="notfound">
                <div className="notfound">
                    <div className="notfound-404">
                        <h1>Oops!</h1>
                    </div>
                    <h2>403! - Forbidden</h2>
                </div>
            </div>
        </>
    }
}