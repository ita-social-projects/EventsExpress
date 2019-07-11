import React from 'react';
import '../css/NavMenu.css';
import { Link } from 'react-router-dom';

export default props => (
    <div class="wrapper">
        <nav id="sidebar">
            <div className="sidebar-header">
                <h3>Event Express</h3>
                <strong>EE</strong>
            </div>

            <ul className="list-unstyled">
                <li className="sidebar-header">
                    <Link to={'/'} className="active">
                        <span className="link">    
                            <i className="fas fa-home"></i>
                            <span className="hiden"> Home</span>
                            <strong></strong>
                        </span>
                    </Link>
                </li>
                <li className="sidebar-header">
                    <Link to={'/account/login'}>
                        <span className="link">
                            <i className="fas fa-paper-plane"></i>
                            <span className="hiden">Login</span>
                            <strong></strong>
                        </span>
                    </Link>
                </li>
            </ul>


        </nav>
        
        <div id="content">

            <nav class="">
                <div class="container-fluid">
                    <button type="button" id="sidebarCollapse" class="btn btn-info  btn-sm" >
                        <i class="fas fa-arrows-alt-h"></i>
                        <span></span>
                    </button>
                <button class="btn btn-dark d-inline-block d-lg-none ml-auto" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <i class="fas fa-align-justify"></i>
                </button>

                    
                </div>
            </nav>


    </div>
    </div >

);
