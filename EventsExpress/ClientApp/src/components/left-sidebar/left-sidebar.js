import React from 'react';
import './left-sidebar.css';
import HeaderProfile from '../header-profile';
import { Link } from 'react-router-dom';

const NavItem = ({to, icon, text}) => {
    return (<li className="sidebar-header">
        <Link to={ to } className="active">
            <span className="link">
                <i className= { icon } ></i>
                <span className="hiden"> &nbsp; { text } </span>
                <strong></strong>
            </span>
        </Link>
    </li>
    );
}


const LeftSidebar = () =>{
    return (
    <div id="colorlib-page">
            <button id="sidebarCollapse" className="js-colorlib-nav-toggle colorlib-nav-toggle" > <i></i> </button>  
            <div id="colorlib-aside" role="complementary" className="js-fullheight">
                <HeaderProfile/>
                <nav id="colorlib-main-menu" role="navigation">

                    <ul className="list-unstyled">
                        
                            <NavItem to={'/'} icon={'fa fa-home'} text={"Home"} />
                            <NavItem to={'/profile'} icon={'fa fa-paper-plane'} text={"Profile"} />


                        
                </ul>
            </nav>
            
        </div> </div>
            );
}

export default LeftSidebar;