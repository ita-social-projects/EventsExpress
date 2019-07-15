import React from 'react';
import './NavMenu.css';
import { Link } from 'react-router-dom';



export default class NavItem extends React.Component {
    constructor(props) {
        super(props)

    }

    render() {
        return <li className="sidebar-header">
            <Link to={ this.props.to } >
                <span className="link">
                    <i className= { this.props.icon } >  </i>
                    <span className="hiden"> &nbsp; { this.props.text } </span>
                    <strong></strong>
                </span>
           </Link>
       </li>
    }
}
 