import React from 'react';
import LeftSidebarWrapper from '../../containers/left-sidebar';
import './layout.css';
import './colorlib.css';

const Layout = ({ children }) => {
    return (
        <>
            <LeftSidebarWrapper />
            <div id="main" className="container-fluid h-100 pl-5">
                {children}
            </div>
        </>
    );
}

export default Layout;
