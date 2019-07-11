import React from 'react';
import './NavMenu.css';
import { Link } from 'react-router-dom';



export default class NavItem extends React.Component {
    constructor(props) {
        super(props)

    }

    render() {
        return <li className="sidebar-header">
            <Link to={ this.props.to } className="active">
                <span className="link">
                    <i className= { this.props.icon } ></i>
                    <span className="hiden"> { this.props.text } </span>
                    <strong></strong>
                </span>
           </Link>
       </li>
    }
}
 