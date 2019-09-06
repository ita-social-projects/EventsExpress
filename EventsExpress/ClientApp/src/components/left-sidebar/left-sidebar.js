import React from 'react';
import './left-sidebar.css';
import HeaderProfileWrapper from '../../containers/header-profile';
import { Link } from 'react-router-dom';

import Badge from '@material-ui/core/Badge';

const NavItem = ({to, icon, text, my_icon}) => {
    return (<li className="sidebar-header">
        <Link to={ to } className="active">
            <span className="link">
                <i className= { icon } ></i>
                {my_icon}
                <span className="nav-item-text"> &nbsp; { text } </span>
                <strong></strong>
            </span>
        </Link>
    </li>
    );
}


const LeftSidebar = (props) => {
    
    function onClick() {        
        if (document.getElementsByTagName('body')[0].classList.contains('offcanvas')) {
            document.getElementById('sidebarCollapse').classList.remove('active');
            document.getElementsByTagName('body')[0].classList.remove('offcanvas');
        } else {
            document.getElementById('sidebarCollapse').classList.add('active');
            document.getElementsByTagName('body')[0].className += " offcanvas";
        }
    }

    return (
        <div id="colorlib-page">
            <button id="sidebarCollapse" onClick={onClick} className="js-colorlib-nav-toggle colorlib-nav-toggle" type="button" > <i></i> </button>  
            <div id="colorlib-aside" role="complementary" className="js-fullheight">
                <HeaderProfileWrapper/>
                <nav id="colorlib-main-menu" role="navigation">
                    <hr/>
                    <ul className="list-unstyled">
                        
                        <NavItem to={'/home/events/?page=1'} icon={'fa fa-home'} text={"Home"} />
                        {props.user.id &&
                        <>
                            <NavItem to={'/user/' + props.user.id} icon={'fa fa-user'} text={"Profile"} />
                            <NavItem to={'/search/users?page=1'} icon={'fa fa-users'} text={"Search Users"} />
                            <NavItem to={'/user_chats'} my_icon={
                                <Badge badgeContent={props.msg_for_read().length} color="primary">
                                    <i className="fa fa-envelope"></i>
                                </Badge>} text={"Comuna"} />
                        </>
                        }
                        {props.user.role === "Admin" &&
                        <>
                            <NavItem to={'/admin/categories/'} icon={'fa fa-hashtag'} text={"Categories"} />
                            <NavItem to={'/admin/users?page=1'} icon={'fa fa-users'} text={"Users"} />
                            <NavItem to={'/admin/events?page=1'} icon={'fa fa-calendar'} text={"Events"} />
                        </>                       
                        }
                        {props.user.role==="User"&&
                        <>
                            <NavItem to={'/contactUs'} icon={'fa fa-exclamation-circle'} text={'Contact us'} />
                        </>
                        }
                    </ul>
                </nav>            
            </div> 
        </div>
    );
}

export default LeftSidebar;