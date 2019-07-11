import React from 'react';
import './NavMenu.css';
import { Link } from 'react-router-dom';
import NavItem from './NavItem';
import "../../css/style.css"

export default class NavMenu extends React.Component {
    constructor(props) {
        super(props)
    }

    render() {
        return <div id="colorlib-page">
            <button id="sidebarCollapse" class="js-colorlib-nav-toggle colorlib-nav-toggle" > <i></i> </button>  
            <aside id="colorlib-aside" role="complementary" className="js-fullheight">
            <nav id="colorlib-main-menu" role="navigation">
                    <ul className="list-unstyled">
                        
                            <NavItem to={'/'} icon={'fas fa-home'} text={"Home"} />
                            <NavItem to={'/account/login'} icon={'fas fa-paper-plane'} text={"Login"} />


                        
                </ul>
            </nav>
            
        </aside> </div>
    }
}





