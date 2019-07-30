import React from 'react';
import './left-sidebar.css';
import HeaderProfileWrapper from '../../containers/header-profile';
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


const LeftSidebar = (props) =>{
    return (
    <div id="colorlib-page">
            <button id="sidebarCollapse" className="js-colorlib-nav-toggle colorlib-nav-toggle" > <i></i> </button>  
            <div id="colorlib-aside" role="complementary" className="js-fullheight">
                <HeaderProfileWrapper/>
                <nav id="colorlib-main-menu" role="navigation">

                    <ul className="list-unstyled">
                        
                        <NavItem to={'/'} icon={'fa fa-home'} text={"Home"} />
                            {props.user.id &&
                            <NavItem to={'/profile'} icon={'fa fa-user'} text={"Profile"} />
                            
                            }
                        {props.user.role === "Admin" &&
                        <>
                            <NavItem to={'/admin/categories/'} icon={'fa fa-hashtag'} text={"Categories"} />
                            
                            <NavItem to={'/admin/users/'} icon={'fa fa-users'} text={"Users"} />
                            
                            <NavItem to={'/admin/events/'} icon={'fa fa-calendar'} text={"Events"} />
                            
                        </>
                        }

                        
                </ul>
            </nav>
            
        </div> </div>
            );
}

export default LeftSidebar;