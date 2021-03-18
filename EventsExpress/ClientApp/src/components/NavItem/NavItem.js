import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './NavItem.css';

export const NavItem = ({ to, icon, text, my_icon }) => {
    return (
        <li className="sidebar-header">
            <Link to={to} className="active">
                <span className="link">
                    <i className={icon + ' nav-item-icon'}></i>
                    {my_icon}
                    <span className="nav-item-text">&nbsp;{text}</span>
                    <strong></strong>
                </span>
            </Link>
        </li>
    );
}
