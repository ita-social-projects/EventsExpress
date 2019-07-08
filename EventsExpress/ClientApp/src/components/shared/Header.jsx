import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';

export default class Header extends React.Component {
    constructor(props) {
        super(props);

    }

    render = () =>
        <nav className="navbar navbar-expand-sm bg-dark">
            <ul className="navbar-nav">
                <li className="nav-item">
                    <a className="nav-link" href="#">Home</a>
                </li>
                <li className="nav-item">
                    <a className="nav-link" href="#" >Login</a>
                </li>
                <li className="nav-item">
                    <a className="nav-link" href="#">Register</a>
                </li>
            </ul>
        </nav>

}

