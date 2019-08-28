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


const LeftSidebar = (props) => {
    
    function onClick() {
        console.log('click', document.getElementsByTagName('body')[0].classList, document.getElementsByTagName('body')[0].classList.contains('offcanvas'));
        if (document.getElementsByTagName('body')[0].classList.contains('offcanvas')) {
            console.log(document.getElementById('sidebarCollapse'));
            document.getElementById('sidebarCollapse').classList.remove('active');
            console.log('remove');
            document.getElementsByTagName('body')[0].classList.remove('offcanvas');

            console.log('remove');
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
                            
                            <NavItem to={'/user_chats'} icon={'fa fa-envelope'} text={"Comuna"} />
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
                            <NavItem to={'/user/contactUs'} icon={'fa fa-exclamation-circle'} text={'Contact us'} />
                            </>
                         }
                         
                        
                </ul>
            </nav>
            
        </div> </div>
            );
}

export default LeftSidebar;